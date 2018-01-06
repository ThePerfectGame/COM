using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    public List<Task> tasks = new List<Task>();

    public Task AddTask()
    {
        Task task = new Task();
        tasks.Add(task);
        return task;
    }

    public void RemoveTask(Task task)
    {
        tasks.Remove(task);
    }

    public Task GetTaskById(string id)
    {
        Task task = tasks.Find(i => i.idTask == id);
        if (task == null) Debug.LogWarning("Can't find: "+id);
        return task;
    }
}
