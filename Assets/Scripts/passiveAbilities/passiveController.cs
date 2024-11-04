using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveController : MonoBehaviour
{
    [SerializeField] playerInput playerInput;

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

    private void changeMagicNature(object sender, EventArgs e)
    {
        currentPassive = naturePassive;
    }

    private void changeMagicBlood(object sender, EventArgs e)
    {
        currentPassive = bloodPassive;
    }

    private void changeMagicMetal(object sender, EventArgs e)
    {
        currentPassive = metalPassive;
    }

    private void changeMagicArcane(object sender, EventArgs e)
    {
        currentPassive = arcanePassive;
    }

    private void Update()
    {
        currentPassive.updatePassive();
    }

    private void OnDisable()
    {
        playerInput.NatureMagic -= changeMagicNature;
        playerInput.BloodMagic -= changeMagicBlood;
        playerInput.MetalMagic -= changeMagicMetal;
        playerInput.ArcaneMagic -= changeMagicArcane;
    }
}
