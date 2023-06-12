using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveItem : MonoBehaviour
{

    [SerializeField] protected float useSpeed;
    
    /**
     * Uses the item
     */
    public abstract void Use();

    public float GetUseSpeed()
    {
        return this.useSpeed;
    }
    
}
