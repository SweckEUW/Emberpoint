using System.Collections;
using System.Collections.Generic;
using Items.ActiveItems;
using Items.Crafting;
using Items.Placeables;
using UnityEngine;

public class ActiveItemCraftingObject : ActiveItemPlaceables
{
    public RadialMenu radialMenu;
    public Craftingmenu craftingMenu;

    public override void InteractWithPlaceable()
    {
        radialMenu = Resources.FindObjectsOfTypeAll<RadialMenu>()[0].GetComponent<RadialMenu>();
        craftingMenu = Resources.FindObjectsOfTypeAll<Craftingmenu>()[0].GetComponent<Craftingmenu>();

        if (item.itemName == "Bench")
        {
            craftingMenu.isBench = true;
        }
        else
        {
            craftingMenu.isBench = false;
        }

        radialMenu.createMenu(InventoryAccessController.inventoryAccessStates.craftingInventory);
    }
}
