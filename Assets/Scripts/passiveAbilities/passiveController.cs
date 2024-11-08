using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveController : MonoBehaviour
{
    
    
    [SerializeField] playerInput playerInput;
    [SerializeField] PlayerController controller;

    private passiveBase currentPassive;
    [SerializeField] passiveBase naturePassive;
    [SerializeField] passiveBase bloodPassive;
    [SerializeField] passiveBase metalPassive;
    [SerializeField] passiveBase arcanePassive;

    private void OnEnable()
    {
        playerInput.NatureMagic += changeMagicNature;
        playerInput.BloodMagic += changeMagicBlood;
        playerInput.MetalMagic += changeMagicMetal;
        playerInput.ArcaneMagic += changeMagicArcane;
    }

    private void Start()
    {
        
        if (controller.GetCurrentClass() == "Nature")
        {
            currentPassive = naturePassive;
            currentPassive.enabled = true;
        }
        else if (controller.GetCurrentClass() == "Metal")
        {
            currentPassive = metalPassive;
            currentPassive.enabled = true;
        }
        else if (controller.GetCurrentClass() == "Blood")
        {
            currentPassive = bloodPassive;
            currentPassive.enabled = true;
        }
        else if (controller.GetCurrentClass() == "Arcane")
        {
            currentPassive = arcanePassive;
            currentPassive.enabled = true;
        }
        else
        {
            currentPassive = null;
        }
    }

    private void changeMagicNature(object sender, EventArgs e)
    {
        currentPassive.enabled = false;
        currentPassive = naturePassive;
        currentPassive.enabled = true;
    }

    private void changeMagicBlood(object sender, EventArgs e)
    {
        currentPassive.enabled = false;
        currentPassive = bloodPassive;
        currentPassive.enabled = true;
    }

    private void changeMagicMetal(object sender, EventArgs e)
    {
        currentPassive.enabled = false;
        currentPassive = metalPassive;
        currentPassive.enabled = true;
    }

    private void changeMagicArcane(object sender, EventArgs e)
    {
        currentPassive.enabled = false;
        currentPassive = arcanePassive;
        currentPassive.enabled = true;
    }


    private void OnDisable()
    {
        playerInput.NatureMagic -= changeMagicNature;
        playerInput.BloodMagic -= changeMagicBlood;
        playerInput.MetalMagic -= changeMagicMetal;
        playerInput.ArcaneMagic -= changeMagicArcane;
    }
}
