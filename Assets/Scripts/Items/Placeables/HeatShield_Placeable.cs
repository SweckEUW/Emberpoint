using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Items.ActiveItems;
using System.Collections;
using UnityEngine.UI;

namespace Items.Placeables.HeatShield_Placeable
{
    public class HeatShield_Placeable : ActiveItemPlaceables
    {
        private bool placed = false;
        [SerializeField] private float timeToDestroy = 100;
        private float timeLeft;

        private GameObject zoneCircle = null;

        [SerializeField] private Transform healthbar;
        [SerializeField] private Image healthbarSprite;
        private Vector3 playerCamVector;

        private void Awake()
        {
            timeLeft = timeToDestroy;
        }

        private void FixedUpdate()
        {
            if (placed)
            {
                if(timeLeft > 0)
                {
                    timeLeft -= Time.fixedDeltaTime;
                }
                else
                {
                    DestroyPlaceable();
                }
                UpdateHealthBar();
            }

            
        }

        public override void Placed(){
            GameObject.Find("Player").GetComponent<Player.Player>().GetActiveZoneShields().Add(placedItem.transform);
            zoneCircle = GameObject.Find("Zone").GetComponent<ZoneUpdater>().CreateHeatShield(placedItem.transform.position);
            placedItem.GetComponent<HeatShield_Placeable>().SetPlaced(true);
            placedItem.GetComponent<HeatShield_Placeable>().zoneCircle = zoneCircle;
        }
        
        public override void DestroyPlaceable(){
            GameObject.Find("Player").GetComponent<Player.Player>().GetActiveZoneShields().Remove(transform);
            zoneCircle.GetComponent<Animation>().Play("ZoneDespawnAnimation");

            StartCoroutine(GameObject.Find("Zone").GetComponent<ZoneUpdater>().removeCircle(zoneCircle));
            Destroy(gameObject);
        }
        
        public void TakeDamage(float amount)
        {
            timeLeft -= amount;
        }

        private void UpdateHealthBar()
        {
            healthbar.forward = playerCamVector;
            healthbarSprite.fillAmount = timeLeft / timeToDestroy;
        }

        public void SetPlaced(bool isPlaced)
        {
            placed = isPlaced;
            if (placed)
            {
                healthbar.gameObject.SetActive(true);
                playerCamVector = GameObject.Find("Player").GetComponentInChildren<Camera>().transform.forward;
            }
        }
    }
}