using System;
using Items.InventoryItems;
using UnityEngine;

namespace Items.DroppedItems
{
    [Serializable]
    public class DroppedItemAmount
    {
        public InventoryItem item;
        public int amount;
    }
}