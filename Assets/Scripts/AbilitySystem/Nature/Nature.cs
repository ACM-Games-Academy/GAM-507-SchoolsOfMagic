using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Nature : MagicBase
{
    public const MagicType magicType = MagicType.Nature;
    private MagicController magicController;
    private GameObject cactusPrefab;
    [SerializeField] float primaryCooldown = 5;
    [SerializeField] float currentPrimaryCooldown;
    [SerializeField] private bool cooldown;

    playerInput playerInput;
    PlayerController controller;

    //private GameObject ironGripPrefab;

    //Constructs a new Metal object
    public void Start()
    {
        magicController = transform.GetComponent<MagicController>();
        magicController.TryGetPrefab("cactusPrefab", out cactusPrefab);

        playerInput = transform.GetComponentInParent<playerInput>();
        controller = transform.GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        currentPrimaryCooldown -= Time.deltaTime;
    }

    // Called when Metal is equipped in MagicController object
    public override void Equip()
    {   
        playerInput.primaryAbil += primaryFired;
        cooldown = false;
    }

    //Called when Metal is unequipped in a MagicController object
    public override void Unequip()
    {
        playerInput.primaryAbil -= primaryFired;
    }

    //Called when Metal is updated in a MagicController object
    public override void MagicUpdate()
    {
        //TODO: Write in input controls for Metal abilities
        //Example of how an input control and subsequent prefab instantiation may look
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    GameObject grip = Object.Instantiate(ironGripPrefab);
        //}
    }

    //this has been changed to use the current input system not the legacy version - Launcelot
    private void primaryFired(object sender, EventArgs e)
    {
        //if (!cooldown)
        if (currentPrimaryCooldown < 0)
        {
            GameObject cactus = GameObject.Instantiate(cactusPrefab, transform.position, Quaternion.identity);
            cactus.GetComponent<CactusAbility>().controller = controller;
            currentPrimaryCooldown = primaryCooldown;
            //cooldown = true;
            //StartCoroutine(startCooldown(primaryCooldown));
        }
    }

    //cooldown has changed to IEnumerator to avoid overflow errors after prolonged use - Launcelot
    private IEnumerator startCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
    }
}

