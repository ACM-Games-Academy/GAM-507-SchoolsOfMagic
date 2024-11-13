using System;
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
    private InputAction arcaneMagic;
    private InputAction abilityMovement;
    private InputAction playerRun;

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
    public event EventHandler ArcaneMagic;

    public event EventHandler movingAbility;

    public event EventHandler runPressed;
    public event EventHandler runReleased;

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
        arcaneMagic = controls.Player.arcaneMagic;
        abilityMovement = controls.Player.movementAbility;
        playerRun = controls.Player.Running;



        //Whenever the player presses either primary or secondary ability, this calls a function to happen
        playerJump.started += PlayerJump;

        playerFire.started += onFirePressed;
        playerFire.canceled += onFireCancelled;

        playerPrimary.started += PrimaryAbility;
        playerSecondary.started += SecondaryAbility;

        natureMagic.performed += ChangeClassOne;
        bloodMagic.performed += ChangeClassTwo;
        metalMagic.performed += ChangeClassThree;
        arcaneMagic.performed += ChangeClassFour;

        abilityMovement.started += AbilityMovement;

        playerRun.started += onRunPressed;
        playerRun.canceled += onRunReleased;
   
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
        arcaneMagic.Enable();
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
        onButton(EventArgs.Empty, NatureMagic);
    }

    private void ChangeClassTwo(InputAction.CallbackContext twoClass)
    {
        onButton(EventArgs.Empty, BloodMagic);
    }

    private void ChangeClassThree(InputAction.CallbackContext threeClass)
    {
        onButton(EventArgs.Empty, MetalMagic);
    }

    private void ChangeClassFour(InputAction.CallbackContext fourClass)
    {
        onButton(EventArgs.Empty, ArcaneMagic);
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
        if (button != null)
        {
            button.Invoke(this, e);
        }
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
        arcaneMagic.Disable();
        abilityMovement.Disable();
        playerRun.Disable();
    }
}
