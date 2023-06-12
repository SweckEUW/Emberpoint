using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{

    public Items.InventoryItems.InventoryItem ember;

    [SerializeField] private int spawnAmount = 10;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private float spawnHeight = 2f;
    private Vector3[] spawnPositions;

    private void Start()
    {
        spawnPositions = new Vector3[spawnAmount];
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPositions[i] = transform.position + (new Vector3(Mathf.Cos(2 * Mathf.PI / spawnAmount * i), 0f, Mathf.Sin(2 * Mathf.PI / spawnAmount * i)) * spawnRadius) + (Vector3.up * spawnHeight);
        }

    }

    public void SpawnEmbers()
    {
        foreach(Vector3 spawnpoint in spawnPositions)
        {
            var droppedItem =
                Instantiate(
                    ember.preFab,
                    spawnpoint,
                    Quaternion.identity);
            var dropItem = droppedItem.GetComponent<Items.DroppedItems.DroppedItem>();
            dropItem.item = ember;
            dropItem.itemAmount = 1;
            dropItem.Initiate();
        }
    }
}
