using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    private GameObject player;
    
    private PlayerController pControl;
    private playerInput pInput;
    private WeaponController weaponControl;
    
    private GameObject playerHud;
    private GameObject pauseMenu;
    private GameObject victoryScreen;
    private GameObject deathScreen;

    [SerializeField] private GameObject NatureWizardHat;
    [SerializeField] private GameObject BloodWizardHat;
    [SerializeField] private GameObject MetalWizardHat;

    private float pHealth;
    private float pHealthMax;
    private string pClass;
    private float pBlood;
    private float pBloodMax;
    private float pIron;
    private float pIronMax;
    private float pCurrentAmmo;
    private float pMaxAmmo;

    private bool isPaused = false;
    private bool freezeOverride = false;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pControl = player.GetComponent<PlayerController>();
        pInput = player.GetComponent<playerInput>();
        weaponControl = player.GetComponentInChildren<WeaponController>();

        pHealth = pControl.GetHealth();
        pHealthMax = pControl.GetMaxHealth();
        pClass = pControl.GetCurrentClass();
        pBlood = pControl.GetBlood();
        pBloodMax = pControl.GetMaxBlood();
        pIron = pControl.GetIron();
        pIronMax = pControl.GetMaxIron();
        pCurrentAmmo = weaponControl.LoadedAmmo;
        pMaxAmmo = weaponControl.MagSize;
        
        
        pControl.playerDeath += OnDeath;
        pControl.healthChange += OnHealthChange;

        pInput.MetalMagic += OnMetalClass;
        pInput.NatureMagic += OnNatureClass;
        pInput.BloodMagic += OnBloodClass;
        
        pInput.gamePaused += onPause;
        
        Time.timeScale = 1f;
        ActiveUI(playerHud);
        isPaused = false;
        freezeOverride = false;
    }

    private void OnDisable()
    {
        pControl.playerDeath -= OnDeath;
        pControl.healthChange -= OnHealthChange;
            
        pInput.gamePaused -= onPause;
    }

    private void onPause(object sender, EventArgs e) // Pause Event
    {
        if (!isPaused && !freezeOverride)
        {
            Time.timeScale = 0f;
            ActiveUI(pauseMenu);
            isPaused = true;
        }
        else if (isPaused && !freezeOverride)
        {
            Time.timeScale = 1f;
            ActiveUI(playerHud);
            isPaused = false;
        }
    }

    // Victory Screen - Win Condition required
    
    private void OnDeath(object sender, EventArgs e) // Death event
    {
        freezeOverride = true;
        Time.timeScale = 0f;
        ActiveUI(deathScreen);
    }

    private void ActiveUI(GameObject activeUI) // Disables all UI and re-enables intended UI
    {
        playerHud.SetActive(false);
        pauseMenu.SetActive(false);
        victoryScreen.SetActive(false);
        deathScreen.SetActive(false);
        activeUI.SetActive(true);
    }

    private void OnHealthChange(object sender, EventArgs e) // Update UI on health change
    {
        pHealth = pControl.GetHealth();
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo);
    }

    private void OnMetalClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = pControl.GetCurrentClass();
        pCurrentAmmo = weaponControl.LoadedAmmo;
        pMaxAmmo = weaponControl.MagSize;
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo);
    }
    private void OnNatureClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = pControl.GetCurrentClass();
        pCurrentAmmo = weaponControl.LoadedAmmo;
        pMaxAmmo = weaponControl.MagSize;
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo);
    }
    private void OnBloodClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = pControl.GetCurrentClass();
        pCurrentAmmo = weaponControl.LoadedAmmo;
        pMaxAmmo = weaponControl.MagSize;
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo);
    }

    private void OnWeaponShoot(object sender, EventArgs e)
    {
        // Event on bullet fire
        // Lower current ammo by 1
    }
    
    private void RedrawHUD(float health, string currentClass, float ammo, float maxAmmo) // Updates UI all at once
    {
        //Change health vs max health
        //Change class hat
        //Change weapon based on chosen class
        //Change blood vs max blood
        //Change iron vs max iron
    }
    
    
}
