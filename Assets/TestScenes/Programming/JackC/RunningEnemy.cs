using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RunningEnemy : Enemy
{
    [SerializeField] private RunnerStats runnerStats;
    public UnityEngine.AI.NavMeshAgent agent;
    private Transform player;
    private bool attacking;
    void OnEnable()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = runnerStats.movementSpeed;
        EnemyInitiate(runnerStats.health, runnerStats.isArmoured);
    }

    private void Update()
    {
        if (!attacking)
        {
            MoveTo();
            if (Vector3.Distance(transform.position, player.position) < 2f)
            {
                StartCoroutine(StartAttack());
            }
        }
    }

    private void MoveTo()
    {
        agent.SetDestination(player.position);
    }

    private IEnumerator StartAttack()
    {
        attacking = true;
        agent.isStopped = true;
        ThrowAttack();
        yield return new WaitForSeconds(runnerStats.attackSpeed);
        agent.isStopped = false;
        attacking = false;
    }

    private void ThrowAttack()
    {
        if (Vector3.Distance(transform.position, player.position) < 2f)
        {
            //Do damage
        }
        else
        {
            //No damage
        }
    }

    protected override void EnemyDeath()
    {
        base.EnemyDeath();
        Destroy(this.gameObject);
    }
}
