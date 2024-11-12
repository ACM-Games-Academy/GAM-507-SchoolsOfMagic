using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] ParticleSystem spawningEffect;
    [SerializeField] float minimumSpawnTime;
    [SerializeField] float maximumSpawnTime;

    float spawnTime;
    
    public void setTime(float minTime, float maxTime)
    {
        minimumSpawnTime = minTime;
        maximumSpawnTime = maxTime;
    }
    
    
    // Awake is to test if all the methods and Coroutines function
    // Remove this once we have one of the three conditions
    //void Awake()
    //{
       // SpawnTimer();
   // }

    private void SpawnTimer()
    {
        spawnTime = Random.Range(minimumSpawnTime, maximumSpawnTime);
        StartCoroutine(BeginSpawning());
    }

    IEnumerator BeginSpawning()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Instantiate(enemyObject, transform.position, Quaternion.identity);
        if (spawningEffect != null) spawningEffect.Play();
    }
}
