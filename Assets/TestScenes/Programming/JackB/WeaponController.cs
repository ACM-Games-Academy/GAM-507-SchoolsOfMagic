using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponPrefabs; // Array of weapon prefabs for different classes
    private WeaponBase currentWeapon;                    // The currently active weapon
    [SerializeField] private playerModel playerModel;                     // Reference to the player's data (class, resources)
    [SerializeField] private playerInput playerInput;                     // Reference to the player's input script

    private int currentClassIndex = 0;                   // To track which class is currently active

    private void OnEnable()
    {
        playerModel = GetComponent<playerModel>();       
        playerInput = GetComponent<playerInput>();       

        // Subscribe to class change events from the playerInput
        playerInput.classChangeOne += OnClassOneSelected;
        playerInput.classChangeTwo += OnClassTwoSelected;
        playerInput.classChangeThree += OnClassThreeSelected;
        playerInput.classChangeFour += OnClassFourSelected;

        // Subscribe to fire events
        playerInput.firePressed += OnFirePressed;
        playerInput.fireReleased += OnFireReleased;

        //we currently don't have the reload keys added yet

        // Initialize the correct weapon based on player's class
        InitializeWeaponForClass(playerModel.getClass());
    }

    private void InitializeWeaponForClass(string playerClass)
    {
        switch (playerClass)
        {
            case "ClassOne":
                ActivateWeapon(0);
                break;
            case "ClassTwo":
                ActivateWeapon(1);
                break;
            case "ClassThree":
                ActivateWeapon(2);
                break;
            case "ClassFour":
                ActivateWeapon(3);
                break;
            default:
                Debug.LogWarning("Unknown class: " + playerClass);
                break;
        }
    }

    private void ActivateWeapon(int weaponIndex)
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);  // Disable the previously active weapon
        }

        // Enable the new weapon
        weaponPrefabs[weaponIndex].SetActive(true);
        currentWeapon = weaponPrefabs[weaponIndex].GetComponent<WeaponBase>();
    }

    private void OnClassOneSelected(object sender, EventArgs e)
    {
        ActivateWeapon(0);
    }

    private void OnClassTwoSelected(object sender, EventArgs e)
    {
        ActivateWeapon(1);
    }

    private void OnClassThreeSelected(object sender, System.EventArgs e)
    {
        ActivateWeapon(2);
    }

    private void OnClassFourSelected(object sender, System.EventArgs e)
    {
        ActivateWeapon(3);
    }

    private void OnFirePressed(object sender, System.EventArgs e)
    {
        if (currentWeapon != null)
        {
            currentWeapon.StartFiring();  // Start firing the current weapon
        }
    }

    private void OnFireReleased(object sender, System.EventArgs e)
    {
        if (currentWeapon != null)
        {
            currentWeapon.StopFiring();   // Stop firing the current weapon
        }
    }

    private void OnDisable()
    {
        // unSubscribe to class change events from the playerInput
        playerInput.classChangeOne -= OnClassOneSelected;
        playerInput.classChangeTwo -= OnClassTwoSelected;
        playerInput.classChangeThree -= OnClassThreeSelected;
        playerInput.classChangeFour -= OnClassFourSelected;

        // unSubscribe to fire events
        playerInput.firePressed -= OnFirePressed;
        playerInput.fireReleased -= OnFireReleased;
    }
}
