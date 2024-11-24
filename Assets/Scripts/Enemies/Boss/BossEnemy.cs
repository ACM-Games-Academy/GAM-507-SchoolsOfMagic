using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Threading.Tasks;

public class BossEnemy : Enemy
{
    [SerializeField] private BossStats bossStats;
    private Transform player;
    public int pillarsActive = 4; //Count number of active pillars
    [SerializeField] private float turnSpeed = 0.5f; // Speed of turning
    [SerializeField] private GameObject bossUI;

    private bool bossStarted  = false; 

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Find the player
        //GetComponentInChildren<Animator>().Play("Branch Slam");
        bossUI.SetActive(false);
        EnemyInitiate(bossStats.health, bossStats.isArmoured);
    }

    private void Update()
    {
        if (pillarsActive == 4)
        {
            BossStart();

        }

        if (bossStarted)
        {
            RotateTowardsPlayer();
        }
        Debug.Log(Health);
    }

    public void BossStart()
    {
        bossStarted = true;
        bossUI.SetActive(true);
    }

    private void RotateTowardsPlayer()
    {
        if (bossStarted)
        {

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    protected override void EnemyDeath()
    {
        base.EnemyDeath();

        // Target position for the object (e.g., lowering it by 5 units in the Y-axis)
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);

        // Smoothly interpolate the object's position toward the target
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 1f);
        bossUI.SetActive(true);
    }
    

}
