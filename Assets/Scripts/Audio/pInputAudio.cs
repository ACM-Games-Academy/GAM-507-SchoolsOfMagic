using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class playerInputAudio : MonoBehaviour
{
    inputManager input;

    private void OnEnable()
    {   
        input.NatureMagic += natureClass;
        input.MetalMagic += metalClass;
        input.BloodMagic += bloodClass;
        input.ArcaneMagic += arcaneClass;
    }

    private void metalClass(object sender, EventArgs e)
    {
        
    }

    private void natureClass(object sender, EventArgs e)
    {
      
    }

    private void bloodClass(object sender, EventArgs e)
    {
        
    }

    private void arcaneClass(object sender, EventArgs e)
    {
        
    }

    private void OnDisable()
    {
        input.NatureMagic -= natureClass;
        input.MetalMagic -= metalClass;
        input.BloodMagic -= bloodClass;
        input.ArcaneMagic -= arcaneClass;
    }
}
