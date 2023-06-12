using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Items.ActiveItems;
using System.Collections;

namespace Items.Placeables.Totem_Placeable
{
    public class Totem_Placeable : ActiveItemPlaceables
    {
        
        public override void Placed(){
            GameObject.Find("Zone").GetComponent<ZoneUpdater>().CreateTotem(placedItem.transform.position);
            Debug.Log("totem");
        }
        
    }
}