using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //Animation
    [Header("Animation")]
    private Animator animator;

    //Attack
    [Header("Attackstats")]
    [SerializeField] private float attackDamageFist = 5f;
  //  [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackRadius = 0.5f;

    //Enemies
    [Header("Enemies")]
    public LayerMask enemyLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /**
     * Triggers the attack on the player
     * Checks for enemies in the attack radius
     */
    public void Attack()
    {
        animator.SetTrigger("Attack");
        /*Collider[] hits = Physics.OverlapSphere(attackPosition.position, attackRadius, enemyLayer);
        foreach (Collider enemy in hits)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
        }*/
    }


    /**
     * For editor debugging
     */
    private void OnDrawGizmosSelected()
    {
     //   Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }

}
