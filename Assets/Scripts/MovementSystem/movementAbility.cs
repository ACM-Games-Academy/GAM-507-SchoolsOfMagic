using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementAbility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void MovementUpdate(movementController player)
    {
        Vector2 leftStick = player.inputModule.GetMovementInput().normalized;
    }

    public virtual void Jump(movementController player)
    {

    }
}
