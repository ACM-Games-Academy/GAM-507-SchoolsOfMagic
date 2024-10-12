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
    private InputAction playerPrimary; //This is the Variable for the Primary Ability
    private InputAction playerSecondary; //This is the Variable for the Second Ability
   
    private Vector2 movementInput;
    private Vector2 cameraInput;

    [SerializeField] UnityEvent primaryFire;
    [SerializeField] UnityEvent secondaryFire;

    void OnEnable()
    {
        controls = new PlayerControls();    

        playerMovement = controls.Player.Move;
        playerLook = controls.Player.Camera;
        playerPrimary = controls.Player.primaryAbility;
        playerSecondary = controls.Player.secondaryAbility;

        //Whenever the player presses either primary or secondary ability, this calls a function to happen
        playerPrimary.performed += PrimaryAbility;
        playerSecondary.performed += SecondaryAbility;
        
        playerMovement.Enable();
        playerLook.Enable();
        playerPrimary.Enable();
        playerSecondary.Enable();
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

    private void OnDisabled()
    {
        playerMovement.Disable();
        playerLook.Disable();
        playerPrimary.Disable();
        playerSecondary.Disable();
    }
}
