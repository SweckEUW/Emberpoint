using System.Collections;
using System.Collections.Generic;
using Items.ActiveItems;
using Items.DroppedItems;
using UnityEngine;

public class ActiveItemSword : ActiveItem
{

    //Sword Stats
    [SerializeField] private float attackDamage = 200f;
    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private float timeBeforeAttackCollision = 0.1f;
    private float timeToEndAttack;

    private Player.Player player;

    //Hitable layers
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private GameObject hitVFXPrefab;

    private bool inAttack;

    private void Awake()
    {
        this.inAttack = false;
        this.timeToEndAttack = attackDuration;
        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
        
        if (rigidbody == null)
        {
            rigidbody = this.gameObject.AddComponent<Rigidbody>();
        }
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        player = GameObject.Find("Player").GetComponent<Player.Player>();
    }

    /**
     * Starts the attack
     */
    public override void Use()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerItemAction>().animator.SetTrigger("Attack");
        Invoke("Attack",timeBeforeAttackCollision);
        GameObject.Find("Sounds").GetComponents<AudioSource>()[1].Play();
    }

    /**
     * Starts the collision of the sword
     */
    private void Attack()
    {
        Invoke("StopAttack",attackDuration);
        this.timeToEndAttack = attackDuration;
        this.inAttack = true;
    }

    /*private void Update()
    {
        if (inAttack)
        {
            if (timeToEndAttack > 0)
            {
                timeToEndAttack -= Time.deltaTime;
            }
            else
            {
                inAttack = false;
            }
        } 
    }*/

    /**
     * Stops the collision of the sowrd after the attack
     */
    public void StopAttack()
    {
        inAttack = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (inAttack)
        {
            if ((collisionLayers.value & 1 << collision.gameObject.layer) != 0)
            {
                Enemy hitEnemy = collision.collider.GetComponent<Enemy>();
                if (hitEnemy != null)
                {
                    if (!hitEnemy.KnockbackRigidbody()) // true if the enemy is still in a knockback motion
                    {
                        GameObject.Find("Sounds").GetComponents<AudioSource>()[2].Play();
                        hitEnemy.TakeDamage(attackDamage*player.currDamage);
                        GameObject hitEffect = Instantiate(hitVFXPrefab, collision.GetContact(0).point, Quaternion.identity);
                        Destroy(hitEffect, 0.5f);
                    }
                    
                }
            }
        }
        
    }
}
