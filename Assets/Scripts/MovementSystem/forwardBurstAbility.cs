using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardBurstAbility : movementAbility
{
    private bool hasForwardBurst;

    public override void MovementUpdate(playerMovement player)
    {
        base.MovementUpdate(player);

        if (player.controller.isGrounded)
        {
            hasForwardBurst = true;
        }
    }

    public override void Jump(playerMovement player)
    {
        base.Jump(player);

        if (hasForwardBurst && !player.controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            hasForwardBurst = false;
            player.Velocity = transform.TransformDirection(0, player.getStats().jumpHeight, player.getStats().movementSpeed * 2.5f);
        }
    }
}
