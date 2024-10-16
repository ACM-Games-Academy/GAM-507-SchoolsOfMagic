using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpAbility : movementAbility
{
    private bool hasDoubleJump;

    public override void MovementUpdate(playerMovement player)
    {
        base.MovementUpdate(player);

        if (player.controller.isGrounded)
        {
            hasDoubleJump = true;
        }
    }

    public override void Jump(playerMovement player)
    {
        base.Jump(player);

        Vector2 leftStick = player.inputModule.GetMovementInput().normalized;

        if (hasDoubleJump && !player.controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            hasDoubleJump = false;
            player.Velocity = transform.TransformDirection(leftStick.x * player.getStats().movementSpeed, player.getStats().jumpHeight, leftStick.y * player.getStats().movementSpeed);
        }
    }
}
