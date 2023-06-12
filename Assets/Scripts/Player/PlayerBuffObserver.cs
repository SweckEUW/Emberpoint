using System;
using System.Collections.Generic;
using Items.ActiveItems;
using UnityEngine;

namespace Player
{
    public class PlayerBuffObserver : MonoBehaviour
    {
        [SerializeField]
        public List<Effect> effects = new List<Effect>();

        public List<Effect> finishedEffects = new List<Effect>();
        private Player player;

        public UIBar healthBar;
        
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }


        
        public void Update()
        {
            // iterate through all effects of a buff
            // and ask if buff has to be turned on or turned off
            foreach (Effect effect in effects)
            {
                // Debug.Log("duration   " +effect.currentDuration);
                // Debug.Log("bufftype   " +effect.BuffType);
                //Debug.Log(effect.currentDuration);
                //check which kind of buff
                if (effect.BuffType.Equals("Attack"))
                {
                    // is user not buffed yet? ( buff not running so turn it on)
                    // and give player buff
                    if (!effect.running)
                    {
                        effect.running = true;
                        effect.currentDuration -= Time.deltaTime;
                        player.currDamage += effect.Percent;
                    }
                    // is the buff still running but buff has run out of time?
                    // then get rid of user buff
                    else if (effect.running && effect.currentDuration <= 0)
                    {
                        effect.running = false;
                        player.currDamage -= effect.Percent;
                        finishedEffects.Add(effect);
                        effect.currentDuration = effect.EffectDuration;
                        continue;
                    }
                    // buff still running but not run out of time?
                    //then decrease time
                    else
                    {
                        effect.currentDuration -= Time.deltaTime;
                    }
                }
                if (effect.BuffType.Equals("RegenerationHealth"))
                {
                    if (!effect.running)
                    {
                        effect.running = true;
                        effect.currentDuration -= Time.deltaTime;
                        player.HealDamage(player.defaultHealth * effect.Percent * Time.deltaTime);
                    }
                    else if (effect.running && effect.currentDuration <= 0)
                    {
                        effect.running = false;
                        finishedEffects.Add(effect);
                        effect.currentDuration = effect.EffectDuration;
                        continue;
                    }
                    else
                    {
                        player.HealDamage(player.defaultHealth * effect.Percent * Time.deltaTime);
                        effect.currentDuration -= Time.deltaTime;
                    }
                }
                if (effect.BuffType.Equals("SaturationRegeneration"))
                {
                    if (!effect.running)
                    {
                        effect.running = true;
                        effect.currentDuration -= Time.deltaTime;
                        player.DecreaseHunger(player.defaultHunger * effect.Percent * Time.deltaTime);
                    }
                    else if (effect.running && effect.currentDuration <= 0)
                    {
                        effect.running = false;
                        finishedEffects.Add(effect);
                        effect.currentDuration = effect.EffectDuration;
                        continue;
                    }
                    else
                    {
                        player.DecreaseHunger(player.defaultHunger * effect.Percent * Time.deltaTime);
                        effect.currentDuration -= Time.deltaTime;
                    }
                }
                if (effect.BuffType.Equals("MoreLife"))
                {
                    effect.running = false;
                    player.maxHealth += player.defaultHealth * effect.Percent;
                    healthBar.SetMaxValue(player.maxHealth);
                    healthBar.SetValue(player.currHealth, true);
                    var healthBarRect = healthBar.GetComponent<RectTransform>();
                    healthBarRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, 
                        healthBarRect.rect.width + healthBarRect.rect.width * effect.Percent);
                    healthBarRect.anchoredPosition  = new Vector2(healthBarRect.anchoredPosition.x + (healthBarRect.rect.width * effect.Percent)/2.3f, 479.63f);
                    finishedEffects.Add(effect);
                    continue;

                }
                if (effect.BuffType.Equals("Saturation"))
                {
                    effect.running = false;
                    player.DecreaseHunger(player.defaultHunger * effect.Percent);
                    finishedEffects.Add(effect);
                    effect.currentDuration = effect.EffectDuration;
                    continue;
                }

                if (effect.BuffType.Equals("Health"))
                {
                    effect.running = false;
                    player.HealDamage(player.defaultHealth * effect.Percent);
                    finishedEffects.Add(effect);
                    continue;
                }
            }

            foreach (var finishedEffect in finishedEffects)
            {
                effects.Remove(finishedEffect);
            }

            finishedEffects.Clear();
        }
        
    }
}