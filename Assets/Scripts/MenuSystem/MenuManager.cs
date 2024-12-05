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
    public WeaponController weaponControl;
    private BossEnemy bossEnemy;
    
    [SerializeField] private GameObject playerHud;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject deathScreen;

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
    public string pClass;
    private float pBlood;
    private float pBloodMax;
    private float pIron;
    private float pIronMax;
    public float pCurrentAmmo;
    public float pMaxAmmo;

    private float bHealth;
    private float bHealthMax;

    private bool isPaused = false;
    private bool freezeOverride = false;
    
    [Header("Boss Audio Events")]
    public AK.Wwise.Event bossDeathSound;
    public AK.Wwise.Event bossIdleStop;
    public AK.Wwise.Event bossIdle;

    private void Start()
    {
       
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pControl = player.GetComponent<PlayerController>();
        pInput = player.GetComponent<playerInput>();
        weaponControl = player.GetComponentInChildren<WeaponController>();

        bossEnemy = GameObject.Find("Boss").GetComponent<BossEnemy>();

        pHealth = pControl.GetHealth();
        pHealthMax = pControl.GetMaxHealth();
        pClass = pControl.GetCurrentClass();
        pBlood = pControl.GetBlood();
        pBloodMax = pControl.GetMaxBlood();
        pIron = pControl.GetIron();
        pIronMax = pControl.GetMaxIron();
        pCurrentAmmo = weaponControl.LoadedAmmo;
        pMaxAmmo = weaponControl.MagSize;

        bHealth = bossEnemy.Health;
        bHealthMax = bossEnemy.Health;
        
        HealthBar.GetComponent<Slider>().maxValue = pHealthMax;
        IronBar.GetComponent<Slider>().maxValue = pIronMax;
        BloodBar.GetComponent<Slider>().maxValue = pBloodMax;
        BossHealth.GetComponent<Slider>().maxValue = bHealthMax;
        
        pControl.playerDeath += OnDeath;
        pControl.healthChange += OnHealthChange;
        pControl.ironChange += OnIronChange;
        pControl.bloodChange += OnBloodChange;

        pInput.MetalMagic += OnMetalClass;
        pInput.NatureMagic += OnNatureClass;
        pInput.BloodMagic += OnBloodClass;
        
        pInput.gamePaused += OnPause;

        bossEnemy.enemyHealthChange += OnBossHealthChange;
        bossEnemy.bossDeath += OnVictory;

        weaponControl.reloadFired += OnWeaponReloaded;
        
        Time.timeScale = 1f;
        ActiveUI(playerHud);
        isPaused = false;
        freezeOverride = false;

        weaponControl.currentWeapon.gunFired += OnWeaponShoot;

        bossIdle.Post(this.gameObject);

        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }

    private void OnDisable()
    {
        pControl.playerDeath -= OnDeath;
        pControl.healthChange -= OnHealthChange;
        pControl.ironChange -= OnIronChange;
        pControl.bloodChange -= OnBloodChange;

        pInput.MetalMagic -= OnMetalClass;
        pInput.NatureMagic -= OnNatureClass;
        pInput.BloodMagic -= OnBloodClass;
        
        pInput.gamePaused -= OnPause;

        bossEnemy.enemyHealthChange -= OnBossHealthChange;
        bossEnemy.bossDeath -= OnVictory;
        
        weaponControl.reloadFired -= OnWeaponReloaded;
        weaponControl.currentWeapon.gunFired -= OnWeaponShoot;
        
    }

    private void OnPause(object sender, EventArgs e) // Pause Event
    {
        PauseNav();
    }

    public void PauseNav()
    {
        if (!isPaused && !freezeOverride)
        {
            ActiveUI(pauseMenu);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
        }
        else if (isPaused && !freezeOverride)
        {
            ActiveUI(playerHud);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
            StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
        }
        else
        {
            Debug.Log("Pause blocked - Freeze override: " + freezeOverride);
        }
    }

    // Victory Screen - Win Condition required
    private void OnVictory(object sender, EventArgs e)
    {
        freezeOverride = true;
        bossIdleStop.Post(this.gameObject);
        bossDeathSound.Post(this.gameObject);
        ActiveUI(victoryScreen);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
    
    private void OnDeath(object sender, EventArgs e) // Death event
    {
        freezeOverride = true;
        ActiveUI(deathScreen);
        Cursor.lockState = CursorLockMode.None;
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
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }

    private void OnMetalClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = "Metal";
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }
    private void OnNatureClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = "Nature";
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }
    private void OnBloodClass(object sender, EventArgs e) // Update UI on class change
    {
        pClass = "Blood";
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }

    private void OnIronChange(object sender, EventArgs e)
    {
        pIron = pControl.GetIron();
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }
    
    private void OnBloodChange(object sender, EventArgs e)
    {
        pBlood = pControl.GetBlood();
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }
    
    private void OnWeaponShoot(object sender, EventArgs e)
    {
        pCurrentAmmo = weaponControl.currentWeapon.CurrentAmmo;
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));

        Debug.Log("Current Ammo: " +  pCurrentAmmo);
    }

    private void OnWeaponReloaded(object sender, EventArgs e)
    {
        pCurrentAmmo = weaponControl.currentWeapon.CurrentAmmo;
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }

    private void OnBossHealthChange(object sender, EventArgs e)
    {
        bHealth = bossEnemy.Health;
        StartCoroutine(RedrawHUD(pHealth, pClass, pCurrentAmmo, pMaxAmmo, pIron, pBlood, bHealth));
    }
    
    /// <summary>
    /// This method updates all UI elements in one call, causing the expensive Update() function to be
    /// obsolete for this purpose. UI all redraws at the same time, so doing it all in one go is worth it.
    /// </summary>
    /// <param name="health"></param> The players current health
    /// <param name="currentClass"></param> The new class the player has swapped to
    /// <param name="ammo"></param> The ammo currently in the players weapon -- Obsolete
    /// <param name="maxAmmo"></param> The maximum ammo capacity of the players weapon -- Obsolete
    /// <param name="iron"></param> The amount of Iron the player has accumulated 
    /// <param name="blood"></param> The amount of Blood the player has accumulated
    
    private IEnumerator RedrawHUD(float health, string currentClass, float ammo, float maxAmmo, float iron, float blood, float bossHealth) // Updates UI all at once
    {
        weaponControl.currentWeapon.gunFired -= OnWeaponShoot;
        yield return new WaitForEndOfFrame();
    
        if (currentClass == "Metal") //Change class hat and Change weapon based on chosen class
        {
            NatureWizardHat.SetActive(false);
            BloodWizardHat.SetActive(false);
            MetalWizardHat.SetActive(true);
        }
        else if (currentClass == "Nature") //Change class hat and Change weapon based on chosen class
        {
            NatureWizardHat.SetActive(true);
            BloodWizardHat.SetActive(false);
            MetalWizardHat.SetActive(false);
        }
        else if (currentClass == "Blood") //Change class hat and Change weapon based on chosen class
        {
            NatureWizardHat.SetActive(false);
            BloodWizardHat.SetActive(true);
            MetalWizardHat.SetActive(false);
        }

        AmmoCurrent.GetComponent<TMP_Text>().text = weaponControl.currentWeapon.CurrentAmmo.ToString();
        AmmoMax.GetComponent<TMP_Text>().text = weaponControl.MagSize.ToString();

        IronBar.GetComponent<Slider>().value = iron; //Change iron vs max iron
        BloodBar.GetComponent<Slider>().value = blood; //Change blood vs max blood

        HealthBar.GetComponent<Slider>().value = health;

        BossHealth.GetComponent<Slider>().value = bossHealth;

        weaponControl.currentWeapon.gunFired += OnWeaponShoot;
    }

}
