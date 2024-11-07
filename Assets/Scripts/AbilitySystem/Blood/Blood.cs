using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;


public class Blood : MagicBase
{
    private playerInput playerInput;
    private playerController controller;

    public const MagicType magicType = MagicType.Blood;
    private MagicController magicController;
    GameObject primaryPrefab;

   [SerializeField] private BloodPrimaryData primaryData;

    bool primaryCooldown;

    //Constructs a new Metal object
    public Blood(MagicController _magicController)
    {
        magicController = _magicController;
        _magicController.TryGetPrefab("Exsanguination", out primaryPrefab);

        playerController playerController = this.GetComponent<playerController>();
        playerInput = new playerInput();

        playerInput.primaryAbil += primaryFired;
    }

    // Called when Metal is equipped in MagicController object
    public override void Equip()
    {
       primaryCooldown = false;
    }

    //Called when Metal is unequipped in a MagicController object
    public override void Unequip()
    {

    }

    //Called when Metal is updated in a MagicController object
    public override void MagicUpdate()
    {
            
    }

    private void primaryFired(object sender, EventArgs e)
    {
        if (primaryCooldown == true)
        {
            GameObject.Instantiate(primaryPrefab, transform);
            StartCoroutine(coolDownTimer(primaryData.cooldown));
            primaryCooldown = false;
        }       
    }

    private IEnumerator coolDownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        primaryCooldown = true;
    }
}

