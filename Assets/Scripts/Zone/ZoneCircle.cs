using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCircle
{   
    private int resolution = 200;

    private float radius;

    private Vector3 position;

    private Vector3[] points;

    public ZoneCircle(float radius, Vector3 position){
        this.radius = radius;
        this.position = position;
        CreatePoints();
    }
    
    private void CreatePoints(){
        points = new Vector3[resolution+1];

        float x;
        float y = position.y;
        float z;
        float angle = 20f;
       
        for (int i = 0; i < (resolution + 1); i++){
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius + position.x;
            z = Mathf.Cos (Mathf.Deg2Rad * angle) * radius + position.z;
            
            points[i] = new Vector3(x,y,z);
            angle += (360f / resolution);
        }
    }

    public void UpdatePosition(Vector3 position){
        this.position = position;
        CreatePoints();
    }

    public void UpdateRadius(float radius){
        this.radius = radius;
        CreatePoints();
    }

    public float getRadius(){
        return radius;
    }

    public Vector3 getPosition(){
        return position;
    }
    
    public Vector3[] getPoints(){
        return points;
    }

}
