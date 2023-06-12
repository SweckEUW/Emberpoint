using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
public class TestInteraction : MonoBehaviour
{
   private InputManager controls;

   public int maxHealth = 100;
   public int currentHealth;
   
   public int maxHunger= 100;
   public int currentHunger;
   
   public int maxShield= 100;
   public int currentShield;

   public UIBar healthBar;
   public UIBar hungerBar;
   public UIBar shieldBar;
   
   void Start()
   {
      currentHealth = maxHealth;
      healthBar.SetMaxValue(maxHealth);

      currentHunger = maxHunger;
      hungerBar.SetMaxValue(maxHunger);
      
      currentShield = maxShield;
      shieldBar.SetMaxValue(maxShield);
   }
   
   public void TakeDamage(int damage)
   {
      currentHealth -= damage;
      
      healthBar.SetValue(currentHealth);
   }

   public void Heal(int amount)
   {
      currentHealth += amount;
      
      healthBar.SetValue(currentHealth);
   }
   
   public void GetHungryBy(int hungerAmount)
   {
      currentHunger -= hungerAmount;
      
      hungerBar.SetValue(currentHunger);
   }
   
   public void ShieldDepletedBy(int shieldDamage)
   {
      currentShield -= shieldDamage;
      
      shieldBar.SetValue(currentShield);
   }
}
