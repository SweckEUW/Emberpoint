using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleLaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.GetComponent<Player.Player>().TakeDamage(9999);
        }
    }
}
