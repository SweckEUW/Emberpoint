using System.Collections;
using System.Collections.Generic;
using Items.DroppedItems;
using Items.InventoryItems;
using UnityEngine;

public class PlayerItemAction : MonoBehaviour
{
    //Use Cooldown
    private float timeToNextAction = 0f;
    private bool inAction = false;

    //References
    private PlayerCombat p_Combat;
    private PlayerMovement p_Movement;

    //Animator
    public Animator animator;
    private Inventory inventory;

    private void Awake()
    {
        timeToNextAction = 0f;
        inAction = false;

        p_Combat = GetComponent<PlayerCombat>();
        p_Movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    /**
     * Determines which kind of item is held by the player and uses it if the use cooldown is 0
     */
    public void UseItem(ActiveItem item)
    {
        if(timeToNextAction <= 0)
        {
            if(item == null)
            {
                return;
            }

            p_Movement.StartCoroutine("SlowDown");
            inAction = true;
            
            item.Use();
            timeToNextAction = item.GetUseSpeed();
        }
    }
    
    public void DropItem(InventoryItem itemToDrop)
    {
        var droppedItem = Instantiate(itemToDrop.preFab,this.gameObject.transform.position,Quaternion.identity);
        var dropItem = droppedItem.GetComponent<DroppedItem>();
        dropItem.item = itemToDrop;
        dropItem.Initiate();
    }



    // Update is called once per frame
    void Update()
    {
        if (inAction)
        {
            if (timeToNextAction > 0)
            {
                timeToNextAction -= Time.deltaTime;
            }
            else
            {
                inAction = false;
                p_Movement.StartCoroutine("SpeedUp");
            }
        }
        
    }
}
