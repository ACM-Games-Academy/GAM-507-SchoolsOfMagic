using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodBoost : movementAbility
{
    public override void MovementUpdate(playerMovement player)
    {
        base.MovementUpdate(player);

        int layersToIgnore = ~LayerMask.GetMask("Player");

        RaycastHit hit;
        if (player.controller.isGrounded && Physics.Raycast(transform.position + (Vector3.down * 0.95f), transform.TransformDirection(Vector3.down), out hit, 1, layersToIgnore))
        {
            print(hit.transform.gameObject.name);
            if (hit.transform.gameObject.name.Contains("Blood"))
            {
                player.controller.Move(new Vector3(player.Velocity.x * 0.5f, player.Velocity.y, player.Velocity.z * 0.5f) * Time.deltaTime);
            }
        }
    }
}
