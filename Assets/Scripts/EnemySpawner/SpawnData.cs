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
    
    [Header("Radius")]
    public float spawnRadius;

    [Header("Height")]
    public float Height;
}
