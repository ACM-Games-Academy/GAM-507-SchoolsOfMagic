using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimedEventSpawn : MonoBehaviour
{
    [SerializeField] float eventTimer;
    private bool eventStarted = false;
    private bool eventReacted = false;
    EnemySpawn enenmySpawn;

    void Start()
    {
        enenmySpawn = GetComponentInChildren<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        //This update void is a experiment to check if the player has/hasn't reacted to the event.
        if (Input.GetKeyDown(KeyCode.Alpha0) && !eventStarted)
        {
            eventStarted = true;
            eventReacted = false;
            StartCoroutine(EventBegin());
        }

        if (eventStarted && !eventReacted && Input.GetKeyDown(KeyCode.Space))
        {
            eventReacted = true;
            Debug.Log("Event Reacted");
        }
    }

    public void StartEvent()
    {
        eventStarted = true;
        eventReacted = false;
        StartCoroutine(EventBegin());
    }
    
    IEnumerator EventBegin()
    {
        Debug.Log("Event Begin");
        yield return new WaitForSeconds(eventTimer);
        EventResults();
    }

    void EventResults()
    {
        if (!eventReacted)
        {
            enenmySpawn.SpawnEnemy();
        }
        eventStarted = false;
    }
}
