using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class playerInput : MonoBehaviour
{
    private PlayerControls controls;
    private InputAction playerMovement;
    private InputAction playerLook;
    private InputAction playerJump;
    private InputAction playerFire;
    private InputAction playerPrimary; //This is the Variable for the Primary Ability
    private InputAction playerSecondary; //This is the Variable for the Second Ability
    private InputAction classOne;
    private InputAction classTwo;
    private InputAction classThree;
    private InputAction classFour;
    private InputAction abilityMovement;
    private InputAction playerRun;
   
    private Vector2 movementInput;
    private Vector2 cameraInput;

    public event EventHandler primaryAbil;
    public event EventHandler secondaryAbil;

    public event EventHandler firePressed;
    public event EventHandler fireReleased;

    public event EventHandler jumpPressed;

    public event EventHandler classChangeOne;
    public event EventHandler classChangeTwo;
    public event EventHandler classChangeThree;
    public event EventHandler classChangeFour;

    public event EventHandler movingAbility;

    public event EventHandler runPressed;
    public event EventHandler runReleased;

    void Awake()
    {
        controls = new PlayerControls();    

        playerMovement = controls.Player.Move;
        playerLook = controls.Player.Camera;
        playerJump = controls.Player.Jump;
        playerFire = controls.Player.Fire;
        playerPrimary = controls.Player.primaryAbility;
        playerSecondary = controls.Player.secondaryAbility;
        classOne = controls.Player.class1;
        classTwo = controls.Player.class2;
        classThree = controls.Player.class3;
        classFour = controls.Player.class4;
        abilityMovement = controls.Player.movementAbility;
        playerRun = controls.Player.Running;

        //Whenever the player presses either primary or secondary ability, this calls a function to happen
        playerJump.started += PlayerJump;

        playerFire.started += onFirePressed;
        playerFire.canceled += onFireCancelled;

        playerPrimary.started += PrimaryAbility;
        playerSecondary.started += SecondaryAbility;

        classOne.started += ChangeClassOne;
        classTwo.started += ChangeClassTwo;
        classThree.started += ChangeClassThree;
        classFour.started += ChangeClassFour;

        abilityMovement.started += AbilityMovement;

        playerRun.started += onRunPressed;
        playerRun.canceled += onRunReleased; 
    }

    private void OnEnable()
    {
        //This enable the inputs to allow the player to perform the functions in the game
        playerMovement.Enable();
        playerLook.Enable();
        playerJump.Enable();
        playerFire.Enable();
        playerPrimary.Enable();
        playerSecondary.Enable();
        classOne.Enable();
        classTwo.Enable();
        classThree.Enable();
        classFour.Enable();
        abilityMovement.Enable();
        playerRun.Enable();
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
        onButton(EventArgs.Empty, classChangeOne);
    }

    private void ChangeClassTwo(InputAction.CallbackContext twoClass)
    {
        onButton(EventArgs.Empty, classChangeTwo);
    }

    private void ChangeClassThree(InputAction.CallbackContext threeClass)
    {
        onButton(EventArgs.Empty, classChangeThree);
    }

    private void ChangeClassFour(InputAction.CallbackContext fourClass)
    {
        onButton(EventArgs.Empty, classChangeFour);
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

    private void onButton(EventArgs e, EventHandler button)
    {
        button.Invoke(this ,e);
    }

    private void OnDisabled()
    {
        playerMovement.Disable();
        playerLook.Disable();
        playerJump.Disable();
        playerFire.Disable();
        playerPrimary.Disable();
        playerSecondary.Disable();
        classOne.Disable();
        classTwo.Disable();
        classThree.Disable();
        classFour.Disable();
        abilityMovement.Disable();
        playerRun.Disable();
    }
}
