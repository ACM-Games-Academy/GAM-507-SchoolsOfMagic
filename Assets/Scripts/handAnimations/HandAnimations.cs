using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HandAnimations : MonoBehaviour
{
    Animator handAnim;
    playerInput input;


    private void Start()
    {
        handAnim = GetComponent<Animator>();

        input = GetComponentInParent<playerInput>();

        input.NatureMagic += ClassChange;
        input.MetalMagic += ClassChange;
        input.BloodMagic += ClassChange;

        input.primaryAbil += AbilityFire;
        input.secondaryAbil += AbilityFire;
    }

    private void ClassChange(object sender, EventArgs e)
    {
        handAnim.SetTrigger("ChangeGun");
    }

    private void AbilityFire(object sender, EventArgs e)
    {
        handAnim.SetTrigger("AbilityFire");
    }
}
