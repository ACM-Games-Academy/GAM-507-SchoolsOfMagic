using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInput : MonoBehaviour
{
    private PlayerControls controls;
    private InputAction playerMovement;
    private InputAction playerLook;
    private InputAction playerJump;
    private InputAction playerFire;
    private InputAction playerPrimary; //This is the Variable for the Primary Ability
    private InputAction playerSecondary; //This is the Variable for the Second Ability
    private InputAction natureMagic;
    private InputAction bloodMagic;
    private InputAction metalMagic;
    private InputAction abilityMovement;
    private InputAction playerRun;
    private InputAction playerReload;
    private InputAction playerInteract;
    private InputAction pauseGame;

    private Vector2 movementInput;
    private Vector2 cameraInput;
    private bool runInput;

    public event EventHandler primaryAbil;
    public event EventHandler secondaryAbil;

    public event EventHandler firePressed;
    public event EventHandler fireReleased;

    public event EventHandler jumpPressed;

    public event EventHandler NatureMagic;
    public event EventHandler BloodMagic;
    public event EventHandler MetalMagic;

    public event EventHandler movingAbility;

    public event EventHandler runPressed;
    public event EventHandler runReleased;

    public event EventHandler reloadPressed;
    public event EventHandler interactPressed;


    public event EventHandler gamePaused;

    bool canChangeMagic;
    [SerializeField] float magicChangeCooldown;

    public void Awake()
    {
        controls = new PlayerControls();    

        playerMovement = controls.Player.Move;
        playerLook = controls.Player.Camera;
        playerJump = controls.Player.Jump;
        playerFire = controls.Player.Fire;
        playerPrimary = controls.Player.primaryAbility;
        playerSecondary = controls.Player.secondaryAbility;
        natureMagic = controls.Player.natureMagic;
        bloodMagic = controls.Player.bloodMagic;
        metalMagic = controls.Player.metalMagic;
        abilityMovement = controls.Player.movementAbility;
        playerRun = controls.Player.Running;
        playerReload = controls.Player.Reload;
        playerInteract = controls.Player.Interact;
        pauseGame = controls.Player.Pause;


        //Whenever the player presses either primary or secondary ability, this calls a function to happen
        playerJump.started += PlayerJump;

        playerFire.started += onFirePressed;
        playerFire.canceled += onFireCancelled;

        playerPrimary.started += PrimaryAbility;
        playerSecondary.started += SecondaryAbility;

        natureMagic.performed += ChangeClassOne;
        bloodMagic.performed += ChangeClassTwo;
        metalMagic.performed += ChangeClassThree;

        abilityMovement.started += AbilityMovement;

        playerRun.started += onRunPressed;
        playerRun.canceled += onRunReleased;

        playerReload.started += onReloadPressed;
        playerInteract.started += onInteractPressed;

        pauseGame.started += onGamePause;
   
        //This enable the inputs to allow the player to perform the functions in the game
        playerMovement.Enable();
        playerLook.Enable();
        playerJump.Enable();
        playerFire.Enable();
        playerPrimary.Enable();
        playerSecondary.Enable();
        natureMagic.Enable();
        bloodMagic.Enable();
        metalMagic.Enable();
        abilityMovement.Enable();
        playerRun.Enable();
        playerReload.Enable();
        playerInteract.Enable();
        pauseGame.Enable();

        canChangeMagic = true;
    }

    public Vector2 getCameraInput()
    {
        cameraInput = playerLook.ReadValue<Vector2>();
        return cameraInput;
    }
    
    public Vector2 GetMovementInput()
    {
        movementInput = playerMovement.ReadValue<Vector2>();
        return movementInput;
    }

    public bool GetRunInput()
    {
        runInput = playerRun.ReadValue<float>() > 0;
        return runInput;
    }

    //This functions only calls when one of the abilities are performed from the input action asset
    private void PrimaryAbility(InputAction.CallbackContext primary)
    {
        onButton(EventArgs.Empty, primaryAbil);
    }

    private void SecondaryAbility(InputAction.CallbackContext secondary)
    {
        onButton(EventArgs.Empty, secondaryAbil);
    }

    private void PlayerJump(InputAction.CallbackContext jump)
    {
        onButton(EventArgs.Empty, jumpPressed);
    }

    private void onFirePressed(InputAction.CallbackContext fire)
    {
        onButton(EventArgs.Empty, firePressed);
    }

    private void onFireCancelled(InputAction.CallbackContext fire)
    {
        onButton(EventArgs.Empty, fireReleased);
    }

    private void ChangeClassOne(InputAction.CallbackContext oneClass)
    {
        if (canChangeMagic)
        {
            onButton(EventArgs.Empty, NatureMagic);
            StartCoroutine(MagicChangeCooldown(magicChangeCooldown));
        }       
    }

    private void ChangeClassTwo(InputAction.CallbackContext twoClass)
    {
        if (canChangeMagic)
        {
            onButton(EventArgs.Empty, BloodMagic);
            StartCoroutine(MagicChangeCooldown(magicChangeCooldown));
        }    
    }

    private void ChangeClassThree(InputAction.CallbackContext threeClass)
    {
        if (canChangeMagic)
        {
            onButton(EventArgs.Empty, MetalMagic);
            StartCoroutine(MagicChangeCooldown(magicChangeCooldown));
        }
    }

    private void AbilityMovement(InputAction.CallbackContext movement)
    {
        onButton(EventArgs.Empty, movingAbility);
    }

    private void onRunPressed(InputAction.CallbackContext run)
    {
        onButton(EventArgs.Empty, runPressed);
    }

    private void onRunReleased(InputAction.CallbackContext run)
    {
        onButton(EventArgs.Empty, runReleased);
    }

    private void onReloadPressed(InputAction.CallbackContext reload)
    {
        onButton(EventArgs.Empty, reloadPressed);
    }

    private void onInteractPressed(InputAction.CallbackContext reload)
    {
        onButton(EventArgs.Empty, interactPressed);
    }

    private void onGamePause(InputAction.CallbackContext pause)
    {
        onButton(EventArgs.Empty, gamePaused);
    }

    private void onButton(EventArgs e, EventHandler button)
    {
        if (button != null)
        {
            button.Invoke(this, e);
        }
    }

    private IEnumerator MagicChangeCooldown(float time)
    {
        canChangeMagic = false;
        yield return new WaitForSeconds(time);
        canChangeMagic = true;
    }

    public void Disable()
    {
        playerMovement.Disable();
        playerLook.Disable();
        playerJump.Disable();
        playerFire.Disable();
        playerPrimary.Disable();
        playerSecondary.Disable();
        natureMagic.Disable();
        bloodMagic.Disable();
        metalMagic.Disable();
        abilityMovement.Disable();
        playerRun.Disable();
        playerReload.Disable();
        playerInteract.Disable();
    }
}
