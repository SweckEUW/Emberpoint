using Items.ActiveItems;
using Items.DroppedItems;
using Items.InventoryItems;
using UnityEngine;

namespace Items.Placeables
{
    public class WoodPlaceable : ActiveItemPlaceables
    {
        public float health;
        public InventoryItem itemToDrop;
        public int amountToDrop;
        public LayerMask WoodLayer;

        
        
        public override void Use()
        {
        }

        public void AttackWood(float attackDamage,GameObject hitObject)
        {
            if (health <= 0)
            {
                DropWood(hitObject);
            }
            else
            {
                health -= attackDamage;
                if (health <= 0) DropWood(hitObject);
            }
        }
        
        private void DropWood(GameObject hitObject)
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