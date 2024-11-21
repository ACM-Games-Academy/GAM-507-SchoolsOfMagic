using UnityEngine;
using System;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon prefab array \n in order of Nature, Blood, Metal, Arcane")]
    [SerializeField] private GameObject[] weaponGameobject; // Array of weapon prefabs for different classes
    private WeaponBase currentWeapon;                    // The currently active weapon
    [SerializeField] private playerInput playerInput;                     // Reference to the player's input script
    [SerializeField] private PlayerController controller;
    [SerializeField] private bool isReloading;

    private int loadedAmmo;
    private int magSize;
    public int LoadedAmmo {  get { return loadedAmmo; } }
    public int MagSize { get { return magSize; } }

    private void Awake()
    {
        
    }

    private void Start()
    {
        // Subscribe to class change events from the playerInput
        playerInput.NatureMagic += OnClassOneSelected;
        playerInput.BloodMagic += OnClassTwoSelected;
        playerInput.MetalMagic += OnClassThreeSelected;
        playerInput.ArcaneMagic += OnClassFourSelected;

        // Subscribe to fire events
        playerInput.firePressed += OnFirePressed;
        playerInput.fireReleased += OnFireReleased;

        playerInput.reloadPressed += OnReloadPressed;

        // Initialize the correct weapon based on player's class
        InitializeWeaponForClass(controller.GetCurrentClass());
    }

    private void OnEnable()
    {
        // Subscribe to class change events from the playerInput
        playerInput.NatureMagic += OnClassOneSelected;
        playerInput.BloodMagic += OnClassTwoSelected;
        playerInput.MetalMagic += OnClassThreeSelected;
        playerInput.ArcaneMagic += OnClassFourSelected;

        // Subscribe to fire events
        playerInput.firePressed += OnFirePressed;
        playerInput.fireReleased += OnFireReleased;

        //we currently don't have the reload keys added yet
        playerInput.reloadPressed += OnReloadPressed;
    }

    private void InitializeWeaponForClass(string playerClass)
    {
        switch (playerClass)
        {
            case "Nature":
                ActivateWeapon(0);
                break;
            case "Blood":
                ActivateWeapon(1);
                break;
            case "Metal":
                ActivateWeapon(2);
                break;
            case "Arcane":
                ActivateWeapon(3);
                break;
            default:
                Debug.LogWarning("Weapon Init: invalid magic " + playerClass);
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
        weaponGameobject[weaponIndex].SetActive(true);
        currentWeapon = weaponGameobject[weaponIndex].GetComponent<WeaponBase>();

        magSize = currentWeapon.WeaponStats.MagazineCapacity;
        loadedAmmo = currentWeapon.CurrentAmmo;
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

    private void OnReloadPressed(object sender, System.EventArgs e)
    {
        if (!isReloading)
        {
            ReloadCurrentGun();
        }
    }   

    private void ReloadCurrentGun()
    {
        WeaponStats weaponStats = currentWeapon.WeaponStats;
        int reloadedAmount = 0;

        //first check if the player has enough iron
        //and how much ammo the player needs. (is the magzine half loaded or only a little bit)

        int ammoDeficit = currentWeapon.WeaponStats.MagazineCapacity - currentWeapon.CurrentAmmo;

        if (controller.GetIron() <= 0)
        {;
            reloadedAmount = 0;
        }
        else if (controller.GetIron() - ammoDeficit >= 0f)
        {
            reloadedAmount = ammoDeficit;

            StartCoroutine(ReloadCoroutine(reloadedAmount));
        }
        else
        {
            reloadedAmount = Convert.ToInt32(controller.GetIron());

            StartCoroutine(ReloadCoroutine(reloadedAmount));
        }
    }

    private IEnumerator ReloadCoroutine(int reloadedAmount)
    {
        WeaponStats weaponStats = currentWeapon.WeaponStats;

        isReloading = true;
        yield return new WaitForSeconds(weaponStats.ReloadSpeed);

        //checks if the player is still holding the same gun. 
        //if not the gun won't reload
        if (weaponStats == currentWeapon.WeaponStats)
        {
            currentWeapon.AddAmmo(Convert.ToInt32(reloadedAmount));
            controller.AddReduceValue(PlayerController.ValueType.Iron, -reloadedAmount, false);
        }

        isReloading = false;
    }

    private void Update()
    {
        //dirty fix to update current ammo
        loadedAmmo = currentWeapon.CurrentAmmo;
    }

    private void OnDisable()
    {
        // unSubscribe to class change events from the playerInput
        playerInput.NatureMagic -= OnClassOneSelected;
        playerInput.BloodMagic -= OnClassTwoSelected;
        playerInput.MetalMagic -= OnClassThreeSelected;
        playerInput.ArcaneMagic -= OnClassFourSelected;

        // unSubscribe to fire events
        playerInput.firePressed -= OnFirePressed;
        playerInput.fireReleased -= OnFireReleased;

        playerInput.reloadPressed -= OnReloadPressed;
    }
}
