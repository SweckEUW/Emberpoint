using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Items.ActiveItems;
using Items.InventoryItems;
using Items.Placeables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ActiveItemChest : ActiveItemPlaceables
{
   public ChestInventory chestInventory;
   private ChestMenu chestMenuInv;
   private TransferMenu chestMenuChest;

   private bool menuisOpenend;

   public GameObject leftToRight;
   public GameObject rightToLeft;
   public List<GameObject> buttons = new List<GameObject>();
   
   public PlayerInput playerInput;

   private Vector2 firstMenuCenter = new Vector2(700, 450);
   private Vector2 secondMenuCenter = new Vector2(1340, 450);
   
   void Awake()
   {
      inventory = Resources.FindObjectsOfTypeAll<Inventory>()[0].GetComponent<Inventory>();
      chestInventory = Resources.FindObjectsOfTypeAll<ChestInventory>()[0].GetComponent<ChestInventory>();

      chestMenuInv = Resources.FindObjectsOfTypeAll<ChestMenu>()[0].GetComponent<ChestMenu>();
      chestMenuChest = Resources.FindObjectsOfTypeAll<TransferMenu>()[0].GetComponent<TransferMenu>();

        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
   }
   

   void Update()
   { 
      if (Keyboard.current.fKey.wasReleasedThisFrame && menuisOpenend)
      {
         chestMenuInv.closeMenu();
         chestMenuChest.closeMenu();
         playerInput.GetInputControls().Player.Act.Enable();
         ClearButtons();
         menuisOpenend = false;
      }
   }

   public override void InteractWithPlaceable()
   {
      chestMenuInv.createMenu("ChestMenuInventory", firstMenuCenter, inventory);
      chestMenuChest.createMenu("ChestMenuInventory", secondMenuCenter, chestInventory);
      Debug.Log(playerInput);
      playerInput.GetInputControls().Player.Act.Disable();
      GenerateButtons();
      menuisOpenend = true;
   }

   private void GenerateButtons()
   {
      Vector3 position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
      Quaternion rotation = Quaternion.Euler(0, 0, 0);
      buttons.Add(Instantiate(leftToRight, position, rotation) as GameObject);
      position.y -= 160;
      buttons.Add(Instantiate(rightToLeft, position, rotation) as GameObject);
      
      buttons[0].transform.parent = GameObject.Find("ChestButtons").transform;
      buttons[1].transform.parent = GameObject.Find("ChestButtons").transform;
      
      buttons[0].GetComponent<Button>().onClick.AddListener(TransferInvToChest);
      buttons[1].GetComponent<Button>().onClick.AddListener(TransferChestToInv);
   }

   private void ClearButtons()
   {
      foreach (var button in buttons)
      {
         Destroy(button);
      }

      buttons.Clear();
   }

   private void TransferInvToChest()
   {
      if (chestInventory.chestItems.Count >= 16)
      {
         return;
      }
      
      var itemsToRemove = new List<InventoryItem>();
      
      for (int i = 0; i < inventory.items.Count; i++)
      {
         var temperItem = chestMenuInv.chestMenuItems[i].GetComponent<ChestMenuItem>();

         if (temperItem.isFullySelected || temperItem.isPartlySelected)
         {
            foreach (var item in chestInventory.chestItems)
            {
               if (temperItem.baseItem.itemName == item.itemReference.itemName)
               {
                  item.itemAmount += temperItem.baseItem.selectedAmount;
                  temperItem.baseItem.itemAmount -= temperItem.baseItem.selectedAmount;
               }
            }

            if (!chestInventory.chestItems.Any(item => item.itemReference.itemName == temperItem.baseItem.itemName) )
            {
               var transferData = new TransferInfo(temperItem.baseItem.selectedAmount, temperItem.baseItem);
               temperItem.baseItem.itemAmount -= temperItem.baseItem.selectedAmount;
               chestInventory.chestItems.Add(transferData);
            }
            
            if (temperItem.baseItem.itemAmount <= 0)
            {
               itemsToRemove.Add(temperItem.baseItem);
            }
            
            temperItem.ResetSelection();
         }
      }
      
      foreach (var itemToRemove in itemsToRemove)
      {
         inventory.items.Remove(itemToRemove);
      }

      itemsToRemove.Clear();
      
      chestMenuInv.closeMenu();
      chestMenuInv.createMenu("ChestMenuInventory", firstMenuCenter, inventory);
      
      chestMenuChest.closeMenu();
      chestMenuChest.createMenu("ChestMenuInventory", secondMenuCenter, chestInventory);
   }

   private void TransferChestToInv()
   {
      if (inventory.items.Count >= 16)
      {
         return;
      }
      
      var itemsToRemove = new List<TransferInfo>();
      
      for (int i = 0; i < chestInventory.chestItems.Count; i++)
      {

         var temperItem = chestMenuChest.chestMenuItems[i].GetComponent<TransferMenuItem>();
         
         if (temperItem.isFullySelected || temperItem.isPartlySelected)
         {
            foreach (var invItem in inventory.items.ToList())
            {
               if (invItem.name == temperItem.baseInfo.itemReference.itemName)
               {
                  invItem.itemAmount += temperItem.selectedAmount;
                  temperItem.baseInfo.itemAmount -= temperItem.selectedAmount;
               }
            }
         
            if (!inventory.items.Contains(temperItem.baseInfo.itemReference))
            {
               var preparedItem = temperItem.baseInfo.itemReference;
               preparedItem.itemAmount = temperItem.selectedAmount;
               inventory.items.Add(preparedItem);
               temperItem.baseInfo.itemAmount -= preparedItem.itemAmount;
            }
      
            if (temperItem.baseInfo.itemAmount <= 0)
            {
               itemsToRemove.Add(temperItem.baseInfo);
            }
         }
      }
      
      foreach (var itemToRemove in itemsToRemove)
      {
         chestInventory.chestItems.Remove(itemToRemove);
      }

      itemsToRemove.Clear();
      
      chestMenuInv.closeMenu();
      chestMenuInv.createMenu("ChestMenuInventory", firstMenuCenter, inventory);
      
      chestMenuChest.closeMenu();
      chestMenuChest.createMenu("ChestMenuInventory", secondMenuCenter, chestInventory);
   }
} 
