using System.Collections;
using System.Collections.Generic;
using Items.InventoryItems;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInput : MonoBehaviour
{
    public InputManager controls;

    public TestInteraction uiTest;
    public Vector2 mousePosition;
    public RadialMenu radialMenu;
    public HitScreen hit;
    
    void Awake()
    {
        controls = new InputManager();
        controls.Test.DepleteHealth.performed += _ => uiTest.TakeDamage(50);
        controls.Test.DepleteHunger.performed += _ => uiTest.GetHungryBy(10);
        controls.Test.DepleteShield.performed += _ => uiTest.ShieldDepletedBy(10);
        controls.Test.IncreaseHealth.performed += _ => uiTest.Heal(40);
        
        //Standard inventory while walking
        controls.Player.OpenStandardInventory.started += _ => radialMenu.createMenu(InventoryAccessController.inventoryAccessStates.inventory);
        controls.Player.OpenStandardInventory.canceled += _ => radialMenu.CloseMenu();

        controls.StandardInventory.UseItem.performed += _ => radialMenu.UseItem();
        controls.StandardInventory.DropItem.performed += _ => radialMenu.DropItem();

        //crafting inventory at the bench or oven
        controls.Player.OpenCraftingInventory.started += _ => radialMenu.createMenu(InventoryAccessController.inventoryAccessStates.craftingInventory);
        controls.Player.OpenCraftingInventory.canceled += _ => radialMenu.CloseMenu();
        
        controls.CraftingInventory.SelectItem.performed += _ => radialMenu.SelectItem();
        controls.CraftingInventory.UnselectItem.performed += _ => radialMenu.UnselectItem();

        controls.Test.Hit.performed += _ => hit.TweenColor();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        mousePosition = controls.Test.MousePosition.ReadValue<Vector2>();
    }
}
