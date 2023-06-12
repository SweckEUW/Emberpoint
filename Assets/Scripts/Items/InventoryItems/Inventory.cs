using System;
using System.Collections.Generic;
using Items.DroppedItems;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

namespace Items.InventoryItems
{
    /*
     * Describes an item inside the inventory or crafting field
     */
    public class Inventory : MonoBehaviour
    {
        public List<InventoryItem> gameItems;
        
        public List<InventoryItem> items;

        public List<InventoryItem> selectedItems;

        public List<StartupItem> startupItems;
        
        public InventoryItem itemToUse;

        public byte maxInventorySize = 16;

        public bool inventoryOpened;

        [SerializeField]
        public Image selectedImage;
        [SerializeField]
        public Text selectedItemName;

        public Text selectedItemMaxStack;
        
        public Text selectedItemDescription;

        public Player.Player player;

        private List<InventoryItem> itemsToRemoveFromInventory = new List<InventoryItem>();

        private PlayerBuildingMode p_BuildingMode;
        
        public void Awake()
        {   
            foreach (InventoryItem item in gameItems)
            {
                item.itemAmount = 0;
            } 
            p_BuildingMode = GameObject.Find("Player").GetComponent<PlayerBuildingMode>();
            foreach (var item in startupItems)
            {
                item.item.itemAmount = (byte)item.itemAmount;
                items.Add(item.item);
            }
        }
        
        public dynamic FindObjectByItemName(string itemName)
        {
            
            foreach (var item in items)
            {
                if (item.itemName.Equals(itemName))
                    return item;
            }
            return false;
        }

        private void Update()
        {
            if (!player.isDead)
            {
                foreach (InventoryItem item in items)
                {
                    if (item.itemAmount == 0) itemsToRemoveFromInventory.Add(item);
                }

                foreach (InventoryItem item in itemsToRemoveFromInventory)
                {
                    RemoveItem(item);
                }
                itemsToRemoveFromInventory.Clear();
            }
        }

        public void AddItemToInventory(InventoryItem inventoryItem,int itemAmountToAdd = 1)
        {
            if (IsInventoryFull()) return;
            bool hasAlreadyItem = false;
            foreach (var item in items)
                {
                    // if itemtype is already in inventory
                    // then add the itemamount to the item that is already inside the inventory 
                    if (item.itemName.Equals(inventoryItem.itemName))
                    {
                        for (int x = 0; x < itemAmountToAdd; x++)
                        {
                            
                        }
                        // short itemToAddToInvetory = Math.Abs(inventoryItem.itemAmount);
                        short itemInsideTheInventory = Math.Abs(item.itemAmount);
                        
                        item.itemAmount = (itemInsideTheInventory + itemAmountToAdd) >= item.maxStackableSize 
                            ? item.maxStackableSize : (byte)(itemInsideTheInventory + itemAmountToAdd);
                        hasAlreadyItem = true;
                        break;
                    }
                }
                // if its not already inside then inventory and inventory is not full then just add it to the list
                if (!hasAlreadyItem && !IsInventoryFull())
                {
                    items.Add(inventoryItem);
                    inventoryItem.itemAmount = (byte)(inventoryItem.itemAmount + itemAmountToAdd);
                }
        }

        public void RemoveItem(InventoryItem item)
        {
            var itemToDrop = items.Find(i => i.itemName.Equals(item.itemName));
            items.Remove(itemToDrop);
            p_BuildingMode.Reset();
        }
        
        /**
         * Remove Item from Inventory and reset Stats of Item
         */
        public void DropItem(InventoryItem item)
        {
            var itemToDrop = items.Find(i => i.itemName.Equals(item.itemName));
            
            var droppedItem = 
                Instantiate(
                    itemToDrop.preFab,
                    player.gameObject.transform.position + new Vector3(Random.Range(-1,1) * 3,2,Random.Range(-1,1) * 3),
                    Quaternion.identity);

            var dropItem = droppedItem.GetComponent<DroppedItem>();
            dropItem.item = itemToDrop;
            dropItem.itemAmount = item.itemAmount;
            dropItem.Initiate();
            
            
            itemToDrop.itemAmount = 0;
            itemToDrop.selectedAmount = 0;
            items.Remove(itemToDrop);

            p_BuildingMode.Reset();
        }

        /**
         * Drops Item that has been crafted due to full stack
         */
        public void DropItemWhenCrafting(InventoryItem item,bool inInventory = false)
        {
            
            var itemToDrop = inInventory 
                ? items.Find(i => i.itemName.Equals(item.itemName))
                : gameItems.Find(i => i.itemName.Equals(item.itemName));

            var droppedItem = 
                Instantiate(
                    itemToDrop.preFab,
                    player.gameObject.transform.position + new Vector3(Random.Range(-1,1) * 3,2,Random.Range(-1,1) * 3),
                    Quaternion.identity);

            var dropItem = droppedItem.GetComponent<DroppedItem>();
            dropItem.item = itemToDrop;
            dropItem.itemAmount = 1;
            dropItem.Initiate();
            p_BuildingMode.Reset();
        }
        
        /**
         * Remove Item from Inventory and reset Stats of Item
         */
        public void DropItem(InventoryItem item,Vector3 itemPosition)
        {
            var itemToDrop = items.Find(i => i.itemName.Equals(item.itemName));
            itemToDrop.itemAmount = 0;
            itemToDrop.selectedAmount = 0;
            items.Remove(itemToDrop);
        }
        
        /**
         * To Change the image and text of the selected item
         */
        public void ChangeVisualSelectedItem(string itemName, byte maxStack, string description)
        {
            selectedImage.sprite = Resources.Load<Sprite>("Resources/UI/" +itemName); 
            selectedItemName.text = itemName;
            selectedItemMaxStack.text = "Max Stack: " + maxStack;
            selectedItemDescription.text = description;
        }
        



        public bool IsInventoryFull(InventoryItem item = null)
        {
            if (item != null)
            {
                // inventory && itemstack full
                if (items.Count >= maxInventorySize && item.itemAmount >= item.maxStackableSize )
                {
                    return true;
                }
                // itemstack of the certain item full
                if (item.itemAmount >= item.maxStackableSize)
                {
                    return true;
                }
                // full inventory but item not picked up yet
                if (items.Count >= maxInventorySize && item.itemAmount == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetDifferenceToMaxStack(InventoryItem item)
        {
            return item.maxStackableSize - item.itemAmount;
        }
    }

    

    [Serializable]
    public class StartupItem
    {
        public int itemAmount;
        public InventoryItem item;
    }
}