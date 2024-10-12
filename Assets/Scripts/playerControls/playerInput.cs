using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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

    [SerializeField] UnityEvent playerJumping;
    [SerializeField] UnityEvent primaryFire;
    [SerializeField] UnityEvent secondaryFire;
    [SerializeField] UnityEvent playerFiring;
    [SerializeField] UnityEvent classChangeOne;
    [SerializeField] UnityEvent classChangeTwo;
    [SerializeField] UnityEvent classChangeThree;
    [SerializeField] UnityEvent classChangeFour;
    [SerializeField] UnityEvent movingAbility;
    [SerializeField] UnityEvent playerRunning;

    void OnEnable()
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
        playerJump.performed += PlayerJump;
        playerFire.performed += PlayerFiring;
        playerPrimary.performed += PrimaryAbility;
        playerSecondary.performed += SecondaryAbility;
        classOne.performed += ChangeClassOne;
        classTwo.performed += ChangeClassTwo;
        classThree.performed += ChangeClassThree;
        classFour.performed += ChangeClassFour;
        abilityMovement.performed += AbilityMovement;
        playerRun.performed += PlayerRun;

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
    void PrimaryAbility(InputAction.CallbackContext primary)
    {
        primaryFire.Invoke();
    }

    void SecondaryAbility(InputAction.CallbackContext secondary)
    {
        secondaryFire.Invoke();
    }

    void PlayerJump(InputAction.CallbackContext jump)
    {
        playerJumping.Invoke();
    }

    void PlayerFiring(InputAction.CallbackContext fire)
    {
        playerFiring.Invoke();
    }

    void ChangeClassOne(InputAction.CallbackContext oneClass)
    {
        classChangeOne.Invoke();
    }

    void ChangeClassTwo(InputAction.CallbackContext twoClass)
    {
        classChangeTwo.Invoke();
    }

    void ChangeClassThree(InputAction.CallbackContext threeClass)
    {
        classChangeThree.Invoke();
    }

    void ChangeClassFour(InputAction.CallbackContext fourClass)
    {
        classChangeFour.Invoke();
    }

    void AbilityMovement(InputAction.CallbackContext movement)
    {
        movingAbility.Invoke();
    }

    void PlayerRun(InputAction.CallbackContext run)
    {
        playerRunning.Invoke();
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
