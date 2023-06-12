using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    private Transform spawnPoint;

    private GameObject laserGO;

    private void Awake()
    {
        spawnPoint = transform.GetChild(0);

        laserGO = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        laserGO.SetActive(false);
    }

    public void ActivateLaser()
    {
        laserGO.SetActive(true);
    }

    public void DeactivateLaser()
    {
        laserGO.SetActive(false);
    }


}
