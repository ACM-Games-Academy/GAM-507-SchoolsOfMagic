using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class playerInputAudio : MonoBehaviour
{
    playerInput input;
    public GameObject player; 
    public AK.Wwise.Event footsteps;
    public AK.Wwise.Event heartbeat;
    public AK.Wwise.State wizardState;
    private AK.Wwise.RTPC health = null;
    
    private void Awake()
    {
        playerStats = GetComponent<playerController>;
    }
    
    private void Update()
    {
        health.SetValue(player, playerStats.health)
    }
    private void OnEnable()
    {   
        input.NatureMagic += natureClass;
        input.MetalMagic += metalClass;
        input.BloodMagic += bloodClass;
        input.ArcaneMagic += arcaneClass;
    }

    private void metalClass(object sender, EventArgs e)
    {
        AkSoundEngine.SetState(WizardStates, Metal);
    }

    private void natureClass(object sender, EventArgs e)
    {
        AkSoundEngine.SetState(WizardStates, Nature);
    }

    private void bloodClass(object sender, EventArgs e)
    {
        AkSoundEngine.SetState(WizardStates, Blood);
}

    private void arcaneClass(object sender, EventArgs e)
    {
        AkSoundEngine.SetState(WizardStates, Arcane);
}

    private void OnDisable()
    {
        input.NatureMagic -= natureClass;
        input.MetalMagic -= metalClass;
        input.BloodMagic -= bloodClass;
        input.ArcaneMagic -= arcaneClass;
    }
}
