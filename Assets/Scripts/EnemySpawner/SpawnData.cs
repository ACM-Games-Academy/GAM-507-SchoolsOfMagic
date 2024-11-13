using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Enemy/SpawnEvent", order = 1)]

public class SpawnData : ScriptableObject
{
    [Header("Amount of Enemies")]
    public int swarmerAmount;
    public int tankAmount;
    public int flyerAmount;

    [Header("Enemy Prefabs")]
    public GameObject swarmerPrefab;
    public GameObject tankPrefab;
    public GameObject flyerPrefab;

    [Header("Radius")]
    public float spawnRadius;

    [Header("Timer")]
    public float spawnTimer;
    public float minimumTimer;
    public float maxiumumTimer;
}
