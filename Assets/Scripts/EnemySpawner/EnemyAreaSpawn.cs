using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaSpawn : MonoBehaviour
{
    [SerializeField] int addEnemies;
    EnemySpawn enemySpawn;

    void Start()
    {
        enemySpawn = GetComponentInChildren<EnemySpawn>();
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            enemySpawn.setTime(3f, 6f);
            enemySpawn.addSpawnAmount(addEnemies);
            enemySpawn.SetSpawn();
        }
    }
}
