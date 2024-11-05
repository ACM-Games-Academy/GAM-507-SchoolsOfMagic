using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class naturePassiveAbility : passiveBase
{
    [SerializeField] passiveAbilityData passiveAbilityData;
    playerController controller;

    float additionalHealth;
    float damageReduction;
    bool nearWater;

    private void Awake()
    {
        additionalHealth = passiveAbilityData.additionalHealth;
        damageReduction = passiveAbilityData.damageReduction;
    }

    private void Start()
    {
        controller = GetComponent<playerController>();
    }

    private void Update()
    {
        if (nearWater) 
        {
            controller.GetComponent<playerModel>().MaxHealth *= additionalHealth;
            controller.GetComponent<playerModel>().DamageReductionBuff = damageReduction;
        }

        if (!nearWater) 
        {
            controller.GetComponent<playerModel>().MaxHealth /= additionalHealth;
            controller.GetComponent<playerModel>().DamageReductionBuff = 1f;
        }
    }

    void OnTriggerEnter(Collider water)
    {
        if (water.gameObject.tag == "Water")
        {
            nearWater = true;
        }
   }

    private void OnTriggerExit(Collider other)
    {
      if(other.gameObject.tag == "Water")
        {
            nearWater = false;
        }   
    }
}
