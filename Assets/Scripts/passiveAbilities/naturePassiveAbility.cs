using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class naturePassiveAbility : passiveBase
{
    [SerializeField] passiveAbilityData passiveAbilityData;
    playerController controller;

    [SerializeField]SphereCollider detect;

    float additionalHealth;
    float damageReduction;
    float detectRadius;
    bool nearWater;

    private void Awake()
    {
        additionalHealth = passiveAbilityData.additionalHealth;
        damageReduction = passiveAbilityData.damageReduction;
        detectRadius = passiveAbilityData.detectionRadius;
    }

    private void Start()
    {
        controller = GetComponent<playerController>();
        detect = GetComponentInChildren<SphereCollider>();
        
        detect.radius = detectRadius;
    }

    private void Update()
    {
        //if (nearWater) 
        //{
          //  controller.GetComponent<playerModel>().MaxHealth *= additionalHealth;
          //  controller.GetComponent<playerModel>().DamageReductionBuff = damageReduction;
        //}

        //if (!nearWater) 
        //{
          //  controller.GetComponent<playerModel>().MaxHealth /= additionalHealth;
          //  controller.GetComponent<playerModel>().DamageReductionBuff = 1f;
        //}

        Debug.Log("MaxHealth: " + controller.GetComponent<playerModel>().MaxHealth);
        Debug.Log("DamageReduction: " + controller.GetComponent<playerModel>().DamageReductionBuff);

    }

    void OnTriggerEnter(Collider water )
    {
        //the trigger is determined on if it's true or false from the sphere collider
        water.isTrigger = detect;
        if (water.gameObject.tag == "Water")
        {
            //nearWater = true;
            controller.GetComponent<playerModel>().MaxHealth *= additionalHealth;
            controller.GetComponent<playerModel>().DamageReductionBuff = damageReduction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //the trigger is determined on if it's true or false from the sphere collider
        other.isTrigger = detect;
        if(other.gameObject.tag == "Water")
        {
            //nearWater = false;
              controller.GetComponent<playerModel>().MaxHealth /= additionalHealth;
              controller.GetComponent<playerModel>().DamageReductionBuff = 1f;
        }
    }
}
