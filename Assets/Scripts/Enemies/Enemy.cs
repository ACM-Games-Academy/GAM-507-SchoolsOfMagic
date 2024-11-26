using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    private bool isArmoured;
    public bool IsArmoured
    { get { return isArmoured; } }

    private float health;
    public float Health
    { get { return health; } }

    private bool isBleeding;
    public bool IsBleeding
    { get { return isBleeding; } }

    public void GiveDamage(float damage, bool isStaggerable)
    {
        if (isStaggerable)
        {
            GiveStagger();
        }

        health -= damage;

        if (health < 0)
        {
            EnemyDeath();
        }
    }

    public void GiveBleeding(float time, float damagePerSec, GameObject bleedingEffect)
    {
        isBleeding = true;
        StartCoroutine(Bleeding(time, damagePerSec, bleedingEffect));
    }

    protected void EnemyInitiate(float initHealth, bool initIsArmoured)
    {
        //this is where you input all the variables from the enemies scriptableObj for their values 
        //this needs to be ran first otherwise the enemy will not have any health
        health = initHealth;
        isArmoured = initIsArmoured;
    }

    protected virtual void GiveStagger()
    {
        //do something specific to the enemy when staggered
        //this will be overriden in the specific enemy class
    }

    protected virtual void EnemyDeath()
    {
        //do something when the enemy dies
        //this will be overriden in the specific enemy class
    }

    // Reset the bosse's health
    public void ResetHealth(float newHealth)
    {
        health = newHealth;
    }


    private IEnumerator Bleeding(float time ,float damagePerSec, GameObject bleedingEffect)
    {
        for (float i = 0; i < time;)
        {
            GiveDamage(damagePerSec * Time.deltaTime, false);            
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(bleedingEffect);
        isBleeding = false;
    }
}
