using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodBoost : movementAbility
{
    public override void MovementUpdate(MovementController player, MovementModel movementModel)
    {
        base.MovementUpdate(player, movementModel);

        int layersToIgnore = ~LayerMask.GetMask("Player");

        RaycastHit hit;
        if (player.controller.isGrounded && Physics.Raycast(transform.position + (Vector3.down * 0.95f), transform.TransformDirection(Vector3.down), out hit, 1, layersToIgnore))
        {
            print(hit.transform.gameObject.name);
            if (hit.transform.gameObject.name.Contains("Blood") || (Input.GetKey(KeyCode.LeftShift) && player.playerController.GetBlood() > 0))
            {
                if (!hit.transform.gameObject.name.Contains("Blood"))
                {
                    player.playerController.AddReduceValue(playerController.ValueType.Blood, Time.deltaTime, false);
                }
                player.controller.Move(new Vector3(player.Velocity.x * movementModel.BloodBoostSpeedMod, player.Velocity.y, player.Velocity.z * movementModel.BloodBoostSpeedMod) * Time.deltaTime);
            }
        }
    }
}
