using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Vector3 velocity;
    public Vector3 Velocity 
    { get { return velocity; } set { velocity = value; } }
    private Vector3 cameraDirection;
    public CharacterController controller;
    [SerializeField]
    private Camera cameraComponent;
    public playerInput inputModule;
    [SerializeField]
    private movementAbility movementAbility;
    [Header("Stats")]
    [SerializeField]
    private movementStats stats;

    private void OnEnable()
    {
        inputModule.jumpPressed += Jump;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraUpdate();
        MovementUpdate();
    }

    void CameraUpdate()
    {
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
        if (movementAbility != null)
        {
            movementAbility.MovementUpdate(this);
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
        if (movementAbility != null)
        {
            movementAbility.Jump(this);
        }
        if (controller.isGrounded)
        {
            velocity.y = stats.jumpHeight;
        }
    }

    public movementStats getStats()
    {
        return stats;
    }
}
