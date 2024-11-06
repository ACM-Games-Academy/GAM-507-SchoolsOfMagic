using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerModel : MonoBehaviour
{
    //this just holds data for the player 
    [SerializeField] playerData playerData;

    //these are the runtime variables. besides the max values they will be read and written during runtime
    private float maxBlood;
    private float maxIron;
    private float blood;
    private float iron;

    private float maxHealth;
    public float MaxHealth
    { get { return maxHealth; } set { maxHealth = value; } }
        
    private float health;

    private float damageReductionBuff;
    public float DamageReductionBuff
    { get { return damageReductionBuff; } set { damageReductionBuff = value; } } 

    private float maxStamina;
    private float stamina;

    [SerializeField] private string currentClass;
    public string CurrentClass {get { return currentClass; } set { currentClass = value; } }

    void Awake()
    {
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

    //stamina methods

    public float[] getStamina()
    {
        return new float[] { stamina , maxStamina};
    }

    public void reduceStamina(float amount)
    {
        if (stamina - amount < 0)
        {
            stamina = 0;
        }
        else
        {
            stamina -= amount;
        }
    }

    //blood methods

    public float getBlood()
    {
        return blood;
    }

    public void reduceBlood(float amount)
    {
        if (blood - amount < 0)
        {
            blood = 0;
        }
        else
        {
            blood -= amount;
        }
    }

    public void addBlood(float amount)
    {
        if(blood + amount > maxBlood)
        {
            blood = maxBlood;
        }
        else
        {
            blood += amount;
        }
    }

    //iron methods

    public float getIron()
    {
        return iron;
    }

    public void reduceIron(float amount)
    {
        if (iron - amount < 0)
        {
            iron = 0;
        }
        else
        {
            iron -= amount;
        }
    }

    public void addIron(float amount)
    {
        if (iron + amount > maxIron)
        {
            iron = maxIron;
        }
        else
        {
            iron += amount;
        }
    }

    //health methods

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float amount)
    {
        health = amount;
    }

    public void reduceHealth(float amount)  //my understanding is someone wouldnt want to reduce their own health so i guess this is fine being somewhat insecure for now?
    {                                       //we'll probably end up using events for the rest anyway maybe?
        if (health - amount < 0)
        {
            health = 0;           
        }
        else
        {
            health -= amount;
        }
    }

    public void IncreaseHealth(float amount)
    {
        if (health + amount > maxHealth)
        {
            Debug.Log("PlayerModel IncreaseHealth: exceeds maximum health");
            health = maxHealth;
            return;
        }
        else
        {
            health += amount;
        }
    }
}
