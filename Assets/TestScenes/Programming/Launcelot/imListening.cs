using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imListening : MonoBehaviour
{
    public playerInput playerController;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController.playerJumping += jump; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void jump(object sender, EventArgs e)
    {
        Debug.Log("Jump button pressed");
    }
}
