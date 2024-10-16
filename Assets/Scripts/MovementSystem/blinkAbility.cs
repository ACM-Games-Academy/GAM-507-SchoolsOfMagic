using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkAbility : movementAbility
{
    public int blinks;
    private float blinkTime;

    public override void MovementUpdate(playerMovement player)
    {
        base.MovementUpdate(player);

        Vector2 leftStick = player.inputModule.GetMovementInput().normalized;

        if (player.controller.isGrounded)
        {
            blinks = 2;
        }

        if (blinks > 0 && blinkTime <= 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            blinks--;
            blinkTime = 0.1f;
            player.velocity = transform.TransformDirection(leftStick.x * player.stats.movementSpeed * 5, 0, leftStick.y * player.stats.movementSpeed * 5);
        }
        if (blinkTime > 0)
        {
            if (blinkTime - Time.deltaTime <= 0)
            {
                player.velocity /= 5;
            }
            blinkTime -= Time.deltaTime;
        }
    }
}
