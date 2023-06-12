using System;
using System.Collections.Generic;
using Items.InventoryItems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Items.Crafting
{
    public class Craftingmenu : MonoBehaviour
    {
        // the currently selected items
        private List<InventoryItem> selectedMaterials;
        // The items inside the inventory
        private List<InventoryItem> items;

        public Inventory inventory;

        
        // the max possible items that can be inside a inventory
        private byte maxInventorySize;

        // all possible recipes
        public List<CraftingRecipe> benchRecipes;
        public List<CraftingRecipe> ovenRecipes;
        // turns true if a craftable item has been found and inventory isnt full
        private bool crafteAble;
        
        public Button craftButton;

        public GameObject craftingInformation;
        
        public bool craftingMenuOpened;

        /**
         *  the itemname of a craftable that has been found (if any found at all)
         * can be either:
         * "YourItemName" or
         * "NotFound"
         */
        private string somethingToCraft;

        public RadialMenu radialMenu;
        
        public bool isBench;

        private void Awake()
        {
            selectedMaterials = GameObject.FindObjectOfType<Inventory>().selectedItems;
            items = GameObject.FindObjectOfType<Inventory>().items;
            maxInventorySize = GameObject.FindObjectOfType<Inventory>().maxInventorySize;
            craftButton.onClick.AddListener(() => CraftItem());
            foreach (var item in inventory.gameItems)
            {
                item.selectedAmount = 0;
            }
        }

        private void Update()
        {
            if(craftingMenuOpened) CheckForItemChange();
        }

        /**
         * To Change the image and text of the selected item
         */
        public void CheckForItemChange()
        {
            var itemName = ContainsCraftableItem(selectedMaterials);
            var item = inventory.gameItems.Find(item => item.itemName == itemName);

            // check if craftable item is still the same
            if (!inventory.selectedItemName.text.Equals(itemName) && !itemName.Equals("NotFound"))
            {
                Debug.Log(itemName);
                craftingInformation.SetActive(true);
                inventory.selectedImage.gameObject.SetActive(true);
                inventory.selectedImage.sprite = Resources.Load<Sprite>("UI/" + itemName);
                inventory.selectedItemName.text = itemName;
                inventory.selectedItemMaxStack.text = "Max Stack: " + item.maxStackableSize;
                inventory.selectedItemDescription.text = item.description;
            }

            if (itemName.Equals("NotFound") && radialMenu.isStandard == false)
            {
                inventory.selectedItemName.text = "";
                inventory.selectedItemMaxStack.text = "";
                inventory.selectedItemDescription.text = "";
            }
        }

        // Use as onclick to trigger a craft
        private void CraftItem()
        {
            somethingToCraft = ContainsCraftableItem(selectedMaterials);
            crafteAble = !somethingToCraft.Equals("NotFound") ? true : false;
            
            Debug.Log(somethingToCraft);
            if (crafteAble)
            {
                GameObject.Find("Sounds").GetComponents<AudioSource>()[11].Play();
                Craft(somethingToCraft,selectedMaterials.ToArray());
                inventory.selectedItemName.text = "";
                inventory.selectedImage.gameObject.SetActive(false);
                radialMenu.createMenu(InventoryAccessController.inventoryAccessStates.craftingInventory);
            }
        }
        
        /**
         * Craft a new Item and assign it to the Inventory
         * <param name="itemName"> the itemname of the item to craft (for the recipes) </param>
         * <param name="itemsToCraftNewItem">materials of the inventory that user has to craft the item</param>
         */
        public void Craft(string itemName,Array itemsToCraftNewItem)
        {
            var recipes = isBench ? benchRecipes : ovenRecipes;
            
            List<CraftingMaterial> requiredMaterial = new List<CraftingMaterial>();
            foreach (CraftingRecipe recipe in recipes)
            {
                if (recipe.itemName.Equals(itemName))
                {
                    requiredMaterial = recipe.craftingMaterial;
                    break;
                }
            }

            foreach (InventoryItem material in itemsToCraftNewItem)
            {
                foreach (CraftingMaterial mat in requiredMaterial)
                {
                    if (mat.item.itemName.Equals(material.itemName))
                    {
                        material.itemAmount -= mat.requiredAmount;
                        break;
                    }
                }
            }

            foreach (var selectedItem in selectedMaterials)
            {
                selectedItem.selectedAmount = 0;
            }

            var craftedItem = inventory.gameItems.Find(i => i.itemName.Equals(itemName));
            //        items.Add(craftedItem);
            if (craftedItem.itemAmount >= craftedItem.maxStackableSize || inventory.items.Count >= inventory.maxInventorySize && craftedItem.itemAmount == 0)
            {
                bool inInventory = !(craftedItem.itemAmount == 0);
                inventory.DropItemWhenCrafting(craftedItem,inInventory: inInventory);
            }
            else
            {
                inventory.AddItemToInventory(craftedItem);
            }
        }


        /**
         * Check for all recipes if any matches with the current selected materials
         * <returns> false if nothing found  or true + the itemName if something found</returns>
         * <param name="selectedMaterials">the selected Items inside the craftingmenu</param>
         */
        private string ContainsCraftableItem(List<InventoryItem> selectedMaterials)
        {
            var recipes = isBench ? benchRecipes : ovenRecipes;
            
            foreach (CraftingRecipe recipe in recipes)
            {
                if (HasCraftingCombination(recipe.itemName,recipe.craftingMaterial,selectedMaterials))
                {
                    return recipe.itemName;
                }
            }
            return "NotFound";
        }

        /**
         * Check if the selected materials match with one of the possible craftings
         * <returns>will return false if nothing found</returns>
         * <param name="recipeName">The name of the recipe inside <see cref="CraftingRecipes"/></param>
         * <param name="requiredMaterials">The materials of a recipe inside (Array) <see cref="CraftingRecipes"/></param>
         * <param name="selectedMaterials">the selected items inside the craftingmenu (Array)</param>
         */
        private bool HasCraftingCombination(string recipeName,List<CraftingMaterial> requiredMaterials,List<InventoryItem> selectedMaterials)
        {
            //escape method if amount of selected items doesnt match amount of required materials
            if (requiredMaterials.Count != selectedMaterials.Count) return false;
            
            int requiredItems = requiredMaterials.Count;
            // will be the same as requiredItems if all the materials required match with the selected materials
            int actualItems = 0;

            //look if the selected materials match with the required materials of the recipe
            for (int x = 0; x < requiredMaterials.Count;x++)
            {
                for (int y = 0; y < selectedMaterials.Count; y++)
                {
                    // have to be the same itemName and the selected material needs
                    //  to be the same amount or higher as the amountNeeded for this craft
                    if (requiredMaterials[x].item.itemName == selectedMaterials[y].itemName && 
                        selectedMaterials[y].selectedAmount == requiredMaterials[x].requiredAmount)
                    {
                        actualItems++;
                        break;
                    }
                }
            }
            return requiredItems == actualItems ? true : false;

        }
    }
}