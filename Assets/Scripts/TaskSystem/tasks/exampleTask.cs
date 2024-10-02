using System;
using UnityEngine;

// Example of utilizing the task system
// This task will track a provided character model until it reaches a provided goal post
public class ExampleTask : Task
{
    // Define variables to point to the container performing this task and the destination the container must reach 
    private Transform container;
    private Vector3 destination;
    private Action onComplete;

    // Constructor taking the container and destination as parameters
    public ExampleTask(Transform _container, Vector3 _destination, Action _onComplete)
    {
        container = _container;
        destination = _destination;
        onComplete = _onComplete;
    }

    // Check for task completion
    public override void Update()
    {
        Debug.Log((container.position - destination).sqrMagnitude);
        if ((container.position - destination).sqrMagnitude < 10)
        {
            Complete();
        }
    }

    public override void Complete()
    {
        onComplete();
    }
}
