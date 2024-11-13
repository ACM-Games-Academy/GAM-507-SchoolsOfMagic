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
            Instantiate(spawnInformation.swarmerPrefab, location.position + randomPoint(spawnInformation.spawnRadius), Quaternion.identity);
        }
        for (int i = 0; i < spawnInformation.tankAmount; i++)
        {
            Instantiate(spawnInformation.tankPrefab, location.position + randomPoint(spawnInformation.spawnRadius), Quaternion.identity);
        }
        for (int i = 0; i < spawnInformation.flyerAmount; i++)
        {
            Instantiate(spawnInformation.flyerPrefab, location.position + randomPoint(spawnInformation.spawnRadius), Quaternion.identity);
        }
    }

    private Vector3 randomPoint(float radius)
    {
        //(x-k)^2 + (y-h)^2 = r^2

        float x = UnityEngine.Random.Range(-radius, radius);

        //equaiton of circle rearanged to find y
        float y = MathF.Sqrt(MathF.Pow(radius, 2) - MathF.Pow(x, 2));

        return new Vector3(x, 0, y);
    }
}
