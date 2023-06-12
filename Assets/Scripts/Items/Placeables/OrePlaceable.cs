using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Items.ActiveItems;
using System.Collections;
using Items.DroppedItems;
using Items.InventoryItems;

namespace Items.Placeables
{
    public class OrePlaceable : ActiveItemPlaceables
    {
        public float health;
        public InventoryItem itemToDrop;
        public int amountToDrop;
        public LayerMask OreLayer;
        public Material blockedMaterial;
        public Material defaultMaterial;

        
        
        public override void Use()
        {
        }

        public void AttackOre(float attackDamage,GameObject hitObject)
        {
            if (health <= 0)
            {
                DropOres(hitObject);
            }
            else
            {
                health -= attackDamage;
                if (health <= 0) DropOres(hitObject);
            }
        }
        
        private void DropOres(GameObject hitObject)
        {
            var droppedItem = Instantiate(hitObject.GetComponent<ActiveItemPlaceables>().preFab,
                    hitObject.transform.position, Quaternion.identity);
                var dropItem = droppedItem.GetComponent<DroppedItem>();
                dropItem.itemAmount = amountToDrop;
                droppedItem.transform.localScale = dropItem.scale;
                dropItem.item = hitObject.GetComponent<ActiveItemPlaceables>().preFab.GetComponent<DroppedItem>().item;
                dropItem.Initiate();
                Destroy(hitObject);
        }
        
        
    }
}