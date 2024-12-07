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

    //Jake Ash - Added unique value for reducing y & z sensitivity
    [SerializeField] float reduceY;
    [SerializeField] float reduceZ;

    public CharacterController controller;

    [SerializeField] private Camera cameraComponent;
    public playerInput inputModule;

    [SerializeField] private movementAbility currentMovement;
    [SerializeField] private movementAbility natureMovement;
    [SerializeField] private movementAbility bloodMovement;
    [SerializeField] private movementAbility metalMovement;
    [Header("Stats")]

    [SerializeField] private movementStats stats;

    private bool paused = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
        movementModel = new MovementModel(stats);

        initAbilties(playerController.GetCurrentClass());
        
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    private void OnEnable()
    {

        inputModule.jumpPressed += Jump;

        inputModule.NatureMagic += switchNature;
        inputModule.BloodMagic += switchBlood;
        inputModule.MetalMagic += switchMetal;
    }

    // Update is called once per frame
    void Update()
    {
        paused = GameObject.Find("MenuManager").GetComponent<MenuManager>().isPaused;
        if (!paused)
        {
            CameraUpdate();
            MovementUpdate();
        }
    }

    void CameraUpdate()
    {
        cameraDirection.y += inputModule.getCameraInput().x / reduceY;
        cameraDirection.x = Mathf.Clamp(cameraDirection.x - (inputModule.getCameraInput().y / reduceZ), -90, 90);

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
            default:
                Debug.LogWarning("Movement ability init: Invalid magic");
                break;
        }
    }

    public void AddSpeedBuffT(float speedMod, float time)
    {
        StartCoroutine(addSpeedBuffT(speedMod, time));
    }

    //this is for speed buffs this was taken from the playerController - Launcelot
    private IEnumerator addSpeedBuffT(float modifier, float time)
    {   
        float increasedSpeed = movementModel.MovementSpeed * modifier;


        movementModel.MovementSpeed += increasedSpeed;
        Debug.Log("Movement Speed: " + movementModel.MovementSpeed);

        yield return new WaitForSeconds(time);

        movementModel.MovementSpeed -= increasedSpeed;
        Debug.Log("Movement Speed After: " + movementModel.MovementSpeed);
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


    private void OnDisable()
    {
        inputModule.jumpPressed -= Jump;

        inputModule.NatureMagic -= switchNature;
        inputModule.BloodMagic -= switchBlood;
        inputModule.MetalMagic -= switchMetal;

        inputModule.Disable();
    }
}
