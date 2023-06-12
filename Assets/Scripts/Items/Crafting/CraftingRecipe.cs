using System;
using System.Collections.Generic;
using Items.InventoryItems;
using UnityEngine;

namespace Items.Crafting
{
    
    
    /**
     * Describes a recipe of an item
     */
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe", order = 2)]
    public class CraftingRecipe : ScriptableObject
    {
        //name of item to craft
        public string itemName;
        public List<CraftingMaterial> craftingMaterial;
    }
    
    /**
     * Lists the items and the required amount of the items to make a CraftingRecipe
     */
    [Serializable]
    public class CraftingMaterial
    {
        public InventoryItem item;
        public byte requiredAmount;
    }
}