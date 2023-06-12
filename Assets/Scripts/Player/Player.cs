using System;
using System.Collections;
using System.Collections.Generic;
using Items.DroppedItems;
using Items.InventoryItems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace  Player
{
    public class Player : MonoBehaviour
    {
        //player stats
        [Header("Player Stats")]
        [SerializeField] private float maxDamage = 100f;

        [SerializeField] private float maxHunger = 100f;
        public float currHunger = 0;
        [SerializeField] private float hungerDamage = 2f;
        [SerializeField] private float starvingRate = 0.33f;
        private bool starving = false;
        private bool hungerStarted = false;
    
        public float defaultHealth;
        public float defaultHunger;

        [SerializeField] public float maxHealth = 100f;
        public float currHealth;

        [SerializeField] private float zoneDamage = 4f;

        //Items
        [Header("Player Stats")]
        public float defaultDamage;
        public float currDamage;

        private ActiveItem activeItem = null;
        [SerializeField] private Transform rightHandItemJoint;

        //ScriptReferences
        private PlayerItemAction p_ItemAction;
        private PlayerBuildingMode p_BuildingMode;
        private PlayerMovement p_Movement;

        private Vector3 spawnPoint = Vector3.zero;

        private Inventory inventory;
        private Cloth cloth;

        [NonSerialized]
        public GameObject equippedItem;

        private List<Transform> activeZoneShields;
        [NonSerialized]
        public bool isDead = false;
        [SerializeField] private float deathAnimationTime = 3f;
        private Animator animator;
    
    
        //PrototypWeapon
        [SerializeField] private GameObject swordPrefab;
    
        //UI References
        [SerializeField] private UIBar healthBar;
        [SerializeField] private UIBar hungerBar;
        [SerializeField] private UIBar shieldBar;

        [SerializeField] private GameObject zone;
        [SerializeField] private Core coreScript;

        /**
         * Initializes the player variables
         */
        private void Awake()
        {
            currDamage = maxDamage;
            currHunger = maxHunger;
            starving = false;
            activeItem = null;
            currHealth = maxHealth;

            defaultDamage = maxDamage;
            defaultHunger = maxHunger;
            defaultHealth = maxHealth;

            spawnPoint = transform.position;
        
            starving = false;
            hungerStarted = false;
            isDead = false;
            animator = GetComponent<Animator>();
            inventory = GameObject.FindObjectOfType<Inventory>(); 
            cloth = GameObject.FindObjectOfType<Cloth>();
            p_ItemAction = GetComponent<PlayerItemAction>();
            p_BuildingMode = GetComponent<PlayerBuildingMode>();
            p_Movement = GetComponent<PlayerMovement>();

            activeZoneShields = new List<Transform>();
        }


        private void Update() {
            if(!zone.GetComponent<ZoneUpdater>().isPointInsideZone(transform.position)){
                TakeDamage(zoneDamage * Time.deltaTime);
                GameObject.Find("Environment").GetComponents<AudioSource>()[0].pitch = 0.5f;
                GameObject.Find("Environment").GetComponents<AudioSource>()[1].pitch = 0.5f;
            }else{
                GameObject.Find("Environment").GetComponents<AudioSource>()[0].pitch = 1;
                GameObject.Find("Environment").GetComponents<AudioSource>()[1].pitch = 1;
            }
                

            if (hungerStarted)
            {
                IncreaseHunger(starvingRate * Time.deltaTime);
            }
            
            if (starving)
            {
                TakeDamage(hungerDamage * Time.deltaTime);
            }
        }

        /**
         * Determines the action based on the actively held item
         */
        public void Act()
        {
            if(isDead || inventory.itemToUse == null)
            {
                return;
            }
            else if (inventory.itemToUse != null 
                     && !inventory.itemToUse.itemName.Equals("StoneSword")
                     && !inventory.itemToUse.itemName.Equals("IronSword")
                     && !inventory.itemToUse.itemName.Equals("DiamondSword")
                     && !inventory.itemToUse.itemName.Equals("EmberSword"))
            {
                p_ItemAction.UseItem(inventory.itemToUse.preFab.GetComponent<ActiveItem>());
            }
            else
            {
                p_ItemAction.UseItem(activeItem);
            } 
        }

        /**
         * Weapon Equip Test
         */
        public void EquipWeapon()
        {
            if (inventory.itemToUse != null)
            {
                if (activeItem == null)
                {
                    if(equippedItem != null) Destroy(equippedItem);
                    GameObject sword = Instantiate(inventory.itemToUse.preFab, rightHandItemJoint.position,
                        rightHandItemJoint.rotation, rightHandItemJoint);
                    equippedItem = sword;
                    sword.GetComponent<DroppedItem>().isWearing = true;
                    //  sword.GetComponent<DroppedItem>().enabled = false;
                    activeItem = sword.GetComponent<ActiveItem>();
                }
            }
        }

        public void Equip()
        {
            p_BuildingMode.Reset();
            if (equippedItem != null) Destroy(equippedItem);
            GameObject item = Instantiate(inventory.itemToUse.preFab, rightHandItemJoint.position,
                rightHandItemJoint.rotation, rightHandItemJoint);
            if(item.GetComponent<Rigidbody>() != null) item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            equippedItem = item;
            item.transform.localScale = item.GetComponent<DroppedItem>().scaleInHand;
            item.GetComponent<DroppedItem>().isWearing = true;
            activeItem = item.GetComponent<ActiveItem>();
            Items.ActiveItems.ActiveItemPlaceables placeableScript = item.GetComponent<Items.ActiveItems.ActiveItemPlaceables>();
            if (placeableScript != null)
            {
                if (item.GetComponent<Items.Placeables.OrePlaceable>() == null && item.GetComponent<Items.Placeables.WoodPlaceable>() == null)
                p_BuildingMode.StartBuildMode(placeableScript);
            }
        }

        /**
         * Deals damage to the player and decreases his curr_hitpoints
         * When the hitpoints reach 0 the player dies
         */
        public void TakeDamage(float amount)
        {
            this.currHealth = Mathf.Max(this.currHealth - amount, 0f);
            healthBar.SetValue(this.currHealth);

            if (isDead) return;
            if(this.currHealth <= 0)
            {
                GameObject.Find("Sounds").GetComponents<AudioSource>()[3].Play();
                isDead = true;
                animator.SetTrigger("Die");
                FindObjectOfType<RadialMenu>().CloseMenu();
                p_BuildingMode.Reset();
                Invoke("Die", deathAnimationTime);
            }
        }

        /**
         * Heals the players hitpoints
         */
        public void HealDamage(float amount)
        {
            if(this.currHealth < this.maxHealth)
            {
                this.currHealth = Mathf.Min(this.currHealth + amount, this.maxHealth);
                healthBar.SetValue(this.currHealth);
            }
        }


        /**
         * Increases the players hunger
         * On 0 hunger the player starves slowly
         */
        public void IncreaseHunger(float amount)
        {
            this.currHunger = Mathf.Max(this.currHunger - amount, 0f);
            hungerBar.SetValue(this.currHunger);

            if (this.currHunger <= 0)
            {
                starving = true;
            }
        }

        /**
         * Decreases the players hunger
         */
        public void DecreaseHunger(float amount)
        {
            if (this.currHunger < this.maxHunger)
            {
                this.currHunger = Mathf.Min(this.currHunger + amount, this.maxHunger);
			    hungerBar.SetValue(this.currHunger);
            }
            if(this.currHunger > 0)
            {
                starving = false;
            }
        }

        /**
         * The player dies
         */
        private void Die()
        {
            

            isDead = true;
            Debug.Log("Player died");

            // insert empty inventory code

            // Shrink Zone
            // zone.GetComponent<ZoneUpdater>().shrinkZone();
            
            foreach (var item in inventory.items)
            {
                var droppedItem = 
                    Instantiate(
                        item.preFab,
                        p_Movement.playerBody.position + Vector3.up,
                        Quaternion.identity);
                var dropItem = droppedItem.GetComponent<DroppedItem>();
                dropItem.item = item;
                dropItem.itemAmount = item.itemAmount;
                dropItem.Initiate();
            }
            
            animator.ResetTrigger("Die");
            ResetPlayer();
            ResetEnemies();
            isDead = false;
            
        }

        /**
         * Resets the stats of the player
         */
        private void ResetPlayer()
        {
            cloth.enabled = false;
           
            CharacterController charController = GetComponent<CharacterController>();
            charController.enabled = false;
            transform.SetPositionAndRotation(spawnPoint, Quaternion.identity);
            charController.enabled = true;

            maxHealth = defaultHealth;
            maxDamage = defaultDamage;
            maxHunger = defaultHunger;

            currDamage = defaultDamage;
            HealDamage(maxHealth);
            DecreaseHunger(maxHunger);

            
            for(int i = 0; i < activeZoneShields.Count;)
            {
                activeZoneShields[i].GetComponent<Items.Placeables.HeatShield_Placeable.HeatShield_Placeable>().DestroyPlaceable();
            }

            foreach (var item in inventory.items)
            {
                item.itemAmount = 0;
            }

            var equipedItem = FindObjectOfType<Player>().equippedItem;
            if(equipedItem != null) Destroy(equipedItem);

            inventory.items.Clear();

            coreScript.SpawnEmbers();

            cloth.enabled = true;
        }

        /**
         * Resets the enemies 
         */
        private void ResetEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.Reset();
            }
        }

        public void StartGame()
        {
            hungerStarted = true;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().StartGame();
            }
        }

  	    public float getMaxHitpoints()
        {
            return maxHealth;
        }

        public float getMaxHunger()
        {
            return maxHunger;
        }

        public List<Transform> GetActiveZoneShields()
        {
            return activeZoneShields;
        }
    }
}
