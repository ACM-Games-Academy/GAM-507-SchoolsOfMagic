using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class naturePassiveAbility : passiveBase
{
    [SerializeField] passiveAbilityData passiveAbilityData;
    PlayerController controller;

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
        controller = GetComponent<PlayerController>();
        detect = GetComponentInChildren<SphereCollider>();
        
        
        detect.radius = detectRadius;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {  
        if (other.gameObject.tag == "Water")
        {
            controller.AddBuff(PlayerController.buffType.Health, additionalHealth, "NaturePassiveHealth", true);
            controller.AddBuff(PlayerController.buffType.Dmg, additionalHealth, "NaturePassiveDmg", true);
            //healthBuff = controller.AddBuff(playerController.buffType.Health, additionalHealth);
            //dmgBuff = controller.AddBuff(playerController.buffType.Dmg, damageReduction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            controller.removeBuff("NaturePassiveHealth");
            controller.removeBuff("NaturePassiveDmg");
            //controller.removeBuff(healthBuff);
            //controller.removeBuff(dmgBuff);
        }
    }
}
