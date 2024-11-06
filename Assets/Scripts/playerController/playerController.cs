using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Composites;
using UnityEditor;
using Magic;
using TMPro;

public class playerController : MonoBehaviour
{
    //Events
    public event EventHandler playerDeath;

    //runtime data and input 
    protected playerModel model;
    playerInput input;

    //player data
    [SerializeField] playerData data;

    // Start is called before the first frame update
    void Start()
    {
        model = new playerModel(data);
        input = new playerInput();
    }
    void OnEnable()
    {
        //sends out debug if playerdata script isnt present
        if (data == null)
        {
            Debug.Log("Needs playermodel");
        }

        input.NatureMagic += natureClass;
        input.MetalMagic += metalClass;
        input.BloodMagic += bloodClass;
        input.ArcaneMagic += arcaneClass;
    }

    void OnDisable()
    {
        input.NatureMagic -= natureClass;
        input.MetalMagic -= metalClass;
        input.BloodMagic -= bloodClass;
        input.ArcaneMagic -= arcaneClass;
    }
    
    public void giveDamage(float amount)
    {
        float processedDamage = amount * model.DmgModifier;
        model.Health =- processedDamage;

        if (model.Health < 0)
        {
            onPlayerDeath(EventArgs.Empty);
        }
    }

    public IEnumerator addHealthModT(float modifier, float time)
    {
        float increasedMaxHealth = model.MaxHealth * modifier;
        float increasedHealth = model.Health * modifier;

        model.MaxHealth =+ increasedMaxHealth;
        model.Health =+ increasedHealth;

        yield return new WaitForSeconds(time);

        model.MaxHealth =- model.MaxHealth * (increasedMaxHealth/model.MaxHealth);
        model.Health =- model.Health * (increasedHealth/model.Health);
    }

    private void onPlayerDeath(EventArgs e)
    {
        playerDeath.Invoke(this, e);
    }

    private void metalClass(object sender, EventArgs e)
    {
        model.CurrentClass = "Metal";
    }

    private void natureClass(object sender, EventArgs e)
    {
        model.CurrentClass = "Nature";
    }

    private void bloodClass(object sender, EventArgs e)
    {
        model.CurrentClass = "Blood";
    }

    private void arcaneClass(object sender, EventArgs e)
    {
        model.CurrentClass = "Arcane";
    }
}

//this is specficially for buffs
//you can apply a new a buff then you can remove the buff by calling the remove method of the buff
public class newBuff : playerController
{
    public enum buffType { Health, Dmg, Blood, Iron };
    public buffType type;
    private float modifier;
    private float startingValue;
    private float startingMaxValue;
    private float valueIncrease;
    private float maxValueIncrease;

    public newBuff(buffType Type, float Modifier)
    {
        modifier = Modifier;

        buffType type = new buffType();
        type = Type;

        switch (type)
        {
            case buffType.Health:
                healthBuff();
                break;
            case buffType.Blood:
                bloodBuff();
                break;
            case buffType.Dmg:
                DmgBuff();
                break;
            case buffType.Iron:
                ironBuff();
                break;
            default:
                return;
        }
    }

    void healthBuff()
    {
        startingValue = model.Health;
        startingMaxValue = model.MaxHealth;

        valueIncrease = model.Health * modifier;
        maxValueIncrease = model.MaxHealth * modifier;

        model.Health =+ valueIncrease;
        model.MaxHealth =+ maxValueIncrease;
    }

    void DmgBuff()
    {
        model.DmgModifier =- modifier;
    }

    void bloodBuff()
    {
        startingValue = model.Blood;
        valueIncrease = model.Blood * modifier;

        valueIncrease = model.Blood * modifier;
        maxValueIncrease = model.Blood * modifier;

        model.Blood =+ valueIncrease;
        model.MaxBlood =+ maxValueIncrease;
    }

    void ironBuff()
    {
        startingValue = model.Iron;
        valueIncrease = model.Iron * modifier;

        valueIncrease = model.Iron * modifier;
        maxValueIncrease = model.Iron * modifier;

        model.Iron =+ valueIncrease;
        model.MaxIron =+ maxValueIncrease;
    }

    public void removeBuff()
    {
        switch(type)
        {
            case buffType.Health:
                model.MaxHealth =- model.MaxHealth * (maxValueIncrease/model.MaxHealth);
                model.Health =- model.Health * (valueIncrease/model.Health);
                break;
            case buffType.Blood:
                model.MaxHealth =- model.MaxHealth * (maxValueIncrease/model.MaxHealth);
                model.Health =- model.Health * (valueIncrease/model.Health);
                break;
            case buffType.Iron:
                model.MaxHealth =- model.MaxHealth * (maxValueIncrease/model.MaxHealth);
                model.Health =- model.Health * (valueIncrease/model.Health);
                break;
            case buffType.Dmg:
                model.DmgModifier =+ modifier;
                break;
            default:
                return;
        }
    }
}
