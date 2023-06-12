using System;
using System.Collections;
using System.Collections.Generic;

using Items.InventoryItems;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Object = System.Object;

public class RadialMenu : MonoBehaviour
{
    public Vector2 normalizedMousePosition;
    public PlayerInput playerInput;

    public float currentAngle;
    public int selection;
    public int previousSelection;
    public float tempSelection;

    [Range(0, 16)]
    public int itemAmount = 8;
  
    public List<GameObject> menuItems = new List<GameObject>();
    public GameObject menuItemPrefab;

    public MenuItem previousMenuItem;
    public MenuItem currentMenuItem;

    public Inventory inventory;

    public ButtonHandler buttonHandler;

    public GameObject craftButton;
    public GameObject craftingInformation;
    private bool waitingForData;

    readonly float[] containerScales = {1, 1, 1, 1, 1, 1, 1, 1, 1, 0.85f, 0.8f, 0.8f, 0.8f, 0.75f, 0.7f, 0.65f};
    readonly float[] containerRotations = {-155, -65, -33, -18, -14, -6.5f, -0.5f, 1.5f, 4.5f, 6.5f, 7.5f, 8.5f, 9.5f, 10.5f, 12f, 13f};
    readonly float [] degrees = {0, -90, 225, 180, 150, 150, 145, 135, 135, 130, 125, 120, 120, 115, 110, 110};
    private float degree;

    public bool isStandard = false;

    void Awake()
    {
        playerInput.GetInputControls().CraftingInventory.Disable();
        playerInput.GetInputControls().StandardInventory.Disable();
        craftButton.SetActive(false);
        craftingInformation.SetActive(false);
    }

    void Update()
    {
        normalizedMousePosition = new Vector2(Mouse.current.position.ReadValue().x - Screen.width / 2, Mouse.current.position.ReadValue().y - Screen.height / 2);
        currentAngle = Mathf.Atan2(normalizedMousePosition.y, normalizedMousePosition.x) * Mathf.Rad2Deg;

        currentAngle = (currentAngle + 360f) % 360f;
        tempSelection = currentAngle / (360f / itemAmount);
        
        selection = (int) tempSelection;
        menuItems[selection].GetComponent<MenuItem>().Select();

        if (itemAmount <= 0)
        {
            return;
        }

        if (itemAmount > 1 && selection != previousSelection)
        {
            previousMenuItem = menuItems[previousSelection].GetComponent<MenuItem>();
            if (previousMenuItem.pressed == false)
            {
                previousMenuItem.Deselect();
            }
            else
            {
                previousMenuItem.Press();
            }
            previousSelection = selection;

            currentMenuItem = menuItems[selection].GetComponent<MenuItem>();
            currentMenuItem.Select();

            if (isStandard)
            {
                inventory.selectedImage.sprite = Resources.Load<Sprite>("UI/" + currentMenuItem.baseItem.itemName);
                inventory.selectedItemName.text = currentMenuItem.baseItem.itemName;
                inventory.selectedItemMaxStack.text = "Max Stack: " + currentMenuItem.baseItem.maxStackableSize;
                inventory.selectedItemDescription.text = currentMenuItem.baseItem.description;
            }
        }
        else if (itemAmount == 1)
        {
            currentMenuItem = menuItems[0].GetComponent<MenuItem>();
            currentMenuItem.Select();
        }
    }
    public void createMenu (InventoryAccessController.inventoryAccessStates state)
    {
        if (inventory.items.Count <= 0)
        {
            return;
        }
        
        CheckInventoryState(state);

        playerInput.GetInputControls().Player.Act.Disable();
        
        if (menuItems.Count > 0)
        {
            ResetMenuItems();
        }
        foreach (var item in inventory.items)
        {
            item.selectedAmount = 0;
        }
        
        if (GameObject.Find("CurrentInventory") == null)
        {
            GameObject currentInventory = new GameObject();
            currentInventory.name = "CurrentInventory";
            currentInventory.transform.SetParent(GameObject.Find("RadialMenu").transform);
        }
        
        enabled = true;
        itemAmount = inventory.items.Count;
        degree = degrees[itemAmount - 1];

        GenerateMenuItems();
    }

    public void CloseMenu()
    {
        playerInput.GetInputControls().Player.Act.Enable();
        if (craftButton.active)
        {
            craftButton.SetActive(false);
        }

        if (craftingInformation.active)
        {
            craftingInformation.SetActive(false);
        }
        previousSelection = 0;
        playerInput.GetInputControls().CraftingInventory.Disable();
        playerInput.GetInputControls().StandardInventory.Disable();
        enabled = false;

        playerInput.transform.GetComponent<PlayerBuildingMode>().SetPreviewActive(true);
        Destroy(GameObject.Find("CurrentInventory"));
    }
    
    public void UseItem()
    {
        
        currentMenuItem.Press();
        inventory.itemToUse = inventory.items[selection];
        var player = GameObject.FindObjectOfType<Player.Player>().GetComponent<Player.Player>();
        player.Equip();
        CloseMenu();
    }
    
    public void DropItem()
    {
        inventory.DropItem(inventory.items[selection]);
        var equippedItem = FindObjectOfType<Player.Player>().equippedItem;
        if(equippedItem != null) Destroy(FindObjectOfType<Player.Player>().equippedItem);
            createMenu(InventoryAccessController.inventoryAccessStates.inventory);
        previousSelection = 0;
        if (inventory.items.Count <= 0)
        {
            CloseMenu();
        }
    }
    
    public void SelectItem()
    {
        if (!buttonHandler.mouseIsOverButton)
        {
            if (currentMenuItem.pressed)
        	{
                //inventory.selectedItems.Find(x => x == inventory.items[selection]).selectedAmount += 1;

                // check if selectedItem.selectedAmount is not becoming higher than the inventory actually has of the item
                // or if the selected item even has something inside the inventory
                var selectedItem =  inventory.selectedItems.Find(x => x == inventory.items[selection]);
                selectedItem.selectedAmount = selectedItem.itemAmount == selectedItem.selectedAmount || selectedItem.itemAmount == 0
                    ? selectedItem.selectedAmount
                    : ++selectedItem.selectedAmount;
                //inventory.ChangeVisualSelectedItem(selectedItem.itemName, selectedItem.maxStackableSize, selectedItem.description);
                if (inventory.inventoryOpened)
                {
                    //inventory.ChangeVisualSelectedItem(selectedItem.itemName, selectedItem.maxStackableSize, selectedItem.description);
                    inventory.selectedImage.gameObject.SetActive(true);
                }
                
            }
        	if (!currentMenuItem.pressed)
            {
                currentMenuItem.Press();
                inventory.selectedItems.Add(inventory.items[selection]);
                
                
                // check if selectedItem.selectedAmount is not becoming higher than the inventory actually has of the item
                // or if the selected item even has something inside the inventory
                var selectedItem =  inventory.selectedItems.Find(x => x == inventory.items[selection]);
                selectedItem.selectedAmount = selectedItem.itemAmount == selectedItem.selectedAmount || selectedItem.itemAmount == 0
                    ? selectedItem.selectedAmount
                    : ++selectedItem.selectedAmount;
                if (inventory.inventoryOpened)
                {
                    //inventory.ChangeVisualSelectedItem(selectedItem.itemName, selectedItem.maxStackableSize, selectedItem.description);
                    inventory.selectedImage.gameObject.SetActive(true);
                }
            }
        }
    }
    
    public void UnselectItem()
    {
        if (currentMenuItem.pressed)
        {
            var selectedItem =  inventory.selectedItems.Find(x => x == inventory.items[selection]);
            selectedItem.selectedAmount = 0;
            currentMenuItem.Deselect();
            inventory.selectedItems.Remove(inventory.items[selection]);
            currentMenuItem.pressed = false;
            inventory.selectedItemName.text = "";
            inventory.selectedImage.gameObject.SetActive(false);
        }
    }

    private void CheckInventoryState(InventoryAccessController.inventoryAccessStates state)
    {
        switch (state)
        {
            case InventoryAccessController.inventoryAccessStates.inventory:
                playerInput.GetInputControls().CraftingInventory.Disable();
                playerInput.GetInputControls().StandardInventory.Enable();
                craftingInformation.SetActive(true);
                isStandard = true;
                break;
            case InventoryAccessController.inventoryAccessStates.craftingInventory:
                craftButton.SetActive(true);
                playerInput.GetInputControls().CraftingInventory.Enable();
                playerInput.GetInputControls().StandardInventory.Disable();
                isStandard = false;
                break;
        }
    }

    private void ResetMenuItems()
    {
        foreach(var obj in menuItems)
        {
            Destroy(obj);
        }

        menuItems.Clear();
        inventory.selectedItems.Clear();
    }

    private void GenerateMenuItems()
    {
        for (int i = 0; i < itemAmount; i++)
        {
            Vector3 position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Quaternion rotation = Quaternion.Euler(0, 0, degree);
            menuItems.Add(Instantiate(menuItemPrefab, position, rotation) as GameObject);

            menuItems[i].GetComponent<MenuItem>().baseItem = inventory.items[i];

            Image backgroundComponent = menuItems[i].transform.GetChild(0).GetComponent<Image>();
            RectTransform itemInfoRotateGroup = menuItems[i].transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
            RectTransform itemInfoContainer = menuItems[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
            
            float offset = itemAmount == 1 ? 0 : 0.003f;
            
            backgroundComponent.fillAmount = ((float) 1 / itemAmount) - offset;
            itemInfoRotateGroup.eulerAngles += new Vector3(0, 0, containerRotations[itemAmount - 1]);
            itemInfoContainer.localScale = new Vector3(containerScales[itemAmount - 1],containerScales[itemAmount - 1], containerScales[itemAmount - 1]);
            
            if (isStandard)
            {
                menuItems[i].GetComponent<MenuItem>().showSelected = false;
            }
            else
            {
                menuItems[i].GetComponent<MenuItem>().showSelected = true;
            }

            menuItems[i].transform.SetParent(GameObject.Find("CurrentInventory").transform);
            degree += (360f / itemAmount);
        }
    }
}
