using System;
using System.Linq;
using Items.InventoryItems;
using Items.Placeables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace Items.ActiveItems
{
    public class ActiveItemPlaceables : ActiveItem
    {
        public GameObject preFab;
        public GameObject buildPreviewPrefab;
        public InventoryItem item;

        // Placeable Object inside the Game. Will be instantiated once player has set a Position
        public GameObject placedItem;

        private InventoryItem selectedItemToPlace;
        private GameObject player;
        protected Inventory inventory;
        public bool uiInteractable;
        public LayerMask placeableLayer;
        public Vector3 colliderExtens;
        public LayerMask terrainLayer;


        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
            selectedItemToPlace = inventory.itemToUse;
        }


        /**
         * place placeable in world if user presses mouse button
         * and observe it for interaction inside PlaceableItemObserver
         */
        public override void Use()
        {
            if (item.itemAmount == 0) return;
            player = GameObject.FindGameObjectWithTag("Player");
            Transform previewObject = player.GetComponent<PlayerBuildingMode>().CheckPlaceable();
            if (previewObject != null)
            {
                item.itemAmount--;
                if(item.itemAmount == 0) Destroy(FindObjectOfType<Player.Player>().equippedItem);
                placedItem = GameObject.Instantiate(preFab, previewObject.position, previewObject.rotation);
                BoxCollider collider = placedItem.GetComponent<BoxCollider>();
                if(collider == null)
                {
                    collider = placedItem.AddComponent<BoxCollider>();
                }
                placedItem.layer = 11;
                NavMeshObstacle obstacle = placedItem.AddComponent<NavMeshObstacle>();
                obstacle.center = collider.center;
                obstacle.size = collider.size;
                Placed();
                player.GetComponent<PlayerItemAction>().animator.SetTrigger("ItemPlaced");
            }

            /*if (CheckForPlacingPlaceable())
            {
                GameObject.FindObjectOfType<Inventory>()
                    .GetComponent<PlaceableItemObserver>()
                    .placeableItems
                    .Add(this);
            }*/
        }
        
        /**
        * Checks for collision with floor and
        * instantiates placeable object
        */
        protected bool CheckForPlacingPlaceable()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit = new RaycastHit();
            
            if (Physics.Raycast(ray,out hit, Mathf.Infinity, terrainLayer))
            {
                if (Vector3.Distance(hit.point,GameObject.FindGameObjectWithTag("Player").transform.position) > 10)
                {
                    return false;
                }
                Vector3 positionToPlace = hit.point;
                var observer = GameObject.FindObjectOfType<Inventory>()
                    .GetComponent<PlaceableItemObserver>()
                    .placeableItems;
                
                // check if any other placeable is around the position of the desired location to place at
                Collider[] colliderArray = Physics.OverlapBox(
                    positionToPlace,
                    colliderExtens,
                    Quaternion.identity,
                    placeableLayer
                    );
               // Transform nearestPlaceable = colliderArray.Any() ? colliderArray[0].transform : null;
               // if (!(nearestPlaceable is null)) return false;
               if (colliderArray.Any()) return false;
                
                placedItem = GameObject.Instantiate(preFab, positionToPlace + (Vector3.up * transform.localScale.y) /2, Quaternion.identity);
                placedItem.transform.up = hit.normal;
                placedItem.AddComponent<BoxCollider>();
                placedItem.layer = 11;
                Placed();
                return true;
            }

            return false;
        }

        /**
         * The interaction to do with the item,if you press right mouse button
         */
        public virtual void InteractWithPlaceable()
        {
        }
        
        public virtual void Placed()
        {
            GameObject.Find("Sounds").GetComponents<AudioSource>()[8].Play();
        }

        public virtual void DestroyPlaceable()
        {
        }
    }
}