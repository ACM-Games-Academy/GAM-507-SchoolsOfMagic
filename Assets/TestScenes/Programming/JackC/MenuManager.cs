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
    
    private GameObject playerHud;
    private GameObject pauseMenu;
    private GameObject victoryScreen;
    private GameObject deathScreen;

    [SerializeField] private GameObject NatureWizardHat;
    [SerializeField] private GameObject BloodWizardHat;
    [SerializeField] private GameObject MetalWizardHat;

    private bool isPaused = false;
    private bool freezeOverride = false;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pControl = player.GetComponent<PlayerController>();
        pInput = player.GetComponent<playerInput>();
        
        pControl.playerDeath += OnDeath;
        pControl.healthChange += OnHealthChange;
        
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

    private void onPause(object sender, EventArgs e)
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
    
    private void OnDeath(object sender, EventArgs e)
    {
        freezeOverride = true;
        Time.timeScale = 0f;
        ActiveUI(deathScreen);
    }

    private void ActiveUI(GameObject activeUI)
    {
        playerHud.SetActive(false);
        pauseMenu.SetActive(false);
        victoryScreen.SetActive(false);
        deathScreen.SetActive(false);
        activeUI.SetActive(true);
    }

    private void OnHealthChange(object sender, EventArgs e)
    {
        var _health = pControl.GetHealth();
        RedrawHUD(_health);
    }
    
    private void RedrawHUD(float health)
    {
        
    }
    
    
}
