using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Items.ActiveItems;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Items.InventoryItems
{
    /*
     * Describes an item inside the inventory or crafting field
     */
    [CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item", order = 1)]
    public class InventoryItem : ScriptableObject
    {
        
        public string itemName;
        // maximum amount of how many items of the same type can be stacked
        public byte maxStackableSize;

        // current amount of item of the same type that the player has
        public byte itemAmount;

        // the amount the user has clicked on a item in the radialmenu
        public byte selectedAmount;
        
        public GameObject preFab;
        
        public string description;
    }
}