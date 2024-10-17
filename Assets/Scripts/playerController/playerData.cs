using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData" , menuName = "player/playerData" ,order = 1)]
public class playerData : ScriptableObject
{
    [Header("Health stats")]
    public float maxHealth;
    public float startingHealth;

    [Header("Blood stats")]
    public float maxBlood;
    public float startingBlood;

    public string startingClass;

    [Header("Stamina stats")]
    public float maxStamina;
    public float startingStamina;
    public float staminaRegen;
    //add other things as required
}
