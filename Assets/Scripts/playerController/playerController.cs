using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class playerController : MonoBehaviour
{
    //this is the event it will send in the event the player dies
    public event EventHandler playerDeath;

    //this where the player will access runtime variables
    [SerializeField] playerModel playerModel;
    [SerializeField] playerInput playerInput;

    private float cooldownTimer;

    // Start is called before the first frame update
    void OnEnable()
    {
        //sends out debug if playerdata script isnt present
        if (playerModel == null)
        {
            Debug.Log("Needs playermodel");
        }

        playerInput.NatureMagic += natureClass;
        playerInput.MetalMagic += metalClass;
        playerInput.BloodMagic += bloodClass;
        playerInput.ArcaneMagic += arcaneClass;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checking if the player has died
        deathCheck();
    }

    //this checks whether the player has died or not. health = 0
    private void deathCheck()
    {
        if (playerModel.getHealth() <= 0)
        {
            Debug.Log("You have died");
            onPlayerDeath(EventArgs.Empty);
        }
    }
    
    public void giveDamage(float amount)
    {
        float processedDamage = amount - (amount * playerModel.DamageReductionBuff);
        playerModel.reduceHealth(processedDamage);
    }

    private void onPlayerDeath(EventArgs e)
    {
        playerDeath.Invoke(this, e);
    }

    private void updateClass(string name)
    {
        playerModel.CurrentClass = name;
    }

    private void metalClass(object sender, EventArgs e)
    {
        playerModel.CurrentClass = "Metal";
    }

    private void natureClass(object sender, EventArgs e)
    {
        playerModel.CurrentClass = "Nature";
    }

    private void bloodClass(object sender, EventArgs e)
    {
        playerModel.CurrentClass = "Blood";
    }

    private void arcaneClass(object sender, EventArgs e)
    {
        playerModel.CurrentClass = "Arcane";
    }

    public playerModel getPlayerModel()
    {
        return playerModel;
    }

    public playerInput getPlayerInput()
    {
        return playerInput;
    }
}
