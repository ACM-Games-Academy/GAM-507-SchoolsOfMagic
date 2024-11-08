using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpAbility : movementAbility
{
    private bool hasDoubleJump;

    public override void MovementUpdate(movementController player, MovementModel movementModel)
    {
        base.MovementUpdate(player, movementModel);

        if (player.controller.isGrounded)
        {
            hasDoubleJump = true;
        }
    }

    public override void Jump(movementController player, MovementModel movementModel)
    {
        base.Jump(player, movementModel);

        Vector2 leftStick = player.inputModule.GetMovementInput().normalized;

        if (hasDoubleJump && !player.controller.isGrounded && Input.GetButtonDown("Jump") && player.playerController.GetIron() > 0)
        {
            player.playerController.AddReduceValue(PlayerController.ValueType.Iron, -1, false);
            hasDoubleJump = false;
            player.Velocity = transform.TransformDirection(leftStick.x * movementModel.MovementSpeed, movementModel.JumpHeight, leftStick.y * movementModel.MovementSpeed);
        }
    }
}
