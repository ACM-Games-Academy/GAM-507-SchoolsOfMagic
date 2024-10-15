using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imListening : MonoBehaviour
{
    public playerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController.playerDeath += iDied; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void iDied(object sender, EventArgs e)
    {
        Debug.Log("I Died");
    }
}
