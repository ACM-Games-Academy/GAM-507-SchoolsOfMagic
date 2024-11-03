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
    
    public AK.Wwise.State bloodState;
    public AK.Wwise.State natureState;
    public AK.Wwise.State metalState;
    public AK.Wwise.State arcaneState;

    public CharacterController controller;
    private AK.Wwise.RTPC health = null;
    private float speed = 0f;
    private float timer = 0f;
    private Vector3 lastPos = Vector3.zero;
    
    private void Awake()
    {
        playerStats = GetComponent<playerModel>;
    }
    
    private void Update()
    {
        health.SetValue(player, playerStats.getHealth);
        timer = Mathf.Max(timer, timer - Time.deltaTime);
    }

    private void FixedUpdate()
    {
        speed = (transform.position - lastPos).magnitude;
        if (speed > 0f && timer <= 0 && controller.isGrounded)
        {
            footsteps.Post();
            timer = 0.31415926535;
        }

        lastPos = transform.position;
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
        metalState.SetValue();
    }

    private void natureClass(object sender, EventArgs e)
    {
        natureState.SetValue();
    }

    private void bloodClass(object sender, EventArgs e)
    {
        bloodState.SetValue();
    }

    private void arcaneClass(object sender, EventArgs e)
    {
        arcaneState.SetValue();
    }

    private void OnDisable()
    {
        input.NatureMagic -= natureClass;
        input.MetalMagic -= metalClass;
        input.BloodMagic -= bloodClass;
        input.ArcaneMagic -= arcaneClass;
    }
}
