using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 cameraDirection;
    public CharacterController controller;
    public Camera camera;
    public playerInput inputModule;
    [Header("Weapon System")]
    public weapon heldWeapon;
    public weapon[] weapons;
    public movementAbility movementAbility;
    [Header("Stats")]
    public float movementSpeed = 5;
    public float groundedAcceleration = 10;
    public float aerialAcceleration = 2.5f;
    public float jumpHeight = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraUpdate();
        MovementUpdate();

        foreach (weapon w in weapons)
        {
            w.gameObject.SetActive(w == heldWeapon);
            w.transform.position = transform.position;
            w.transform.rotation = transform.rotation;
        }
        if (heldWeapon != null)
        {
            heldWeapon.HeldUpdate(this);
        }
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
        camera.transform.rotation = Quaternion.Euler(cameraDirection.x, transform.eulerAngles.y, 0);
    }

    void MovementUpdate()
    {
        movementAbility.MovementUpdate(this);

        Vector2 leftStick = inputModule.GetMovementInput().normalized;
        if (controller.isGrounded)
        {
            velocity = Vector3.Lerp(velocity, transform.TransformDirection(leftStick.x * movementSpeed, velocity.y, leftStick.y * movementSpeed), Time.deltaTime * groundedAcceleration);
            velocity.y = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpHeight;
            }
        }
        else
        {
            velocity = Vector3.Lerp(velocity, transform.TransformDirection(leftStick.x * movementSpeed, velocity.y, leftStick.y * movementSpeed), Time.deltaTime * aerialAcceleration);
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
