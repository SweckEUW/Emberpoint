using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Items.DroppedItems;
using Items.InventoryItems;
using Items.Placeables;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items.ActiveItems
{
    public class ActiveItemPickaxe : ActiveItem
    {
        public float timeInSeconds;
        private float currentTimeInSeconds;

        public List<InventoryItem> destroyableItems;

        //Pickaxe Stats
        [SerializeField] private float attackDamage;
        public LayerMask placeables;
        public LayerMask ores;

        private void Awake()
        {
            currentTimeInSeconds = timeInSeconds;
        }

        public override void Use()
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerItemAction>().animator.SetTrigger("Mine");
            CheckForCollision();
        }

        private void CheckForCollision()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit = new RaycastHit();
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeables))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Placeables.HeatShield_Placeable.HeatShield_Placeable>() != null) return;
                var droppedItem = Instantiate(hitObject.GetComponent<ActiveItemPlaceables>().preFab,hitObject.transform.position,Quaternion.identity);
                var dropItem = droppedItem.GetComponent<DroppedItem>();
                droppedItem.transform.localScale = dropItem.scale;
                hitObject.GetComponent<ActiveItemPlaceables>().DestroyPlaceable();
                dropItem.item = hitObject.GetComponent<ActiveItemPlaceables>().preFab.GetComponent<DroppedItem>().item;
                dropItem.Initiate();
                Destroy(hitObject);
                GameObject.Find("Sounds").GetComponents<AudioSource>()[6].Play();
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, ores))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (!CheckIfItemDestroyable(hitObject)) return;
                hitObject.GetComponent<OrePlaceable>().AttackOre(attackDamage,hitObject);
                GameObject.Find("Sounds").GetComponents<AudioSource>()[6].Play();
            }
        }

        private bool CheckIfItemDestroyable(GameObject collidedItem)
        {
            foreach (var item in destroyableItems)
            {
                if (item.itemName.Equals(collidedItem.GetComponent<DroppedItem>().item.itemName))
                {
                    return true;
                }
            }

            changeOreColour(collidedItem);
            return false;
        }

        private async Task changeOreColour(GameObject collidedItem)
        {
         
            collidedItem.GetComponent<Renderer>().material = collidedItem.GetComponent<OrePlaceable>().blockedMaterial;
            await Task.Delay(350);
            collidedItem.GetComponent<Renderer>().material = collidedItem.GetComponent<OrePlaceable>().defaultMaterial;
        }          


    }
}