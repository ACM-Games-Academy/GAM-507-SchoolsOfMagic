using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] GameObject SpawnEvent;
    TimedEvent timedEvent;
    // Start is called before the first frame update
    void Start()
    {
        timedEvent = SpawnEvent.GetComponent<TimedEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            timedEvent.StartEvent();
        }
    }
}
