using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    
    // Start is called before the first frame update
    void OnTriggerEnter(Collider Player)
    {
        if(Player.transform.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            Debug.Log("DEad");
            controller.AddReduceValue(PlayerController.ValueType.Health, -controller.GetHealth(), false);
        }
    }
}
