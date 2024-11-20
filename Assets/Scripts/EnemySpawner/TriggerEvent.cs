using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : SpawnEventHandler
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            completion = true;
        }
    }
}
