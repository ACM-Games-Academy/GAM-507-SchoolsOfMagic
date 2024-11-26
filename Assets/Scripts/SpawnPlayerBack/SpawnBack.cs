using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBack : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    
    // Start is called before the first frame update
    void OnTriggerEnter(Collider Player)
    {
        if(Player.gameObject.name == "Player")
        {
            Debug.Log("Player Found");
            Player.transform.position = spawnPosition.transform.position;
        }
    }
}
