using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    // Declare TaskManager as a Singleton
    public static TaskManager Instance { get; private set; }

    // Dictionary to store tasks and List to store the active tasks
    private Dictionary<string, Task> tasks = new Dictionary<string, Task>();
    private List<Task> activeTasks = new List<Task>();

    // Call the 'Update' method of each active task
    void Update()
    {
        foreach (Task task in activeTasks)
        {
            task.Update();
        }
    }

    // Enable TaskManager to persist across scenes
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add a new task to the tasks dictionary
    public void AddTask(string taskName, Task task)
    {
        tasks.Add(taskName, task);
    }

    // Sets a task to the active tasks list and calls the 'Start' method
    public void ActivateTask(string taskName)
    {
        // Get and validate corresponding task
        Task task;
        if (tasks.TryGetValue(taskName, out task))
        {
            // Check that the task is not already active
            if (activeTasks.Contains(task)) return;

            // Append task to the active tasks list & call the 'Start' method
            activeTasks.Add(task);
            task.Activate();
        }
    }

    // Suspends an active task without resetting its state
    public void SuspendTask(string taskName)
    {
        // Get and validate corresponding task
        Task task;
        if (tasks.TryGetValue(taskName, out task))
        {
            // Remove the task if the task is present in the active tasks list
            bool wasRemoved = activeTasks.Remove(task);
            
            // Call the 'Suspend' method of the task if it was present
            if (wasRemoved) task.Suspend();
        }
    }
}