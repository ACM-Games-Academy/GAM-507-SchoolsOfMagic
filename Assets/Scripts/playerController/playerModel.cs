using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playerModel : MonoBehaviour
{
    //this just holds data for the player 
    [SerializeField] playerData playerData;

    //these are the runtime variables. besides the max values they will be read and written during runtime
    private float maxBlood;
    private float maxStamina;
    private float maxHealth;
    private float blood;
    private float health;
    private float stamina;
    [SerializeField] private string currentClass;

    void Awake()
    {
        //setting playerData scriptableObj values to this script
        maxBlood = playerData.maxBlood;
        maxHealth = playerData.maxHealth;
        maxStamina = playerData.maxStamina;
        blood = playerData.startingBlood;
        health = playerData.startingHealth;
        stamina = playerData.startingStamina;
        currentClass = playerData.startingClass;
    }
    
    public string getClass()
    {
        return currentClass;
    }

    public float getBlood()
    {
        return blood;
    }

    public float getHealth()
    {
        return health;
    }

    public float getStamina()
    {
        return stamina;
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

    public void reduceHealth(float amount)  //my understanding is someone wouldnt want to reduce their own health so i guess this is fine being somewhat insecure for now?
    {                                       //we'll probably end up using events for the rest anyway maybe?
        if (blood - amount < 0)
        {
            blood = 0;
        }
        else
        {
            blood -= amount;
        }
    }

    //for things that will give buffs like more blood or health this will use events cus secure code *nerd face*
}
