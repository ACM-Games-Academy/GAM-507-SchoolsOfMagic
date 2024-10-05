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

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        playerMovement = controls.Player.Move;
        playerLook = controls.Player.Camera;

        playerMovement.Enable();
        playerLook.Enable();
    }

    public Vector2 getCameraInput()
    {
        return cameraInput;
    }
    
    public Vector2 GetMovementInput()
    {
        return movementInput;   
    }
    
    
    // Update is called once per frame
    void Update()
    {
        cameraInput = playerLook.ReadValue<Vector2>();
        movementInput = playerMovement.ReadValue<Vector2>();
        
        Debug.Log("Camera movement is: " + cameraInput);
        //Debug.Log("General movement is: " + movementInput);
    }
}
