using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerController : MonoBehaviour
{
    //this is the event it will send in the event the player dies
    public event EventHandler playerDeath;

    //this where the player will access runtime variables
    [SerializeField] playerModel playerModel;

    // Start is called before the first frame update
    void OnEnable()
    {
        //sends out debug if playerdata script isnt present
        if (playerModel == null)
        {
            Debug.Log("Needs playermodel");
        }
    }

    // Update is called once per frame
    void Update()
    {
        deathCheck();
    }

    //this checks whether the player has died or not. health = 0
    private void deathCheck()
    {
        if (playerModel.getHealth() <= 0)
        {
            Debug.Log("You have died");
            onPlayerDeath(EventArgs.Empty);
        }
    }

    private void onPlayerDeath(EventArgs e)
    {
        //playerDeath.Invoke(this, e);
    }
}
