using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractEvent : SpawnEventHandler
{
    [SerializeField] playerInput playerInput;
    private bool playerInArea = false;

    private void OnEnable()
    {
        playerInput.runPressed += Interact;
    }

    void Interact(object sender, EventArgs e)
    {
      
        if (playerInArea) 
        {
            completion = true;
        }
    }
    
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInArea = false;
        }
    }

    private void OnDisable()
    {
        playerInput.runPressed -= Interact;
    }
}
