using System.Collections.Generic;
using Items.DroppedItems;
using Items.InventoryItems;
using Items.Placeables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items.ActiveItems
{
    public class ActiveItemAxe : ActiveItem
    {
        public float timeInSeconds;
        private float currentTimeInSeconds;

        public List<InventoryItem> destroyableItems;

        //Pickaxe Stats
        [SerializeField] private float attackDamage;
        public LayerMask placeables;
        public LayerMask wood;

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
                GameObject.Find("Sounds").GetComponents<AudioSource>()[7].Play();
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, wood))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (!CheckIfItemDestroyable(hitObject)) return;
                hitObject.GetComponent<WoodPlaceable>().AttackWood(attackDamage,hitObject);
                GameObject.Find("Sounds").GetComponents<AudioSource>()[7].Play();
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

            return false;
        }
    }
}