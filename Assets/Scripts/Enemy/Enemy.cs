using System;
using System.Collections;
using System.Collections.Generic;
using Items.DroppedItems;
using Items.InventoryItems;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    private Player.Player playerScript;
    private NavMeshAgent agent;
    private Rigidbody rigidbody;
    private Transform zoneObject;
    private List<Transform> heatshields;

    [SerializeField] private GameObject buffEffectPrefab;
    private GameObject buffEffect;

    [Header("Enemy stats")]
    [SerializeField] private float maxHitpoints = 100f;
    private float currHitpoints;

    [Header("Move stats")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float patrollingSpeed = 2f; // Move speed while patrolling
    [SerializeField] private float retreatSpeed = 6f; // Enemy is sped up on retreat

    [SerializeField] private float knockbackForce = 20f;
    [SerializeField] private float knockbackDuration = 0.4f;

    [SerializeField] private LayerMask walkableLayer;

    [Header("Attack")]
    [SerializeField] private Transform attackPosition; // position from where the attacks checks for collision
    [SerializeField] private float attackRadius = 0.5f; // radius of the attack collision
    [SerializeField] private GameObject attackEffect; // effect attack
    [SerializeField] private GameObject attackBuildUpEffect; // effect of the build up for the attack
    [SerializeField] private float attackBuildUpDuration = 0.2f; // build up duration before the collision of the attack is checked
    [SerializeField] private LayerMask attackLayers; // layermask for the attackable objects (the player)

    [Header("Attack stats")]
    [SerializeField] private bool shieldPriority = false; // whether the enemy focuses it's attack on shield or on the player
    
    [SerializeField] private float buffFactor = 1.3f; // factor by which the stats increase if in the zone
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 1f; // time between attacks - attackSpeed 
    [SerializeField] private float attackDuration = 0.4f; // time of the attack animation
    private float timeToNextAttack = 0;
    private float timeToEndAttack = 0;
    private bool isAttacking = false;
    private bool inKnockback = false;
    private float currBuff = 1;

    [Header("PlayerDetection stats")]
    [SerializeField] private float hearDistance = 5f; // radius in which the player is automatically detected
    [SerializeField] private float viewDistance = 20f; // distance in which the player is seen by the enemy
    [SerializeField] private float viewAngle = 30f; // angle in which the player is seen by the enemy

    [Header("Patrolling State")]
    private string state = "idle"; // states: patrol, combat, retreat
    private Vector3 spawnPoint; // point to retreat to and patrol around
    [SerializeField] private float patrolDistance = 15f; // radius to patrol around the spawn point
    [SerializeField] private float retreatDistance = 100f; // retreats if the distance to spawnpoint is higher than the retreatDistance
    [SerializeField] private float minWaitTime = 2f; // min wait time before getting a new patrol destination
    [SerializeField] private float maxWaitTime = 8f; // max wait time before getting a new patrol destination
    private float waitTime = 0f;

    [Header("Healthbar")]
    [SerializeField] private Transform healthbar;
    [SerializeField] private Image healthbarSprite;
    private Vector3 playerCamVector;

    public List<EnemyDroppedItem> itemDrops;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.GetComponent<Player.Player>();
        playerCamVector = player.GetComponentInChildren<Camera>().transform.forward;
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        zoneObject = GameObject.Find("Zone").transform;
        //rigidbody.detectCollisions = false;

        currHitpoints = maxHitpoints;

        timeToNextAttack = 0;
        timeToEndAttack = attackDuration;
        isAttacking = false;
        inKnockback = false;

        currBuff = 1;

        agent.speed = patrollingSpeed;
        agent.acceleration = acceleration;

        state = "patrol";
        spawnPoint = transform.position;
        waitTime = 0;
        agent.stoppingDistance = 0f;

        buffEffect = Instantiate(buffEffectPrefab, transform.position + Vector3.up * 1, Quaternion.identity, transform);
    }

    /**
     * Switches to patrol state
     */
    private void SwitchToPatrolState()
    {
        agent.speed = patrollingSpeed;
        agent.acceleration = acceleration;
        agent.isStopped = false;
        waitTime = 0f;
        state = "patrol";
    }

    /**
     * Switches to retreat state
     */
    private void SwitchToRetreatState()
    {
        agent.speed = retreatSpeed;
        agent.acceleration = acceleration;
        agent.isStopped = false;
        agent.stoppingDistance = 0f;
        agent.SetDestination(spawnPoint);
        state = "retreat";
    }

    /**
     * Switches to combat state
     */
    private void SwitchToCombatState()
    {
        agent.speed = moveSpeed;
        agent.acceleration = acceleration;
        agent.isStopped = false;
        agent.stoppingDistance = attackRange*0.9f;
        state = "combat";
    }

    /**
     * Patrols around spawnpoint
     */
    private void Patrol()
    {
        if(waitTime > 0)
        {
            if (CheckPatrolPositionReached())
            {
                waitTime -= Time.deltaTime;
                animator.ResetTrigger("Move");
                animator.SetTrigger("Stop");
            }
        }
        else
        {
            animator.ResetTrigger("Stop");
            animator.SetTrigger("Move");
            agent.SetDestination(SearchRandomPatrolPosition());
            waitTime = DetermineRandomWaitTime();
        }
    }

    /**
     * Checks if the assigned patrol position is reached and returns the result
     */
    private bool CheckPatrolPositionReached()
    {
        return ((transform.position - agent.pathEndPosition).magnitude < 0.3f);
    }

    /**
     * Returns a random point in a circle around the spawnpoint and checks if the destiantion is reachable
     * Loops until a viable destiantion is found
     */
    private Vector3 SearchRandomPatrolPosition()
    {
        Vector2 randomPosition = Random.insideUnitCircle * patrolDistance;
        Vector3 newPatrolPosition = new Vector3(spawnPoint.x + randomPosition.x, 0f, spawnPoint.z + randomPosition.y);
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(newPatrolPosition, path);
        while(path.status == NavMeshPathStatus.PathPartial)
        {
            randomPosition = Random.insideUnitCircle * patrolDistance;
            newPatrolPosition = new Vector3(spawnPoint.x + randomPosition.x, 0f, spawnPoint.z + randomPosition.y);
            agent.CalculatePath(newPatrolPosition, path);
        }
        return newPatrolPosition;
    }

    /**
     * Returns a random wait time
     */
    private float DetermineRandomWaitTime()
    {
        return Random.Range(minWaitTime, maxWaitTime);
    }

    /**
     * Checks if the player can be detected by the enemy
     */
    private bool CheckForPlayerDetection()
    {
        if (CheckForPlayerInSight())
        {
            return true;
        }
        return ((transform.position - player.position).magnitude < hearDistance);
    }

    /**
     * Checks if the player can be detected by the enemy
     */
    private bool CheckForPlayerInSight()
    {
        if((transform.position - player.position).magnitude < viewDistance)
        {
            Vector3 playerVector = player.position - transform.position;
            float angle = Vector3.Angle(playerVector, transform.forward);
            return (Mathf.Abs(angle) <= viewAngle);
        }
        return false;
        
    }

    /**
     * Checks if the enemy is to far away from it's spawn point
     */
    private bool CheckOverRetreatDistance()
    {
        return ((transform.position - spawnPoint).magnitude > retreatDistance) || playerScript.isDead;
    }

    /**
     * Checks if the spawn position is reached and returns the result
     */
    private bool CheckRetreatPositionReached()
    {
        return ((transform.position - spawnPoint).magnitude < 0.3f);
    }

    /**
     * Checks if the enemy is in range of the player for an attack
     */
    private void CheckForAttack()
    {
        if(timeToNextAttack <= 0 && (agent.destination - transform.position).magnitude < attackRange*1.1f)
        {
            BuildUpAttack();
        }
    }

    /**
     * Triggers the build up for the attack
     * The agent stops on it's position for the duration of the attack
     */
    private void BuildUpAttack()
    {
        animator.SetTrigger("Attack");
        transform.rotation = Quaternion.LookRotation((agent.destination - transform.position), Vector3.up);
        agent.SetDestination(transform.position);
        timeToEndAttack = attackDuration;
        timeToNextAttack = attackCooldown / currBuff;
        isAttacking = true;
        GameObject effect = Instantiate(attackBuildUpEffect, attackPosition.position, Quaternion.identity, attackPosition);
        Destroy(effect, attackBuildUpDuration);
        StartCoroutine("Attack");
    }

    /**
     * Starts the attack collision after the buildup time
     */
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackBuildUpDuration);
        Collider[] collisions = Physics.OverlapSphere(attackPosition.position, attackRadius, attackLayers);

        heatshields = playerScript.GetActiveZoneShields();
        foreach(Transform shield in heatshields)
        {
            if(Vector3.Distance(shield.position, transform.position) < attackRange * 1.2f)
            {
                shield.GetComponent<Items.Placeables.HeatShield_Placeable.HeatShield_Placeable>().TakeDamage(attackDamage);
                Debug.Log("heatshield hit");
            }
        }
        
        if (collisions.Length > 0)
        {
            GameObject.Find("Sounds").GetComponents<AudioSource>()[9].Play();
            player.GetComponent<Player.Player>().TakeDamage(attackDamage * currBuff);
        }
        GameObject effect = Instantiate(attackEffect, attackPosition.position, Quaternion.identity);
        Destroy(effect, 1f);
    }

    /**
     * Checks if the attack duration is over
     */
    private void CheckForAttackEnd()
    {
        if(timeToEndAttack > 0)
        {
            timeToEndAttack -= Time.deltaTime;
        }
        else
        {
            isAttacking = false;
            agent.isStopped = false;
        }
    }

    /**
     * Reduces the time until the enemy attacks again
     */
    private void ReduceAttackCooldown()
    {
        if(timeToNextAttack > 0)
        {
            timeToNextAttack -= Time.deltaTime;
        }
    }

    /**
     * Sets the destination of the agent to the position of the player 
     * Starts the move animation of the enemy
     */
    private void ChasePlayer()
    {
        animator.SetTrigger("Move");
        

        if (shieldPriority)
        {
            heatshields = playerScript.GetActiveZoneShields();
            int minIndex = -1;
            float playerDistance = Vector3.Distance(player.position, transform.position);
            for (int j = 0; j < heatshields.Count; j++)
            {
                float distanceToShield = Vector3.Distance(heatshields[j].position, transform.position);
                if (distanceToShield < playerDistance)
                {
                    if(minIndex != -1)
                    {
                        if(distanceToShield < Vector3.Distance(heatshields[minIndex].position, transform.position)){
                            minIndex = j;
                        }
                    }
                    else
                    {
                        minIndex = j;
                    }
                }
            }
            if(minIndex != -1)
            {
                agent.SetDestination(heatshields[minIndex].position);
                return;
            }
        }
        agent.SetDestination(player.position);
        
    }

    /**
     * Reduces the current hitpoints by amount
     */
    public void TakeDamage(float amount)
    {

        this.currHitpoints = Mathf.Max(this.currHitpoints - amount / currBuff, 0f);

        if (this.currHitpoints <= 0)
        {
            Die();
        }
    }

    /**
     * The enemy object dies
     */
    private void Die()
    {
        DropItem();
        Destroy(gameObject);
    }
    
    public void DropItem()
    {
        EnemyDroppedItem[] itemsToDrop = new EnemyDroppedItem[3];

        for(int x = 0; x < 3; x++)
        {
            itemsToDrop[x] = itemDrops[Random.Range(0,itemDrops.Count)];
        }
        
        foreach (var item in itemsToDrop)
        {
            var droppedItem = 
                Instantiate(
                    item.item.preFab,
                    this.gameObject.transform.position + new Vector3(Random.Range(-1,1) * 2,1,Random.Range(-1,1) * 2),
                    Quaternion.identity);
            var dropItem = droppedItem.GetComponent<DroppedItem>();
            dropItem.item = item.item;
            dropItem.itemAmount = item.itemAmount;
            dropItem.Initiate();
        }
    }

    /**
     * Knocks the enemy back depending on the players position by assigning a velocity in the oppositve direction of the player by rigidbody physics
     */
    public bool KnockbackRigidbody()
    {
        if (!inKnockback)
        {
            agent.enabled = false;
            rigidbody.isKinematic = false;
            inKnockback = true;
            rigidbody.velocity = (transform.position - player.position).normalized * (knockbackForce / currBuff);
            StartCoroutine("EndAfterKnockbackDuration");
            return false;
        }
        else
        {
            return true;
        }

    }

    /**
     * Ends the knockback and returns the enemy to normal
     */
    private void EndKnockback()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;
       
        agent.enabled = true;
        inKnockback = false;

        RaycastHit hit;
        if(Physics.Raycast(transform.position,-Vector3.up, out hit,Mathf.Infinity,walkableLayer)){
            agent.Warp(hit.point);
        }

        //agent.Warp(new Vector3(transform.position.x, 0f, transform.position.z));
        agent.isStopped = false;
        agent.speed = moveSpeed;
        agent.updateRotation = true;
        agent.acceleration = acceleration;
        agent.velocity = Vector3.zero;
        if(state == "retreat")
        {
            agent.SetDestination(spawnPoint);
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    private IEnumerator EndAfterKnockbackDuration()
    {
        yield return new WaitForSeconds(knockbackDuration);
        EndKnockback();
    }

    private void CheckForZoneBuff()
    {
        if (!zoneObject.GetComponent<ZoneUpdater>().isPointInsideZone(transform.position))
        {
            currBuff = buffFactor;
            buffEffect.SetActive(true);
        }
        else
        {
            currBuff = 1;
            buffEffect.SetActive(false);
        }
    }

    private void UpdateHealthbar()
    {
        healthbar.forward = playerCamVector;
        healthbarSprite.fillAmount = currHitpoints / maxHitpoints;
    }

    /**
     * Resets the enemy to its start situation
     */
    public void Reset()
    {
        transform.position = spawnPoint;


        rigidbody.isKinematic = true;

        currHitpoints = maxHitpoints;

        timeToNextAttack = 0;
        timeToEndAttack = attackDuration;
        isAttacking = false;
        inKnockback = false;

        agent.speed = patrollingSpeed;
        agent.acceleration = acceleration;

        state = "patrol";
        waitTime = 0;
        agent.stoppingDistance = 0f;
    }

    public void StartGame()
    {
        healthbar.gameObject.SetActive(true);
    }

    /**
     * Update is called once per frame
     */
    void Update()
    {
        CheckForZoneBuff();
        if(state == "patrol")
        {
            Patrol();
            if (CheckForPlayerDetection())
            {
                SwitchToCombatState();
            }
        }
        else if(state == "combat")
        {
            if (!inKnockback)
            {
                if (!isAttacking)
                {
                    ChasePlayer();
                    if (CheckOverRetreatDistance()){
                        SwitchToRetreatState();
                    }
                    else
                    {
                        CheckForAttack();
                    }
                }
                else
                {
                    CheckForAttackEnd();
                }
            }


            ReduceAttackCooldown();
        }
        else if(state == "retreat")
        {
            if (CheckRetreatPositionReached())
            {
                SwitchToPatrolState();
            }
        }

        UpdateHealthbar();
        
    }

    [Serializable]
    public class EnemyDroppedItem
    {
        public int itemAmount;
        public InventoryItem item;
    }

}
