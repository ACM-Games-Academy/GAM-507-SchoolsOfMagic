using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Composites;
using UnityEditor;
using Magic;
using TMPro;
using JetBrains.Annotations;

public class playerController : MonoBehaviour
{
    //Events
    public event EventHandler playerDeath;

    //runtime data and input 
    private playerModel model;
    playerInput input;

    //player data
    [SerializeField] playerData data;

    //the currently active buff list
    List<newBuff> buffList; 

    //value types
    public enum ValueType { Health, Iron, Blood }

    // Start is called before the first frame update
    void Awake()
    {
        model = new playerModel(data);
        input = new playerInput();
        input.Initialise();
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

        input.Disable();
    }
    
    public void giveDamage(float amount)
    {
        float processedDamage = amount * model.DmgModifier;
        model.Health -= processedDamage;

        if (model.Health < 0)
        {
            onPlayerDeath(EventArgs.Empty);
        }
    }

    public IEnumerator addHealthModT(float modifier, float time)
    {
        float increasedMaxHealth = model.MaxHealth * modifier;
        float increasedHealth = model.Health * modifier;

        model.MaxHealth += increasedMaxHealth;
        model.Health += increasedHealth;

        yield return new WaitForSeconds(time);

        model.MaxHealth -= model.MaxHealth * (increasedMaxHealth / model.MaxHealth);
        model.Health -= model.Health * (increasedHealth / model.Health);
    }

    public float GetHealth() { return model.Health; }
    public float GetMaxHealth() { return model.MaxHealth; }
    public float GetBlood() { return model.Blood; }
    public float GetMaxBlood() { return model.MaxBlood; }
    public float GetIron() { return model.Iron; }
    public float GetMaxIron() { return model.MaxIron; }
    public string GetCurrentClass() { return model.CurrentClass; }

    public void SetMaxBlood(float amount)
    {
        model.MaxBlood = amount;
    }
    public void SetIron(float amount)
    {
        model.Iron = amount;
    }
    public void SetMaxIron(float amount)
    {
        model.MaxIron = amount;
    }

    public void AddReduceValue(ValueType type, float addedValue, bool maxValue)
    {
        switch (type)
        {
            case ValueType.Health:
                if (!maxValue) { model.Health = model.Health + addedValue; }
                else { model.MaxHealth = model.MaxHealth + addedValue; }
                break;
            case ValueType.Iron:
                if (!maxValue) { model.Iron = model.Iron + addedValue; }
                else { model.MaxIron = addedValue; }
                break;
            case ValueType.Blood:
                if (!maxValue) { model.Blood = model.Blood + addedValue; }
                else { model.MaxBlood = model.MaxBlood + addedValue; }
                break;          
            default:
                Debug.LogError("playerController: invalid buff type");
                break;
        }
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



    public enum buffType { Health, Dmg, Blood, Iron };
    public newBuff AddBuff(buffType type, float modifier)
    {
        newBuff buff = new newBuff(newBuff.buffType.Health, modifier, this);

        switch (type)
        {
            case buffType.Health:
                buff = new newBuff(newBuff.buffType.Health, modifier, this);
                break;
            case buffType.Blood:
                buff = new newBuff(newBuff.buffType.Blood, modifier, this);
                break;
            case buffType.Iron:
                buff = new newBuff(newBuff.buffType.Iron, modifier, this);
                break;
            case buffType.Dmg:
                buff = new newBuff(newBuff.buffType.Iron, modifier, this);
                break;
            default:
                Debug.LogError("playerController: invalid buff type");
                break;
        }

        buffList.Add(buff);
        return buff;
    }

    public void removeBuff(newBuff buff)
    {
        buff.removeBuff();
        buffList.Remove(buff);
    }



    public class newBuff
    {
        playerModel model;

        public enum buffType { Health, Dmg, Blood, Iron };
        public buffType type;
        private float modifier;
        private float startingValue;
        private float startingMaxValue;
        private float valueIncrease;
        private float maxValueIncrease;

        public newBuff(buffType Type, float Modifier, playerController controller)
        {
            modifier = Modifier;
            model = controller.model;
            

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

            model.Health += valueIncrease;
            model.MaxHealth += maxValueIncrease;
        }

        void DmgBuff()
        {
            model.DmgModifier = -modifier;
        }

        void bloodBuff()
        {
            startingValue = model.Blood;
            valueIncrease = model.Blood * modifier;

            valueIncrease = model.Blood * modifier;
            maxValueIncrease = model.Blood * modifier;

            model.Blood += valueIncrease;
            model.MaxBlood += maxValueIncrease;
        }

        void ironBuff()
        {
            startingValue = model.Iron;
            valueIncrease = model.Iron * modifier;

            valueIncrease = model.Iron * modifier;
            maxValueIncrease = model.Iron * modifier;

            model.Iron += valueIncrease;
            model.MaxIron += maxValueIncrease;
        }

        public void removeBuff()
        {
            switch (type)
            {
                case buffType.Health:
                    model.MaxHealth -= model.MaxHealth * (maxValueIncrease / model.MaxHealth);
                    model.Health -= model.Health * (valueIncrease / model.Health);
                    break;
                case buffType.Blood:
                    model.MaxHealth -= model.MaxHealth * (maxValueIncrease / model.MaxHealth);
                    model.Health -= model.Health * (valueIncrease / model.Health);
                    break;
                case buffType.Iron:
                    model.MaxHealth -= model.MaxHealth * (maxValueIncrease / model.MaxHealth);
                    model.Health -= model.Health * (valueIncrease / model.Health);
                    break;
                case buffType.Dmg:
                    model.DmgModifier += modifier;
                    break;
                default:
                    return;
            }
        }
    }
}

