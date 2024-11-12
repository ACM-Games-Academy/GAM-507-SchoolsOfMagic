using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardBurstAbility : movementAbility
{
    private bool hasForwardBurst;

    public override void MovementUpdate(movementController player, MovementModel movementModel)
    {
        base.MovementUpdate(player, movementModel);

        if (player.controller.isGrounded)
        {
            hasForwardBurst = true;
        }
    }

    public override void Jump(movementController player, MovementModel movementModel)
    {
        base.Jump(player, movementModel);

        if (hasForwardBurst && !player.controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            hasForwardBurst = false;
            player.Velocity = transform.TransformDirection(0, movementModel.JumpHeight, movementModel.ForwardBurstSpeed);
        }
    }
}
