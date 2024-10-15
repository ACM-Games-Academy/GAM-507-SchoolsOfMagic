using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpAbility : movementAbility
{
    public bool hasDoubleJump;

    public override void MovementUpdate(playerMovement player)
    {
        base.MovementUpdate(player);

        Vector2 leftStick = player.inputModule.GetMovementInput().normalized;

        if (player.controller.isGrounded)
        {
            hasDoubleJump = true;
        }

        if (hasDoubleJump && !player.controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            hasDoubleJump = false;
            player.velocity = transform.TransformDirection(leftStick.x * player.movementSpeed, player.jumpHeight, leftStick.y * player.movementSpeed);
        }
    }
}
