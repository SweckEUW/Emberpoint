using System.Collections;
using System.Collections.Generic;
using Items.InventoryItems;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransferMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
{
    public TransferInfo baseInfo;

    private string itemName;
    private byte itemAmount;
    public byte selectedAmount;

    public Color baseColor = new Color(223, 26, 100);
    public Color hoverColor;
    public Color selectionColor;
    public Color wholeStackColor;

    public Image background;
    public Image itemIcon;
    public TMP_Text amount;
    
    public bool isFullySelected;
    public bool isPartlySelected;
    
    void Start()
    {
        itemName = baseInfo.itemReference.itemName;
        itemAmount = baseInfo.itemAmount;
        
        background.color = baseColor;

        Sprite loadedSprite = Resources.Load<Sprite>("UI/" + itemName);
        itemIcon.sprite = loadedSprite;
    }

    private void Hover()
    {
        background.color = hoverColor;
    }

    private void StopHover()
    {
        if (!isFullySelected && !isPartlySelected)
        {
            background.color = baseColor;
        }
    }

    private void ChooseSelection()
    {
        if (baseInfo.itemAmount <= 0 || baseInfo.itemAmount == selectedAmount)
        {
            return;
        }
        ++selectedAmount;
        
        background.color = selectionColor;
        isPartlySelected = true;
        
        if (selectedAmount == baseInfo.itemAmount)
        {
            ResetSelection();
            ChooseWholeStack();
        }
    }
    
    private void ChooseWholeStack()
    {
        if (baseInfo.itemAmount <= 0)
        {
            return;
        }
        background.color = wholeStackColor;
        selectedAmount = itemAmount;
        isPartlySelected = false;
        isFullySelected = true;
    }

    public void ResetSelection()
    {
        background.color = baseColor;
        selectedAmount = 0;
        isPartlySelected = false;
        isFullySelected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ChooseSelection();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            ChooseWholeStack();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            ResetSelection();
        }
    }
    
    public void Update()
    {
        amount.text = selectedAmount + "/" + itemAmount;
    }
}
