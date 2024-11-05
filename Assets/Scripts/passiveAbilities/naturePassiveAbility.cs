using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class naturePassiveAbility : passiveBase
{
    [SerializeField] passiveAbilityData passiveAbilityData;
    playerController controller;
    playerModel model;

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
        model = controller.getPlayerModel();
        
        detect.radius = detectRadius;
    }

    private void Update()
    {
        Debug.Log("MaxHealth: " + model.MaxHealth);
        Debug.Log("CurrentHeath: " + model.getHealth());
        Debug.Log("DamageReduction: " + model.DamageReductionBuff);
    }

    private void OnTriggerEnter(Collider other)
    {  
        if (other.gameObject.tag == "Water")
        {
            model.MaxHealth = model.MaxHealth * (1 + additionalHealth);
            model.setHealth(model.getHealth() * (1 + additionalHealth));
            model.DamageReductionBuff = damageReduction;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {          
            model.MaxHealth = model.MaxHealth / (1 + additionalHealth);
            model.setHealth(model.getHealth() / (1 + additionalHealth));
            model.DamageReductionBuff = 0f;
        }
    }
}
