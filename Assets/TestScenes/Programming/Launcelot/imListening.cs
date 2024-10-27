using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imListening : MonoBehaviour
{
    public inputManager playerInput;

    private void OnEnable()
    {
        playerInput.jumpPressed += jump;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void jump(object sender, EventArgs e)
    {
        Debug.Log("Jump button pressed");
    }

    void OnDisabled()
    {
        playerInput.jumpPressed -= jump;
    }
}
