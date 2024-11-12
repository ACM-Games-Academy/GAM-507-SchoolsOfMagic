using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaSpawn : MonoBehaviour
{

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            Debug.Log("Player");
        }
    }
}
