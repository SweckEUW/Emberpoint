using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Items.InventoryItems;
using UnityEngine;

namespace Items.DroppedItems
{
    /**
     * classifies the object of a item that is dropped from the player or any other interaction
     */
    public class DroppedItem : MonoBehaviour
    {
        public InventoryItem item;

        public bool isWearing = false;
        public int itemAmount;

        // scales when dropping item
        public Vector3 scale;

        // scale for item when its in players hand
        public Vector3 scaleInHand;

        // time before user can pick up the item
        public float pickupCooldown = 0;

        private int playerLayer = 9;
        
        
        public void Initiate()
        {
            gameObject.layer = 15;
            pickupCooldown = 10;
            isWearing = false;
            transform.localScale = scale;
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            if (this.gameObject.GetComponent<Rigidbody>() == null)
            {
                this.gameObject.AddComponent<Rigidbody>();
            }
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            StartCoroutine(CountCooldown());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (FindObjectOfType<Player.Player>().isDead) return;
            
            if (!isWearing && other.gameObject.tag.Equals("Player"))
            {
                var inventory = FindObjectOfType<Inventory>();
                // player picks up item
                if (!inventory.IsInventoryFull(item: item) && pickupCooldown == 0)
                {
                    PickUpItem(inventory.GetDifferenceToMaxStack(item));
                }
            }
            else if (!isWearing && other.gameObject.layer == 10)
            {
                if (this.gameObject.GetComponent<Rigidbody>() != null)
                {
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }

        IEnumerator CountCooldown()
        {
            yield return new WaitForSeconds(1f);
            pickupCooldown = 0;
            if (!isWearing)
            {
                BoxCollider collider = GetComponent<BoxCollider>();
                Collider[] collisions = Physics.OverlapBox(transform.position+collider.center, collider.size/2, transform.rotation, 1 << playerLayer);
                if(collisions.Length > 0)
                {
                    var inventory = FindObjectOfType<Inventory>();
                    // player picks up item
                    if (!inventory.IsInventoryFull(item: item))
                    {
                        PickUpItem(inventory.GetDifferenceToMaxStack(item));
                    }
                }
                
            }
        }

        private void PickUpItem(int possiblePickupAmount)
        {
            GameObject.Find("Sounds").GetComponents<AudioSource>()[0].Play();

            
            if (itemAmount != null)
            {
                if (possiblePickupAmount >= itemAmount)
                {
                    GameObject.FindWithTag("Inventory")
                    .GetComponent<Inventory>()
                    .AddItemToInventory(item, itemAmountToAdd: itemAmount);
                }
                else
                {
                    GameObject.FindWithTag("Inventory")
                    .GetComponent<Inventory>()
                    .AddItemToInventory(item, itemAmountToAdd: possiblePickupAmount);
                    itemAmount -= possiblePickupAmount;
                    return;
                }
            }
            else
            {
                GameObject.FindWithTag("Inventory")
                                .GetComponent<Inventory>()
                                .AddItemToInventory(item);
            }
            Destroy(gameObject);
            Destroy(this);
        }
    }
}