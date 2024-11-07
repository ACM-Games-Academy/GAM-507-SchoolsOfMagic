using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movementController : MonoBehaviour
{
    public playerController playerController;
    private Vector3 velocity;
    public Vector3 Velocity 
    { get { return velocity; } set { velocity = value; } }

    private Vector3 cameraDirection;

    public CharacterController controller;

    [SerializeField] private Camera cameraComponent;
    public playerInput inputModule;

    private movementAbility currentMovement;
    [SerializeField] private movementAbility natureMovement;
    [SerializeField] private movementAbility bloodMovement;
    [SerializeField] private movementAbility metalMovement;
    [SerializeField] private movementAbility arcaneMovement;
    [Header("Stats")]

    [SerializeField] private movementStats stats;

    private void OnEnable()
    {       

        inputModule.jumpPressed += Jump;

        inputModule.NatureMagic += switchNature;
        inputModule.BloodMagic += switchBlood;
        inputModule.MetalMagic += switchMetal;
        inputModule.ArcaneMagic += switchArcane;
    }

    // Start is called before the first frame update
    void Start()
    {
        initAbilties(playerController.GetCurrentClass());
    }

    // Update is called once per frame
    void Update()
    {
        CameraUpdate();
        MovementUpdate();
    }

    void CameraUpdate()
    {
        //these will be temporary for the time being until we have a menu / pause menu setup
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        cameraDirection.y += inputModule.getCameraInput().x;
        cameraDirection.x = Mathf.Clamp(cameraDirection.x - inputModule.getCameraInput().y, -90, 90);

        transform.rotation = Quaternion.Euler(0, cameraDirection.y, 0);
        cameraComponent.transform.rotation = Quaternion.Euler(cameraDirection.x, transform.eulerAngles.y, 0);
    }

    void MovementUpdate()
    {
        if (currentMovement != null)
        {
            currentMovement.MovementUpdate(this);
        }

        Vector2 leftStick = inputModule.GetMovementInput().normalized;
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity = Vector3.Lerp(velocity, transform.TransformDirection(leftStick.x * stats.movementSpeed, velocity.y, leftStick.y * stats.movementSpeed), Time.deltaTime * stats.groundedAcceleration);
            velocity.y = -0.5f;
        }
        else
        {
            velocity = Vector3.Lerp(velocity, transform.TransformDirection(leftStick.x * stats.movementSpeed, velocity.y, leftStick.y * stats.movementSpeed), Time.deltaTime * stats.aerialAcceleration);
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void Jump(object sender, System.EventArgs e)
    {
        if (currentMovement != null)
        {
            currentMovement.Jump(this);
        }
        if (controller.isGrounded)
        {
            velocity.y = stats.jumpHeight;
        }
    }

    private void initAbilties(string name)
    {
        switch (name)
        {
            case "Nature":
                currentMovement = natureMovement;
                break;
            case "Blood":
                currentMovement = bloodMovement;
                break;
            case "Metal":
                currentMovement = metalMovement;
                break;
            case "Arcane":
                currentMovement = arcaneMovement;
                break;
            default:
                Debug.LogWarning("Movement ability init: Invalid magic");
                break;
        }
    }

    public movementStats getStats()
    {
        return stats;
    }

    private void switchNature(object sender, EventArgs e)
    {
        currentMovement = natureMovement;
    }

    private void switchBlood(object sender, EventArgs e)
    {
        currentMovement = bloodMovement;
    }

    private void switchMetal(object sender, EventArgs e)
    {
        currentMovement = metalMovement;
    }

    private void switchArcane(object sender, EventArgs e)
    {
        currentMovement = arcaneMovement;
    }

    private void OnDisable()
    {
        inputModule.jumpPressed -= Jump;

        inputModule.NatureMagic -= switchNature;
        inputModule.BloodMagic -= switchBlood;
        inputModule.MetalMagic -= switchMetal;
        inputModule.ArcaneMagic -= switchArcane;

        inputModule.Disable();
    }
}
