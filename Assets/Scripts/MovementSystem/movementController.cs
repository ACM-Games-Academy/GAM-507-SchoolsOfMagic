using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movementController : MonoBehaviour
{
    public PlayerController playerController;
    private MovementModel movementModel;

    private Vector3 velocity;
    public Vector3 Velocity 
    { get { return velocity; } set { velocity = value; } }

    private Vector3 cameraDirection;

    public CharacterController controller;

    [SerializeField] private Camera cameraComponent;
    public playerInput inputModule;

    [SerializeField] private movementAbility currentMovement;
    [SerializeField] private movementAbility natureMovement;
    [SerializeField] private movementAbility bloodMovement;
    [SerializeField] private movementAbility metalMovement;
    [SerializeField] private movementAbility arcaneMovement;
    [Header("Stats")]

    [SerializeField] private movementStats stats;
    

    // Start is called before the first frame update
    void Start()
    {
        movementModel = new MovementModel(stats);

        initAbilties(playerController.GetCurrentClass());
    }

    private void OnEnable()
    {

        inputModule.jumpPressed += Jump;

        inputModule.NatureMagic += switchNature;
        inputModule.BloodMagic += switchBlood;
        inputModule.MetalMagic += switchMetal;
        inputModule.ArcaneMagic += switchArcane;
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
            currentMovement.MovementUpdate(this, movementModel);
        }

        Vector2 leftStick = inputModule.GetMovementInput().normalized;
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity = Vector3.Lerp(velocity, transform.TransformDirection(leftStick.x * movementModel.MovementSpeed, velocity.y, leftStick.y * movementModel.MovementSpeed), Time.deltaTime * movementModel.GroundedAcceleration);
            velocity.y = -0.5f;
        }
        else
        {
            velocity = Vector3.Lerp(velocity, transform.TransformDirection(leftStick.x * movementModel.MovementSpeed, velocity.y, leftStick.y * movementModel.MovementSpeed), Time.deltaTime * movementModel.AerialAcceleration);
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void Jump(object sender, System.EventArgs e)
    {
        if (currentMovement != null)
        {
            currentMovement.Jump(this, movementModel);
        }
        if (controller.isGrounded)
        {
            velocity.y = movementModel.JumpHeight;
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

    //this is for speed buffs this was taken from the playerController - Launcelot
    public IEnumerator addSpeedModT(float modifier, float time)
    {   
        float increasedSpeed = movementModel.MovementSpeed * modifier;


        movementModel.MovementSpeed += increasedSpeed;

        yield return new WaitForSeconds(time);

        movementModel.MovementSpeed -= movementModel.MovementSpeed * (increasedSpeed / movementModel.MovementSpeed);
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
