using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class TankEnemy : Enemy
{
    [SerializeField] private TankStats tankStats;
    private Transform player;
    private Rigidbody playerRB;
    public NavMeshAgent agent;

    private bool IsStaggered;
    private bool isAttacking;

    //Temp
    public TMPro.TextMeshPro staggeredText;
    public Animator walkingAnim;


    //========== Stagger ===========
    protected override void GiveStagger()
    {
        StartCoroutine(StaggerCoroutine());
        Debug.Log("Tank enemy staggered!");
        GetComponentInChildren<Animator>().Play("a_CG_Idle");
        IsStaggered = true;
        agent.speed = 0; //Set speed to 0 when stunned
        staggeredText.text = "Staggered!";
    }

    private IEnumerator StaggerCoroutine()
    {
        yield return new WaitForSeconds(tankStats.staggerDuration);
        IsStaggered = false;
        Debug.Log("Staggered over");
        staggeredText.text = "";
        agent.speed = tankStats.movementSpeed;  //Resets movement speed after stun
    }


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        staggeredText.text = "";

        EnemyInitiate(tankStats.health, tankStats.isArmoured);        // Initialize health and armor from scriptable object

        agent.speed = tankStats.movementSpeed;                        // Set agent speed

    }

    private void Update()
    {
        if (!isAttacking)
        {
            MoveTowardsPlayer();
            if (Vector3.Distance(transform.position, player.position) < 3f)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        agent.isStopped = true;

        // A wind-up time for the attack?
        // yield return new WaitForSeconds(tankData.attackSpeed);


        yield return new WaitForSeconds(tankStats.attackSpeed/2);

        // Perform the attack 
        PerformAttack();
   
        // Cooldown before the next attack
        yield return new WaitForSeconds(tankStats.attackSpeed);

        

        agent.isStopped = false;
        isAttacking = false;
    }

    private void PerformAttack()
    {
        Debug.Log("Attacked!");
        GetComponentInChildren<Animator>().Play("a_CG_attack");

        // Calculate the direction from the enemy to the player
        Vector3 knockbackDirection = (player.position - transform.position).normalized;

        // Add a force to the player's Rigidbody away from the enemy
        float knockbackForce = 100f;
        playerRB.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }




    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);


            GetComponentInChildren<Animator>().Play("a_CG_walk");
            
        }
    }
}
