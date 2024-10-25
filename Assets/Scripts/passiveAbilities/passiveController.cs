using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passiveController : MonoBehaviour
{
    [SerializeField] playerInput playerInput;

    private void OnEnable()
    {
        playerInput.NatureMagic += changeMagicNature;
        playerInput.BloodMagic += changeMagicBlood;
        playerInput.MetalMagic += changeMagicMetal;
        playerInput.ArcaneMagic += changeMagicArcane;
    }

    private void changeMagicNature(object sender, EventArgs e)
    {

    }

    private void changeMagicBlood(object sender, EventArgs e)
    {

    }

    private void changeMagicMetal(object sender, EventArgs e)
    {

    }

    private void changeMagicArcane(object sender, EventArgs e)
    {

    }

    private void OnDisable()
    {
        playerInput.NatureMagic -= changeMagicNature;
        playerInput.BloodMagic -= changeMagicBlood;
        playerInput.MetalMagic -= changeMagicMetal;
        playerInput.ArcaneMagic -= changeMagicArcane;
    }
}
