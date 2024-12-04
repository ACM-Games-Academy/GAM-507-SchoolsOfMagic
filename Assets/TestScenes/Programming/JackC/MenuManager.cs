using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject IronBar;
    [SerializeField] private GameObject BloodBar;
    
    [SerializeField] private GameObject AmmoCurrent;
    [SerializeField] private GameObject AmmoMax;
    
    [SerializeField] private GameObject Ability;
    [SerializeField] private GameObject MovementAbility;
    
    [SerializeField] private GameObject BossHealth;
    
    
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
        
        HealthBar.GetComponent<Slider>().maxValue = pHealthMax;
        IronBar.GetComponent<Slider>().maxValue = pIronMax;
        BloodBar.GetComponent<Slider>().maxValue = pBloodMax;
        
        pControl.playerDeath += OnDeath;
        pControl.healthChange += OnHealthChange;
        pControl.ironChange += OnIronChange;
        pControl.bloodChange += OnBloodChange;

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
            ActiveUI(pauseMenu);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else if (isPaused && !freezeOverride)
        {
            ActiveUI(playerHud);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    // Victory Screen - Win Condition required
    
    private void OnDeath(object sender, EventArgs e) // Death event
    {
        freezeOverride = true;
        ActiveUI(deathScreen);
        Time.timeScale = 0f;
    }

    private void ActiveUI(GameObject activeUI) // Disables all UI and re-enables intended UI
    {
        playerHud.SetActive(false);
        pauseMenu.SetActive(false);
        victoryScreen.SetActive(false);
        deathScreen.SetActive(false);
        activeUI.SetActive(true);
    }
    
    /// <summary>
    /// UI Update functionality listed below. Whenever Anything changes, it changes the whole UI at the same time as
    /// to avoid using expensive methods such as Update, updating the UI every frame, but instead subscribing
    /// to events, and updating the UI whenever an event is called.
    /// </summary>

    private void OnHealthChange(object sender, EventArgs e) // Update UI on health change
    {
        pHealth = pControl.GetHealth();
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood);
    }

    private void OnMetalClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = pControl.GetCurrentClass();
        pCurrentAmmo = weaponControl.LoadedAmmo;
        pMaxAmmo = weaponControl.MagSize;
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood);
    }
    private void OnNatureClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = pControl.GetCurrentClass();
        pCurrentAmmo = weaponControl.LoadedAmmo;
        pMaxAmmo = weaponControl.MagSize;
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood);
    }
    private void OnBloodClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = pControl.GetCurrentClass();
        pCurrentAmmo = weaponControl.LoadedAmmo;
        pMaxAmmo = weaponControl.MagSize;
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood);
    }

    private void OnIronChange(object sender, EventArgs e)
    {
        pIron = pControl.GetIron();
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood);
    }
    
    private void OnBloodChange(object sender, EventArgs e)
    {
        pBlood = pControl.GetBlood();
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood);
    }
    
    private void OnWeaponShoot(object sender, EventArgs e)
    {
        pCurrentAmmo = weaponControl.LoadedAmmo;
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood);
    }

    private void OnWeaponReloaded(object sender, EventArgs e)
    {
        pCurrentAmmo = weaponControl.LoadedAmmo;
        RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood);
    }
    
    /// <summary>
    /// This method updates all UI elements in one call, causing the expensive Update() function to be
    /// obsolete for this purpose. UI all redraws at the same time, so doing it all in one go is worth it.
    /// </summary>
    /// <param name="health"></param> The players current health
    /// <param name="currentClass"></param> The new class the player has swapped to
    /// <param name="ammo"></param> The ammo currently in the players weapon
    /// <param name="maxAmmo"></param> The maximum ammo capacity of the players weapon
    /// <param name="iron"></param> The amount of Iron the player has accumulated 
    /// <param name="blood"></param> The amount of Blood the player has accumulated
    
    private void RedrawHUD(float health, string currentClass, float ammo, float maxAmmo, float iron, float blood) // Updates UI all at once
    {
        HealthBar.GetComponent<Slider>().value = health; //Change health vs max health
        
        if (currentClass == "Metal") //Change class hat and Change weapon based on chosen class
        {
            NatureWizardHat.SetActive(false);
            BloodWizardHat.SetActive(false);
            MetalWizardHat.SetActive(true);
            AmmoCurrent.GetComponent<TextMeshPro>().text = ammo.ToString();
            AmmoMax.GetComponent<TextMeshPro>().text = maxAmmo.ToString();
        }
        else if (currentClass == "Nature") //Change class hat and Change weapon based on chosen class
        {
            NatureWizardHat.SetActive(true);
            BloodWizardHat.SetActive(false);
            MetalWizardHat.SetActive(false);
            AmmoCurrent.GetComponent<TextMeshPro>().text = ammo.ToString();
            AmmoMax.GetComponent<TextMeshPro>().text = maxAmmo.ToString();
        }
        else if (currentClass == "Blood") //Change class hat and Change weapon based on chosen class
        {
            NatureWizardHat.SetActive(false);
            BloodWizardHat.SetActive(true);
            MetalWizardHat.SetActive(false);
            AmmoCurrent.GetComponent<TextMeshPro>().text = ammo.ToString();
            AmmoMax.GetComponent<TextMeshPro>().text = maxAmmo.ToString();
        }

        IronBar.GetComponent<Slider>().value = iron; //Change iron vs max iron
        BloodBar.GetComponent<Slider>().value = blood; //Change blood vs max blood
    }
    
    
}
