using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCircleVertex
{   
    public Vector3 vertex;

    private bool looseEnd = false;

    public ZoneCircleVertex(Vector3 vertex){
        this.vertex = vertex;
    }
    
    public void setLooseEnd(bool looseEnd){
        this.looseEnd = looseEnd;
    }

    public bool isLooseEnd(){
        return looseEnd;
    }
}
