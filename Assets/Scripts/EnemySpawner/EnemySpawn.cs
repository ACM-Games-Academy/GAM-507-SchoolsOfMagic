using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] int spawnAmount;

    private Vector3 spawnLocation;
    [SerializeField] float spawnLocationX;
    [SerializeField] float Height;
    [SerializeField] float spawnLocationZ;
    [SerializeField] ParticleSystem spawningEffect;

    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;

    float spawnTime;

    int maximumSpawnAmount = 10;
    
    public void addSpawnAmount(int Amount)
    {
       if(spawnAmount + Amount > maximumSpawnAmount)
        {
            spawnAmount = maximumSpawnAmount;
        }

       else
        {
            spawnAmount += Amount;
        }
    }


    public void setTime(float minTime, float maxTime)
    {
        minimumSpawnTime = minTime;
        maximumSpawnTime = maxTime;
    }

    //SetSpawn & BeginSpawning are methods for the Enemy Area Spawn, this will check if there's anymore enemies to spawn
    //This will then begin the Coroutine and spawn each enemy one at a time
    public void SetSpawn()
    {
        if(spawnAmount > 0) 
        {
            spawnAmount--;
            spawnTime = Random.Range(minimumSpawnTime, maximumSpawnTime);
            StartCoroutine(BeginSpawning());
        }
    }

    IEnumerator BeginSpawning()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnEnemy();
        yield return new WaitForSeconds(2);
        SetSpawn();
    }

    public void SpawnEnemy()
    {
        spawnLocation.x = Random.Range(-spawnLocationX, spawnLocationX);
        spawnLocation.y = Height;
        spawnLocation.z = Random.Range(-spawnLocationZ, spawnLocationZ);

        Instantiate(enemyObject, transform.position + spawnLocation, Quaternion.identity);
        if (spawningEffect != null) 
        { 
            spawningEffect.Play();
        }
    }
}
