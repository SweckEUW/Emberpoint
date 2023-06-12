using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTemple : MonoBehaviour
{

    //Controls the lasers of the laser temple puzzle

    [SerializeField] private float laserOnTime = 2f;
    [SerializeField] private float laserDownTime = 1.5f;
    [SerializeField] private float startingDistance = 40f; // determines at which distance to the player the puzzle is activated (puzzle is only active when player is near)
    
    private LaserSpawner[] spawnerArray;
    private Transform player;

    private float timeToSwitchLasers = 0f;
    private bool lasersActive = false;

    private void Awake()
    {
        spawnerArray = new LaserSpawner[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnerArray[i] = transform.GetChild(i).GetComponent<LaserSpawner>();
        }

        timeToSwitchLasers = 0f;

        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if((transform.position - player.position).magnitude < startingDistance)
        {
            if (timeToSwitchLasers <= 0)
            {
                if(lasersActive)
                {
                    DeactivateLaserSpawner();
                }
                else
                {
                    ActivateLaserSpawner();
                }
            }
            timeToSwitchLasers -= Time.deltaTime;
        }
        else
        {
            if (lasersActive)
            {
                DeactivateLaserSpawner();
            }
        }
    }

    /**
     * Activates all laserspawners
     */
    private void ActivateLaserSpawner()
    {
        foreach (LaserSpawner spawner in spawnerArray)
        {
            spawner.ActivateLaser();
        }
        timeToSwitchLasers = laserOnTime;
        lasersActive = true;
    }

    /**
     * Deactivates all laserspawners
     */
    private void DeactivateLaserSpawner()
    {
        foreach (LaserSpawner spawner in spawnerArray)
        {
            spawner.DeactivateLaser();
        }
        timeToSwitchLasers = laserDownTime;
        lasersActive = false;
    }
}
