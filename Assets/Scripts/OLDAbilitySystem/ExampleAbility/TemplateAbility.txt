using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateAbility : AbilityController  //all abilities NEED to inherit the abilityController the name should be different e.g. for a nature primary it should be called naturePrimary
{
    //Launcelot Raven

    //Effected enemies effects
    float damage;
    float bleedDamage;
    float bleedDuration;
    float physicsPower;  //this is for visual effects e.g. pushing enemies away

    //resources buffs
    float bloodGain;
    float metalGain;

    //health buffs
    float healthGain;

    //some of these variables may not be needed for the ability you are doing so they may not be need to be included
    //you may also need more components for your ability these can be added if youd like but they MUST BE within your ability prefab

    //accessing values like health and resources have not been implemented yet so leave comments for where this is needed

    //ability data scriptable object. This should be referenced through the editor
    [SerializeField] AbilityData abilityData;

    private void OnEnable()
    {
        //IMPORTANT this is required for your ability to work.
        AbilityController.primary += ability;  //this can be primary or seconday depends on what ability you are programming

        damage = abilityData.damage;
        bleedDamage = abilityData.bleedDamage;
        bleedDuration = abilityData.bleedDuration;
        physicsPower = abilityData.physicsPower;

        bloodGain = abilityData.bloodGain;
        metalGain = abilityData.metalGain;

        healthGain = abilityData.healthGain;
    }

    public void ability(object sender, EventArgs e)
    {
        //this is where your ability code will go 
        Debug.Log("It works!!!");
    }

    private void OnDisable()
    {
        //IMPORTANT this is required.
        AbilityController.primary -= ability;
    }
}
