using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : Enemy
{
    [SerializeField] private TankStats tankStats;
    public Transform playerPos;
    public NavMeshAgent agent;

    // Override the GiveStagger method from Enemy
    protected override void GiveStagger()
    {
        Debug.Log("Tank enemy staggered!");
       
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the tanks health 
        EnemyInitiate(tankStats.health, tankStats.isArmoured);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerPos.position);
    }
}
