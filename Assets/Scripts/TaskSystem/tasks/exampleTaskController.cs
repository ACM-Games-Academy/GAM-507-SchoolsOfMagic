using System;
using UnityEngine;

// Example of applying a task utilizing the task manager
public class exampleTaskController : MonoBehaviour
{
    // Define variables to point to the container and destination
    public Transform container;
    public Vector3 destination;

    // Method to begin the task
    private void Start()
    {
        Action onComplete = () => {
            Debug.Log("Task complete!");
        };

        TaskManager.Instance.AddTask("ExampleTask", new ExampleTask(container, destination, onComplete));
        TaskManager.Instance.ActivateTask("ExampleTask");
    }
}
