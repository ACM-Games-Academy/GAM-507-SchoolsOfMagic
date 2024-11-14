using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : SpawnEventHandler
{
    [SerializeField] playerInput playerInput;
    [SerializeField] float eventTimer;
    bool eventReacted;
    int eventCountDown = 3;


    private void OnEnable()
    {
        playerInput.jumpPressed += PlayerReact;
    }

    public void StartEvent()
    {
        eventCountDown = 3;
        eventReacted = false;
        StartCoroutine(EventBegin());
    }

    private void PlayerReact(object sender, EventArgs e)
    {
        eventReacted = true;
    }
    
    IEnumerator EventBegin()
    {
        Debug.Log(eventCountDown);
        yield return new WaitForSeconds(eventTimer);
        eventCountDown--;
        Countdown();

    }

    void Countdown()
    {
        if (!eventReacted)
        {
            if (eventCountDown == 0)
            {
                EventResults();
            }

            else
            {
                StartCoroutine(EventBegin());
            }
        }

    }
    void EventResults()
    {
        completion = true;
    }

    private void OnDisable()
    {
        playerInput.jumpPressed -= PlayerReact;
    }
}
