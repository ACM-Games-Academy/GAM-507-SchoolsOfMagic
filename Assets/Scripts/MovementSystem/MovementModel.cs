using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModel
{
    private float movementSpeed;
    public float MovementSpeed
    { get { return movementSpeed; } set { movementSpeed = value; } }       

    private float groundedAcceleration;
    public float GroundedAcceleration
    { get { return groundedAcceleration;  } set { groundedAcceleration = value; } }

    private float aerialAcceleration;
    public float AerialAcceleration
    { get { return aerialAcceleration; } set { aerialAcceleration = value; } }

    private float jumpHeight;
    public float JumpHeight
    { get { return jumpHeight; } set { jumpHeight = value; } }


    private float forwardBurstSpeed;
    public float ForwardBurstSpeed
    { get { return forwardBurstSpeed; } set { forwardBurstSpeed = value; } }

    private int blinkAmount;
    public int BlinkAmount
    { get { return blinkAmount; } set { blinkAmount = value; } }

    private float bloodBoostSpeedBoostMultiplier;
    public float BloodBoostSpeedMod
    { get { return bloodBoostSpeedBoostMultiplier; } set { bloodBoostSpeedBoostMultiplier = value; } }

    public MovementModel(movementStats data)
    {
        movementSpeed = data.movementSpeed;
        groundedAcceleration = data.groundedAcceleration;
        aerialAcceleration = data.aerialAcceleration;
        jumpHeight = data.jumpHeight;

        forwardBurstSpeed = data.forwardBurstSpeed;
        blinkAmount = data.blinkAmount;
        bloodBoostSpeedBoostMultiplier = data.bloodBoostSpeedBoostMultiplier;
    }
}
