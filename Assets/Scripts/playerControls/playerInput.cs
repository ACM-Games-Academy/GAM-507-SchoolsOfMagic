using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInput : MonoBehaviour
{
    private PlayerControls controls;
    private InputAction playerMovement;
    private InputAction playerLook;
   
    private Vector2 movementInput;
    private Vector2 cameraInput;

    void OnEnable()
    {
        controls = new PlayerControls();    

        playerMovement = controls.Player.Move;
        playerLook = controls.Player.Camera;

        playerMovement.Enable();
        playerLook.Enable();
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

    private void OnDisabled()
    {
        playerMovement.Disable();
        playerLook.Disable();
    }
}
