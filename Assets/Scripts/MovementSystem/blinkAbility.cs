using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkAbility : movementAbility
{
    private int blinks;
    private float blinkTime;

    public override void MovementUpdate(MovementController player, MovementModel movementModel)
    {
        base.MovementUpdate(player, movementModel);

        Vector2 leftStick = player.inputModule.GetMovementInput().normalized;

        if (player.controller.isGrounded)
        {
            blinks = movementModel.BlinkAmount;
        }

        if (blinks > 0 && blinkTime <= 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            blinks--;
            blinkTime = 0.1f;
            player.Velocity = transform.TransformDirection(leftStick.x * movementModel.MovementSpeed * 5, 0, leftStick.y * movementModel.MovementSpeed * 5);
        }
        if (blinkTime > 0)
        {
            if (blinkTime - Time.deltaTime <= 0)
            {
                player.Velocity /= 5;
            }
            blinkTime -= Time.deltaTime;

            
        }
    }
}
