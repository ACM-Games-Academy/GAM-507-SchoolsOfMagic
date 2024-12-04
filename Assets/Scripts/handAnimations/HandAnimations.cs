using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HandAnimations : MonoBehaviour
{
    Animator handAnim;
    playerInput input;

    WeaponBase currentWeapon;
    WeaponBase previousWeapon;

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

    public void GunAnimInit(WeaponBase startingWeapon)
    {
        currentWeapon = startingWeapon;
        previousWeapon = startingWeapon;
    }

    private void ClassChange(object sender, EventArgs e)
    {
        handAnim.SetTrigger("ChangeGun");
    }

    private void AbilityFire(object sender, EventArgs e)
    {
        handAnim.SetTrigger("AbilityFire");
    }

    public void SwitchGuns()
    {
        previousWeapon.gameObject.SetActive(false);
        currentWeapon.gameObject.SetActive(true);
    }

    public void SetNextWeapon(WeaponBase nextWeapon)
    {
        previousWeapon = currentWeapon;
        currentWeapon = nextWeapon;
    }

    public void SetInactiveGun()
    {
        currentWeapon.GetGunModel().SetActive(false);
        currentWeapon.SetActiveShooting(false);
    }

    public void SetActiveGun()
    {
        currentWeapon.GetGunModel().SetActive(true);
        currentWeapon.SetActiveShooting(true);
    }
}