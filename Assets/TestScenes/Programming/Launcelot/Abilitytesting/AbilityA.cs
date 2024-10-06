using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityA : AbilityController
{
    private void OnEnable()
    {
        AbilityController.primary += ability;  //this can be primary or seconday depends on what ability you are programming
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ability(object sender, EventArgs e)
    {
        //this is where your ability code will go 
    }
}
