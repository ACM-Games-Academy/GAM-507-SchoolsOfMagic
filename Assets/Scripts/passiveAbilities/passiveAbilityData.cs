using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "passiveAbilityData", menuName = "passive/passiveData", order = 1)]
public class passiveAbilityData : ScriptableObject
{
    [Header("Passive Name")]
    public string passiveName;

    [Header("Radius")]
    public float detectionRadius = 5f;
    
    [Header("Passive Abilities")]
    public float additionalHealth = 1.1f; //This adds 10% of Health
    public float damageReduction = 0.8f; //This will Reduce Damage by 20%
}
