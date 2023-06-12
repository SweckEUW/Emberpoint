using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items.ActiveItems;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //Input
    private InputManager inputControls;

    //Playerscripts
    private Player.Player player;
    private PlayerMovement p_Movement;
    private PlayerBuildingMode p_BuildMode;
    private PlayerCombat p_Combat;
    private Camera mainCam;
    public float placeableRadiusDetection;
    public LayerMask placeableLayer;
    
    //UI Script
    public RadialMenu radialMenu;

    /**
     * Initialize inputs
     */
    private void Awake()
    {
        player = GetComponent<Player.Player>();
        p_Movement = GetComponent<PlayerMovement>();
        p_Combat = GetComponent<PlayerCombat>();
        p_BuildMode = GetComponent<PlayerBuildingMode>();

        inputControls = new InputManager();

        mainCam = GameObject.Find("Isometric Camera").GetComponent<Camera>();

        //MovementInputs
        inputControls.Player.Movement.performed += direction => p_Movement.ChangeHorizontalMovement(direction.ReadValue<Vector2>());
        inputControls.Player.Dash.performed += context => p_Movement.Dash();

        //Interaction
        inputControls.Player.Act.performed += context => player.Act();
        inputControls.Player.Interact.performed += context => SearchForInteraction();

        //Prototyp
        inputControls.Player.Equip1.performed += context => player.EquipWeapon();

        //Inventory
        //Standard inventory while walking
        inputControls.Player.OpenStandardInventory.started += _ => OpenRadialMenu();
        inputControls.Player.OpenStandardInventory.canceled += _ => radialMenu.CloseMenu();

        inputControls.StandardInventory.UseItem.performed += _ => radialMenu.UseItem();
        inputControls.StandardInventory.DropItem.performed += _ => radialMenu.DropItem();

        //crafting inventory at the bench or oven
       // inputControls.Player.OpenCraftingInventory.started += _ => radialMenu.createMenu(InventoryAccessController.inventoryAccessStates.craftingInventory);
        inputControls.Player.OpenCraftingInventory.started += _ => SearchForInteraction();
        inputControls.Player.OpenCraftingInventory.canceled += _ => radialMenu.CloseMenu();
        
        inputControls.CraftingInventory.SelectItem.performed += _ => radialMenu.SelectItem();
        inputControls.CraftingInventory.UnselectItem.performed += _ => radialMenu.UnselectItem();

    }
    

    /**
     * Searches the nearest interactable
     */
    private void SearchForInteraction()
    {
        if (player.isDead) return;
        Collider[] colliderArray = Physics.OverlapSphere(player.transform.position,placeableRadiusDetection,placeableLayer);
        Transform nearestPlaceable = colliderArray.Any() ? colliderArray[0].transform : null;
        if (nearestPlaceable is null) return;
        
        foreach (var collider in colliderArray)
        {
            if (Vector3.Distance(player.transform.position,collider.transform.position) <
                Vector3.Distance(player.transform.position,nearestPlaceable.position))
            {
                nearestPlaceable = collider.transform;
            }
        }

        nearestPlaceable.GetComponent<ActiveItemPlaceables>().InteractWithPlaceable();
        p_BuildMode.SetPreviewActive(false);
    }

    private void OpenRadialMenu()
    {
        if (player.isDead) return;

        radialMenu.createMenu(InventoryAccessController.inventoryAccessStates.inventory);
        p_BuildMode.SetPreviewActive(false);
    }

    /**
     * Determines player rotation depending on mouse position
     */
    private void DetermineMousePosition()
    {
        if (player.isDead) return;
        Ray ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        float distance;
        playerPlane.Raycast(ray, out distance);
        Vector3 target = ray.GetPoint(distance);
        Vector3 direction = target - transform.position;
        float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion rotationEuler = Quaternion.Euler(0, rotation, 0);
        p_Movement.RotateBodyToMouse(rotationEuler);
    }

    private void Update()
    {
        DetermineMousePosition();
    }

    private void OnEnable()
    {
        inputControls.Enable();
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }

    public InputManager GetInputControls()
    {
        return inputControls;
    }
}
