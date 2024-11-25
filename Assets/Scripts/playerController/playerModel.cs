using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class playerModel
{
    playerData playerData;

    private float maxHealth;
    public float  MaxHealth
    { get { return maxHealth; } set { maxHealth = value; } }

    private float health;
    public float  Health
    { get { return health; } set { health = value; } }
    private float healthModifier;
    public float HealthModifier
    { get { return healthModifier; } set { healthModifier = value; }}

    private float dmgModifier;
    public float  DmgModifier
    { get { return dmgModifier; } set { dmgModifier = value; } }

    private float maxStamina;
    public float  MaxStamina
    { get { return maxStamina; } set { maxStamina = value; } }

    private float stamina;
    public float  Stamina
    { get { return stamina; } set { stamina = value; } }

    private float staminaModifier;
    public float  StaminaModifier
    { get { return staminaModifier; } set { staminaModifier = value; } }


    private float maxBlood;
    public float  MaxBlood
    { get { return maxBlood; } set { maxBlood = value; } }

    private float blood;
    public float  Blood
    { get { return blood; } set { blood = value; } }

    private float bloodModifier;
    public float BloodModifier
    { get { return bloodModifier; } set { bloodModifier = value; } }


    private float maxIron;
    public float  MaxIron
    { get { return maxIron; } set { maxIron = value; } }

    private float iron;
    public float  Iron
    { get { return iron; } set { iron = value; } }

    private float ironModifier;
    public float  IronModifier
    { get { return ironModifier; } set { ironModifier = value; } }


    private string currentClass;
    public string  CurrentClass
    { get { return currentClass; } set { currentClass = value; } }


    public playerModel(playerData PlayerData)
    {
        playerData = PlayerData;

        //setting playerData scriptableObj values to this script
        maxBlood = playerData.maxBlood;
        maxIron = playerData.maxIron;
        maxHealth = playerData.maxHealth;
        maxStamina = playerData.maxStamina;
        blood = playerData.startingBlood;
        iron = playerData.startingIron;
        health = playerData.startingHealth;
        stamina = playerData.startingStamina;
        currentClass = playerData.startingClass;
    }
}
