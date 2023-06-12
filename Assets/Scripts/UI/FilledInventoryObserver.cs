using System.Collections;
using System.Collections.Generic;
using Items.InventoryItems;
using UnityEngine;

public class FilledInventoryObserver : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject content;

    void Start()
    {
        content.SetActive(false);
    }

    void Update()
    {
        if (inventory.items.Count >= inventory.maxInventorySize)
        {
            if (content.active == false)
            {
                content.SetActive(true);
            }
        }
        else
        {
            content.SetActive(false);
        }
    }
}
