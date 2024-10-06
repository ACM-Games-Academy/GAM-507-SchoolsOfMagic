using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{
    public PlayerControls controls;
    private InputAction primaryInput;
    private InputAction secondaryInput;

    protected static event EventHandler primary;
    protected static event EventHandler secondary;

    [Header("these are for testing")]
    [SerializeField] private string currentClass;
    public bool tempButton;
    [Header("---------------")]

    //these are going to hold the prefabs for the abilities
    [SerializeField] private GameObject[] naturePrefabs;
    [SerializeField] private GameObject[] metalPrefabs;
    [SerializeField] private GameObject[] bloodPrefabs;

    //the ability controller needs to listen for 2 things 
    //primary and secondary inputs
    //these events come directly from the playerInput script
    void OnEnable()
    {
        controls = new PlayerControls();

        primaryInput = controls.Player.primaryAbility;
        secondaryInput = controls.Player.secondaryAbility;

        primaryInput.performed += primaryA;
        secondaryInput.performed += SecondaryA;

        primaryInput.Enable();
        secondaryInput.Enable();
    }

    // Start is called before the first frame update
    //spawning all avaiable abilites then disabling them
    void Start()
    {
        disableAbilites(bloodPrefabs);
        disableAbilites(metalPrefabs);
        disableAbilites(naturePrefabs);
    }

    // Update is called once per frame
    void Update()
    {
        if (tempButton == true)
        {
            tempButton = false;
            changeWizClass(currentClass);
        }
    }

    //when these are triggered it will send an event and any abilities that are enabled and inherit this class 
    //will receive the event and any subcribed functions will run
    private void primaryA(InputAction.CallbackContext a)
    {
        Debug.Log("Ability Controller: primary ability");
        primary.Invoke(this, EventArgs.Empty);
    }

    private void SecondaryA(InputAction.CallbackContext a)
    {
        Debug.Log("Ability Controller: secondary ability");
        secondary.Invoke(this, EventArgs.Empty);        
    }

    private void disableAbilites(GameObject[] abilArray)
    {
        foreach (GameObject abil in abilArray)
        {
            abil.SetActive(false);
        }

    }

    public void changeWizClass(string wizClass)
    {
        //this method will check waht class its being changed to and depending if its a valid class or not it will disable 
        //all posible prefabs and enabnle the ones it needs

        if (wizClass == "Nature")
        {
            disableAbilites(metalPrefabs);
            disableAbilites(bloodPrefabs);
            
            foreach (GameObject prefabAbil in naturePrefabs)
            {
                prefabAbil.SetActive(true);
            }

            currentClass = wizClass;
        }

        else if (wizClass == "Metal")
        {
            disableAbilites(naturePrefabs);
            disableAbilites(bloodPrefabs);

            foreach (GameObject prefabAbil in metalPrefabs)
            {
                prefabAbil.SetActive(true);
            }

            currentClass = wizClass;
        }

        else if (wizClass == "Blood")
        {
            disableAbilites(metalPrefabs);
            disableAbilites(naturePrefabs);

            foreach (GameObject prefabAbil in bloodPrefabs)
            {
                prefabAbil.SetActive(true);
            }

            currentClass = wizClass;
        }
        else
        {
            Debug.Log("Invalid Class");
        }
    }

    private void OnDisable()
    {
        primaryInput.Disable();
        secondaryInput.Disable();
    }
}
