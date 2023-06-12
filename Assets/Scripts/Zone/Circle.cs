using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    
    public float radius;
    public Vector3 position;
    public ZoneCircle circle;
    public GameObject zoneUpdater;

    public void CreateCircle(float radius, Vector3 position){
        circle = new ZoneCircle(radius, position);
        this.radius = radius;
        this.position = position;
    }

    private void Update() {
        if(radius != circle.getRadius())
            UpdateRadius(radius);

        if(position != circle.getPosition())
            UpdatePosition(position);
    }

    void UpdateRadius(float radius){
        circle.UpdateRadius(radius);
        zoneUpdater.GetComponent<ZoneUpdater>().UpdateZone();
    }

    void UpdatePosition(Vector3 position){
        circle.UpdatePosition(position);
        zoneUpdater.GetComponent<ZoneUpdater>().UpdateZone();
    }
}
