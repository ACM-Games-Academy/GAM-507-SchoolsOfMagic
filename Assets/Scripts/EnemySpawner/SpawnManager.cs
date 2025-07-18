using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct SpawnCriteria
{
    public string name;
    public SpawnEventHandler spawnCriteria;
    public SpawnData spawnInformation;
    public Transform location;
}
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnCriteria> spawnEvents;
    [SerializeField] private GameObject groundParticleEffect;

    [SerializeField] private GameObject swarmerPrefab;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private GameObject flyerPrefab;

    private void Update()
    {
        foreach (SpawnCriteria script in spawnEvents)
        {
            if (script.spawnCriteria.Completion == true)
            {
                Spawn(script.spawnInformation, script.location);
                spawnEvents.Remove(script);
            }
        }
    }

    private void Spawn(SpawnData spawnInformation, Transform location)
    {
        for (int i = 0; i < spawnInformation.swarmerAmount; i++)
        {
            StartCoroutine(spawnEnemy(swarmerPrefab, location.position + randomPoint(spawnInformation.spawnRadius)));
            //Instantiate(spawnInformation.swarmerPrefab, location.position + randomPoint(spawnInformation.spawnRadius), Quaternion.identity);
        }
        for (int i = 0; i < spawnInformation.tankAmount; i++)
        {
            StartCoroutine(spawnEnemy(tankPrefab, location.position + randomPoint(spawnInformation.spawnRadius)));           
        }
        for (int i = 0; i < spawnInformation.flyerAmount; i++)
        {
            //this wont use the particle effect as it spawns in the sky
            Instantiate(flyerPrefab, location.position + Height(spawnInformation.Height) + randomPoint(spawnInformation.spawnRadius), Quaternion.identity);
        }
    }

    private Vector3 randomPoint(float radius)
    {
        //(x-k)^2 + (y-h)^2 = r^2

        float x = UnityEngine.Random.Range(-radius, radius);

        //equaiton of circle rearanged to find y
        float y = MathF.Sqrt(MathF.Pow(radius, 1) - MathF.Pow(x, 1));

        return new Vector3(x, 0, y);
    }

    private Vector3 Height(float height)
    {
        return new Vector3(0, height, 0);
    }

    private IEnumerator spawnEnemy(GameObject enemy, Vector3 position)
    {
        GameObject particleSfx = Instantiate(groundParticleEffect, position, Quaternion.identity);

        yield return new WaitForSeconds(2f);

        Destroy(particleSfx);

        Instantiate(enemy, position, Quaternion.identity);
        yield return null;
    }
}
