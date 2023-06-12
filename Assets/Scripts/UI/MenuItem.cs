using System;
using System.Collections;
using System.Collections.Generic;
using Items.InventoryItems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.Mathematics;
using UnityEditor;

public class MenuItem : MonoBehaviour
{
    public InventoryItem baseItem;

    public string itemName;
    public byte itemAmount;
    public byte selectedAmount;

    public Color baseColor;
    public Color hoverColor;
    public Color pressedColor;
    public Image background;

    public GameObject itemInfoContainer;
    
    public Image itemIcon;
    public TMP_Text amount;
    
    public bool pressed;

    public bool showSelected;

    void Start()
    {
         itemName = baseItem.itemName;
         itemAmount = baseItem.itemAmount;
         selectedAmount = baseItem.selectedAmount;
        
         background.color = baseColor;
         itemInfoContainer.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);

         Sprite loadedSprite = Resources.Load<Sprite>("UI/" + itemName);
         itemIcon.sprite = loadedSprite;
    }

    public void Select()
    {
        background.color = hoverColor;
    }

    public void Deselect()
    {
        background.color = baseColor;
    }
    
    public void Press()
    {
        background.color = pressedColor;
        pressed = true;
    }
    
    public void Update()
    {
        selectedAmount = baseItem.selectedAmount;
        if (showSelected)
        {
            
            amount.text = selectedAmount + "/" + itemAmount;
        }
        else
        {
            amount.text = "" + itemAmount;
        }
    }
}
