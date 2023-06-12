using System.Collections;
using System.Collections.Generic;
using Items.InventoryItems;
using UnityEngine;

[System.Serializable]
public class TransferInfo
{
    public byte itemAmount;
    public InventoryItem itemReference;

    public TransferInfo(byte amount, InventoryItem reference)
    {
        itemAmount = amount;
        itemReference = reference;
    }
}
