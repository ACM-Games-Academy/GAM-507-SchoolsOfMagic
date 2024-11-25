using Magic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Blood : MagicBase
{
    public AK.Wwise.Event exsangSound;
    //these need to be public so that the other abilties can access them
    private playerInput playerInput;
    private PlayerController controller;
    [SerializeField] private movementController movementController;

    public const MagicType magicType = MagicType.Blood;
    private MagicController magicController;
    GameObject primaryPrefab;

    [SerializeField] private BloodPrimaryData primaryData;

    [SerializeField] private bool abilityReady = true;

    //Constructs a new Metal object
    void Start()
    {
        magicController = transform.GetComponent<MagicController>();
        magicController.TryGetPrefab("Exsanguination", out primaryPrefab);

        playerInput = transform.GetComponentInParent<playerInput>();
        controller = transform.GetComponentInParent<PlayerController>();
    }

    // Called when Metal is equipped in MagicController object
    public override void Equip()
    {
        playerInput.primaryAbil += primaryFired;
    }


    //Called when Metal is unequipped in a MagicController object
    public override void Unequip()
    {
        playerInput.primaryAbil -= primaryFired;
    }

    //Called when Metal is updated in a MagicController object
    public override void MagicUpdate()
    {
            
    }

    private void primaryFired(object sender, EventArgs e)
    {
        if (abilityReady == true)
        {
            GameObject instance = GameObject.Instantiate(primaryPrefab, transform.position, transform.rotation, transform);
            instance.GetComponent<Exsanguination>().InitAbil(controller, movementController, exsangSound);
            StartCoroutine(coolDownTimer(primaryData.cooldown));
            abilityReady = false;
        }

        Debug.Log("Blood primary");
    }

    private IEnumerator coolDownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        abilityReady = true;
    }
}

