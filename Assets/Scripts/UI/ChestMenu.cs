using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Items.InventoryItems;
using UnityEngine;
using UnityEngine.UI;

public class ChestMenu : MonoBehaviour
{
    [SerializeField] private float gap;
    [SerializeField] private GameObject chestMenuItem;
    [SerializeField] private GameObject chestMenuPlaceholder;
    public List<GameObject> chestMenuItems = new List<GameObject>();
    private RectTransform chestMenuItemRectTransform;
    private float chestMenuItemWidth;

    private float menuWidth;
    private Vector2 startPoint;
    private Vector2 xSpacingVector;
    private Vector2 ySpacingVector;

    private List<Vector2> positions = new List<Vector2>();
    
    [SerializeField] private float itemCount = 6;

    void Awake()
    {
        chestMenuItemRectTransform = chestMenuItem.GetComponent<RectTransform>();
        chestMenuItemWidth = chestMenuItemRectTransform.rect.width * chestMenuItemRectTransform.localScale.x;

        menuWidth = 3 * gap + 4 * chestMenuItemWidth;
        xSpacingVector = new Vector2(gap + chestMenuItemWidth, 0);
        ySpacingVector = new Vector2(0, gap + chestMenuItemWidth);

        positions = new List<Vector2>();
    }
    
    public void createPlaceholders(string hierarchyObject, Vector2 menuCenter)
    {
        startPoint = new Vector2(menuCenter.x - menuWidth / 2, menuCenter.y + menuWidth / 2); 
        
        if (chestMenuItems.Count > 0)
        {
            foreach(var obj in chestMenuItems)
            {
                Destroy(obj);
            }
            chestMenuItems.Clear();
        }
        
        for (float i = 0; i < 4; i++)
        {
            if (i > 0)
            {
                startPoint -= ySpacingVector;
            }

            for (float j = 0; j < 4; j++)
            {
                Vector2 position = startPoint + j * xSpacingVector;
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                GameObject chestMenuPlaceholderInstance =
                    Instantiate(chestMenuPlaceholder, position, rotation) as GameObject;
                chestMenuItems.Add(chestMenuPlaceholderInstance);

                chestMenuPlaceholderInstance.transform.SetParent(GameObject.Find(hierarchyObject).transform);
                positions.Add(position);
            }
        }
    }

    public void createMenu(string hierarchyObject, Vector2 menuCenter, Inventory inventory)
    {

        itemCount = inventory.items.Count;
        createPlaceholders(hierarchyObject, menuCenter);
        
        for (int i = 0; i < itemCount; i++)
        {
            Vector2 position = positions[i];
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            
            Destroy(chestMenuItems[i]);
            chestMenuItems.RemoveAt(i);
            
            GameObject chestMenuItemInstance = Instantiate(chestMenuItem, position, rotation) as GameObject;
            chestMenuItemInstance.GetComponent<ChestMenuItem>().baseItem = inventory.items[i];
            
            
            chestMenuItems.Insert(i, chestMenuItemInstance);
            chestMenuItemInstance.transform.SetParent(GameObject.Find(hierarchyObject).transform);
        }
    }
    
    public void closeMenu()
    {
        foreach (var chestMenuItem in chestMenuItems )
        {
            Destroy(chestMenuItem);
        }

        chestMenuItems.Clear();
    }
}
