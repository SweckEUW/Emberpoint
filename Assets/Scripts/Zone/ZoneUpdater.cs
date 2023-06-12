using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class ZoneUpdater : MonoBehaviour
{

    public GameObject zoneMeshPrefab;
    public GameObject zoneCirclePrefab;

    public GameObject mainZone;
    private float threshold = 1;
    
    void Awake(){ 
        Vector3 position = GameObject.Find("Core").transform.position;
        position += new Vector3(0,-10,0);
        this.mainZone = CreateZoneCircle(60,position);
    }
    
    public void UpdateZone(){
        foreach (Transform child in GameObject.Find("ZoneMeshes").transform)
            Destroy(child.gameObject);
        
        // Find intersecting circle groups
        List<List<ZoneCircle>> circleGroups = FindCircleGroups();

        // Merge intersecting circle groups 
        List<Mesh> meshes = MergeCircleGroups(circleGroups);

        // Create zone meshes
        foreach (Mesh mesh in meshes)
            CreateZoneMesh(mesh);
    }

    List<List<ZoneCircle>> FindCircleGroups(){
        List<List<ZoneCircle>> circleGroups = new List<List<ZoneCircle>>();
        
        List<ZoneCircle> allZoneCircles = new List<ZoneCircle>();
        foreach (Transform child in GameObject.Find("ZoneCircles").transform)
            allZoneCircles.Add(child.GetComponent<Circle>().circle);
        
        while(allZoneCircles.Count > 0){
            List<ZoneCircle> circleGroup = new List<ZoneCircle>();
            CheckIntersections(allZoneCircles[0],allZoneCircles,circleGroup);
            circleGroups.Add(circleGroup);
        }

        return circleGroups;
    }

    List<Mesh> MergeCircleGroups(List<List<ZoneCircle>> circleGroups){
        List<Mesh> meshes = new List<Mesh>();
        foreach(List<ZoneCircle> circleGroup in circleGroups) 
            meshes.Add(MergeCircleGroup(circleGroup));

        return meshes;
    }

    Mesh MergeCircleGroup(List<ZoneCircle> circleGroup){
        List<List<Vector3>> allVertices = new List<List<Vector3>>();

        for (int m = 0; m < circleGroup.Count; m++){
            
            Vector3[] pointsToFilter = circleGroup[m].getPoints();
            List<ZoneCircleVertex> newPoints = new List<ZoneCircleVertex>();

            for (int i = 0; i < pointsToFilter.Length; i++){

                bool removePoint = false;

                for (int j = 0; j < circleGroup.Count; j++){
                    if(circleGroup[j] != circleGroup[m]){
                        Vector3 position = circleGroup[j].getPosition();
                        float radius = circleGroup[j].getRadius();

                        if((Vector3.Distance(pointsToFilter[i],position) < radius+threshold))
                            removePoint = true;
                    }
                }

                if(removePoint){
                    if(newPoints.Count > 0){
                        newPoints[newPoints.Count-1].setLooseEnd(true); //Mark Last added Point to loose end        
                    }
                }else{
                    newPoints.Add(new ZoneCircleVertex(pointsToFilter[i]));
                }
            }

            int firstCutIndex = 999999;
            for (int i = 0; i < newPoints.Count; i++)
                if(newPoints[i].isLooseEnd()){
                    firstCutIndex = i+1;
                    break;
                }
                
            firstCutIndex = firstCutIndex == 999999 ? 0 : firstCutIndex;
            List<Vector3> splittedList = new List<Vector3>();

            int cutCount = 0;

            for (int i = firstCutIndex; i < newPoints.Count; i++){
                splittedList.Add(newPoints[i].vertex);
                if(newPoints[i].isLooseEnd()){
                    cutCount++;
                    allVertices.Add(splittedList);
                    splittedList = new List<Vector3>();
                }
            }

            for (int i = 0; i < firstCutIndex; i++)
                splittedList.Add(newPoints[i].vertex);
            
            if(splittedList.Count > 0)
                allVertices.Add(splittedList);
        }

        Vector3[] points = MergeVertices(allVertices);

        Mesh mesh = ConstructMesh(points);

        return mesh;
    }

    Vector3[] MergeVertices(List<List<Vector3>> allVertices){
        List<Vector3> combinedVertices = new List<Vector3>();

        int removeIndex = 0;
        List<Vector3> currentList = allVertices[removeIndex];
        Vector3 nextLooseEnd = allVertices[removeIndex][allVertices[removeIndex].Count-1];
        Vector3 looseEnd = allVertices[removeIndex][allVertices[removeIndex].Count-1];

        combinedVertices.AddRange(currentList);
        allVertices.RemoveAt(removeIndex);

        while(allVertices.Count > 0){    

            float closestDistance = 99999999;

            for (int m = 0; m < allVertices.Count; m++){

                float distance1 = Vector3.Distance(looseEnd,allVertices[m][0]);
                float distance2 = Vector3.Distance(looseEnd,allVertices[m][allVertices[m].Count-1]);
                float distance = Mathf.Min(distance1,distance2);

                if(distance < closestDistance){
                    removeIndex = m;
                    closestDistance = distance;
                    nextLooseEnd = allVertices[m][distance1 > distance2 ? 0 : allVertices[m].Count-1];
                }   
            }

            currentList = allVertices[removeIndex];

            // Vector3 shouldBeConnectedEnd = currentList[0];  
            // float distanceToConnected = Vector3.Distance(looseEnd,shouldBeConnectedEnd);
            // if(distanceToConnected > 25)
            //     currentList.Reverse();  
            

            looseEnd = nextLooseEnd;

            combinedVertices.AddRange(currentList);

            allVertices.RemoveAt(removeIndex);
        }

        return combinedVertices.ToArray();
    }


    void CheckIntersections(ZoneCircle circle, List<ZoneCircle> testAgainst, List<ZoneCircle> circleGroup){
        if(testAgainst.Count > 0){
            testAgainst.Remove(circle);
            if(!circleGroup.Contains(circle))
                circleGroup.Add(circle);

            List<ZoneCircle> intersectedCircles = new List<ZoneCircle>();

            for (int i = 0; i < testAgainst.Count; i++)
                if(CheckIntersection(circle,testAgainst[i]))
                    intersectedCircles.Add(testAgainst[i]);

            for (int i = 0; i < intersectedCircles.Count; i++)
                CheckIntersections(intersectedCircles[i],testAgainst,circleGroup); 
        }
    }

    bool CheckIntersection(ZoneCircle circle1, ZoneCircle circle2){
        float r1 = circle1.getRadius();
        Vector3 p1 = circle1.getPosition();

        float r2 = circle2.getRadius();
        Vector3 p2 = circle2.getPosition();

        float distance = Vector3.Distance(p1, p2);

        return distance < r1+r2+threshold;
    }

    Mesh ConstructMesh(Vector3[] points) {        
        // Genreate vertices
		Vector3[] vertices = new Vector3[points.Length*2];
        for (int i = 0; i < points.Length; i++){
            vertices[i*2] = points[i];
            vertices[i*2+1] = points[i] + new Vector3(0,30,0); //ZoneHeight = 10
        }

        // Generate indices
		int[] triangles = new int[vertices.Length*3];
        for (int i = 0; i < triangles.Length-6; i+=6){
            triangles[i] = i/3;
            triangles[i+1] = i/3+1;
            triangles[i+2] = i/3+2;

            triangles[i+3] = i/3+2;
            triangles[i+5] = i/3+3;  
            triangles[i+4] = i/3+1; 
        }

        // Catch Meshes that have 0 vertices
        // TODO: put this code into loop above
        if(triangles.Length > 0){
            triangles[triangles.Length-6] = (triangles.Length-6)/3;
            triangles[triangles.Length-5] = (triangles.Length-6)/3+1;
            triangles[triangles.Length-4] = 0;

            triangles[triangles.Length-3] = 0;
            triangles[triangles.Length-2] = (triangles.Length-6)/3+1;
            triangles[triangles.Length-1] = 1;
        }
       
		// Generate UV
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i+=2){
            uvs[i] = new Vector2((float)i/uvs.Length,0);
            uvs[i+1] = new Vector2((float)i/uvs.Length,1);
        }

        Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
        mesh.uv = uvs;
		mesh.Optimize();
		// mesh.RecalculateNormals();

        return mesh;
	}

    public void CreateZoneMesh(Mesh mesh){
        GameObject zoneMesh = Instantiate(zoneMeshPrefab, new Vector3(0,0,0) , Quaternion.identity);

        foreach(Transform child in zoneMesh.transform){
            if(child.gameObject.GetComponent<MeshFilter>()){
                child.gameObject.GetComponent<MeshFilter>().mesh = mesh;

                // child.gameObject.GetComponent<Renderer>().material.SetFloat("AdjustedScale",200f/mesh.vertexCount);
            }
             
        }
        
        foreach(Transform child in zoneMesh.transform)
            if(child.gameObject.GetComponent<VisualEffect>())
                child.gameObject.GetComponent<VisualEffect>().SetMesh("New Mesh",mesh);

        zoneMesh.transform.SetParent(GameObject.Find("ZoneMeshes").transform);
    }

    public void shrinkZone(){
        if(this.mainZone.GetComponent<Circle>().radius > 20)
            this.mainZone.GetComponent<Circle>().radius -= 20;
    }

    public GameObject CreateZoneCircle(float radius, Vector3 position){
        GameObject zoneCircle  = Instantiate(zoneCirclePrefab, new Vector3(0,0,0) , Quaternion.identity);
        zoneCircle.GetComponent<Circle>().CreateCircle(radius,position);
        zoneCircle.transform.SetParent(GameObject.Find("ZoneCircles").transform);
        
        UpdateZone();

        return zoneCircle;
    }

    public bool isPointInsideZone(Vector3 point){
        List<ZoneCircle> allZoneCircles = new List<ZoneCircle>();
        foreach (Transform child in GameObject.Find("ZoneCircles").transform)
            allZoneCircles.Add(child.GetComponent<Circle>().circle);
        
        foreach (ZoneCircle circle in allZoneCircles)
            if(circle.getRadius() > Vector3.Distance(circle.getPosition(),point))
                return true;
        
        return false;
    }


    public void CreateTotem(Vector3 position){
        GameObject zoneCircle  = Instantiate(zoneCirclePrefab, new Vector3(0,0,0) , Quaternion.identity);
        zoneCircle.GetComponent<Circle>().CreateCircle(0,position);
        zoneCircle.transform.SetParent(GameObject.Find("ZoneCircles").transform);
        
        zoneCircle.GetComponent<Animation>().Play("TotemSpawnAnimation");

        UpdateZone();
    }

     public void CreatePermanentZone(Vector3 position){
        GameObject zoneCircle  = Instantiate(zoneCirclePrefab, new Vector3(0,0,0) , Quaternion.identity);
        zoneCircle.GetComponent<Circle>().CreateCircle(0,position);
        zoneCircle.transform.SetParent(GameObject.Find("ZoneCircles").transform);
        
        zoneCircle.GetComponent<Animation>().Play("PermanentZone");

        UpdateZone();
    }

     public GameObject CreateHeatShield(Vector3 position){
        GameObject zoneCircle  = Instantiate(zoneCirclePrefab, new Vector3(0,0,0) , Quaternion.identity);
        zoneCircle.GetComponent<Circle>().CreateCircle(0,position);
        zoneCircle.transform.SetParent(GameObject.Find("ZoneCircles").transform);
        
        zoneCircle.GetComponent<Animation>().Play("ZoneSpawnAnimation");
        
        UpdateZone();

        return zoneCircle;
    }

     public IEnumerator removeCircle( GameObject zoneCircle ){
        yield return new WaitForSeconds(2);

        DestroyImmediate(zoneCircle);
        UpdateZone();
    }

}
