using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEvent : SpawnEventHandler
{
    [SerializeField] float eventTimer;

    public void Start()
    {
        StartCoroutine(EventBegin());
    }
    
    IEnumerator EventBegin()
    {
        yield return new WaitForSeconds(eventTimer);
        completion = true;
    }
}
