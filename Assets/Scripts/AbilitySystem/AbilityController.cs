using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{
    private PlayerControls controls;
    private InputAction primaryInput;
    private InputAction secondaryInput;

    protected static event EventHandler primary;
    protected static event EventHandler secondary;

    [SerializeField] private string currentClass;

    //these are going to hold the prefabs for the abilities
    [SerializeField] private GameObject[] naturePrefabs;
    [SerializeField] private GameObject[] metalPrefabs;
    [SerializeField] private GameObject[] bloodPrefabs;

    void onAwake()
    {
        controls = new PlayerControls();
    }

    //the ability controller needs to listen for 3 things 
    //the hat changing and the primary and secondary inputs
    //these events come directly from the playerInput script
    void OnEnable()
    {
        primaryInput = controls.Player.primaryAbility;
        secondaryInput = controls.Player.secondaryAbility;

        primaryInput.performed += test;
        secondaryInput.performed += testA;

        controls.Enable();
        primaryInput.Enable();
        secondaryInput.Enable();
    }

    // Start is called before the first frame update
    //spawning all avaiable abilites then disabling them
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //when these are triggered it will send an event and any abilities that are enabled and inherit this class 
    //will receive the event and any subcribed functions will run
    private void test(InputAction.CallbackContext a)
    {
        Debug.Log("Ability Controller: primary ability");
        primary.Invoke(this, EventArgs.Empty);
    }

    private void testA(InputAction.CallbackContext a)
    {
        Debug.Log("Ability Controller: secondary ability");
        secondary.Invoke(this, EventArgs.Empty);        
    }

    private void OnDisable()
    {
        controls.Disable();
        primaryInput.Disable();
        secondaryInput.Disable();
    }
}
