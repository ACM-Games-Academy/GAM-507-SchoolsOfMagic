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

    private bool isPaused = false;
    private bool freezeOverride = false;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pControl = player.GetComponent<PlayerController>();
        pInput = player.GetComponent<playerInput>();
        pControl.playerDeath += OnDeath;
        pInput.gamePaused += onPause;
        Time.timeScale = 1f;
        UI_Reset(playerHud);
        isPaused = false;
        freezeOverride = false;
    }

    private void onPause(object sender, EventArgs e)
    {
        if (!isPaused && !freezeOverride)
        {
            Time.timeScale = 0f;
            UI_Reset(pauseMenu);
            isPaused = true;
        }
        else if (isPaused && !freezeOverride)
        {
            Time.timeScale = 1f;
            UI_Reset(playerHud);
            isPaused = false;
        }
    }

    // Victory Screen - Win Condition required
    
    private void OnDeath(object sender, EventArgs e)
    {
        freezeOverride = true;
        Time.timeScale = 0f;
        UI_Reset(deathScreen);
    }

    private void UI_Reset(GameObject activeUI)
    {
        playerHud.SetActive(false);
        pauseMenu.SetActive(false);
        victoryScreen.SetActive(false);
        deathScreen.SetActive(false);
        activeUI.SetActive(true);
    }
    
    
}
