using System;
using System.Collections.Generic;
using Items.ActiveItems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items.Placeables
{
    public class PlaceableItemObserver : MonoBehaviour
    {
        public List<ActiveItemPlaceables> placeableItems;

        private Player.Player player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
        }

        /**
         * Checks all item that has been placed in the world and if user wants to interact with them
         */
        public void Update()
        {
            foreach (var placeableItem in placeableItems)
            {
                // check if player is near placeable and presses  right button
                if (Mouse.current.rightButton.wasPressedThisFrame &&
                    Vector3.Distance(player.transform.position, placeableItem.placedItem.transform.position) <= 5
                    && !placeableItem.uiInteractable)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                    RaycastHit hit = new RaycastHit();
                    if (placeableItem.placedItem.GetComponent<BoxCollider>().Raycast(ray, out hit, 1000))
                    {
                        placeableItem.InteractWithPlaceable();
                    }
                }
                // check if placeable is a item that will be triggerd by ui ( crafting,inventory...)
                else if ( placeableItem.uiInteractable &&
                         Vector3.Distance(player.transform.position, placeableItem.placedItem.transform.position) <= 5)
                {
                    Debug.Log("uiinteractable"); 
                    placeableItem.InteractWithPlaceable();
                    
                }
            }
        }
    }
}