using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            other.GetComponent<Player.Player>().TakeDamage(999999);
        }
        else if (other.gameObject.layer == 6)
        {
            other.GetComponent<Enemy>().TakeDamage(999999);
        }

    }
}
