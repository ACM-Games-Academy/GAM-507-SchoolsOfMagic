using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : SpawnEventHandler
{
    [SerializeField] playerInput playerInput;
    [SerializeField] float eventTimer;
    bool eventReacted;


    private void OnEnable()
    {
        playerInput.jumpPressed += PlayerReact;
    }

    public void StartEvent()
    {
        eventReacted = false;
        StartCoroutine(EventBegin());
    }

    private void PlayerReact(object sender, EventArgs e)
    {
        eventReacted = true;
    }
    
    IEnumerator EventBegin()
    {
        yield return new WaitForSeconds(eventTimer);
        EventResults();
    }

    void EventResults()
    {
        if (!eventReacted)
        {
            completion = true;
        }
    }

    private void OnDisable()
    {
        playerInput.jumpPressed -= PlayerReact;
    }
}
