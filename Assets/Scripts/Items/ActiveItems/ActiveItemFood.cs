using System;
using System.Collections.Generic;
using Items.InventoryItems;
using Player;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace Items.ActiveItems
{
    public class ActiveItemFood : ActiveItem
    {
        // all the buffs that the item has
        public List<Effect> buff;
        public InventoryItem item;


        /**
         * Activate buff by adding time and/or effects to the observer
         * for the specific buff
         */
        public override void Use()
        {
            if (item.itemAmount == 0) return;
            GameObject.Find("Sounds").GetComponents<AudioSource>()[5].Play();
            var inventory = GameObject.FindObjectOfType<Inventory>().GetComponent<Inventory>();
            item.itemAmount--;
            if(item.itemAmount == 0) Destroy(FindObjectOfType<Player.Player>().equippedItem);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            foreach (var effect in buff)
            {
                player.GetComponent<PlayerBuffObserver>().effects.Add(effect);
                player.GetComponent<PlayerItemAction>().animator.SetTrigger("Eat");
            }

        }
    }

    
    /**
     *
     *
     * "Attack",
        "RegenerationHealth",
        "MoreLife",
        "SaturationRegeneration",
        "Saturation",
        "Health"
     *
     * 
     */
    
    /**
     * specifies one specific effect of a buff
     */
    [Serializable]
    public class Effect
    {
        public string BuffType;
        
        // percentage of buff to add
        public float Percent;

        //overall time to run the buff
        public float EffectDuration;
        
        // time to describe the turned on buff
        public float currentDuration;
        
        // required for initalizing values first time in playerbuffobserver class
        public bool running  = false;
    }
}