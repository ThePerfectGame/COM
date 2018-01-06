using System.Collections.Generic;

public class JournalController : Singleton<JournalController>
{
    public List<Task> tasks;
    public List<Task> completeTasks;
    public List<Note> notes;

    public void AddNote(Note note, bool showNotification = true)
    {
        if (note == null || notes.Find(n => n.idNote == note.idNote) != null) return;
        notes.Add(note);
        if (showNotification) UIController.Instance.ShowNotification(string.Format(Localization.Get("Notifications.NewNote"), note.Name));
    }

    public void AddNote(string noteId, bool showNotification = true)
    {
        AddNote(NoteManager.Instance.GetNoteById(noteId), showNotification);
    }

    public void AddTask(Task task)
    {
        if (task == null || tasks.Find(t => t.idTask == task.idTask) != null || completeTasks.Find(t => t.idTask == task.idTask) != null) return;
        tasks.Add(task);
    }

    public void AddTask(string taskId)
    {
        AddTask(TaskManager.Instance.GetTaskById(taskId));
    }

    public void CompleteTask(string idTask)
    {
        Task task = tasks.Find(t => t.idTask == idTask);
        if (task != null)
        {
            tasks.Remove(task);
            completeTasks.Add(task);
        }
    }

    public void Reset()
    {
        notes = new List<Note>();
        tasks = new List<Task>();
        completeTasks = new List<Task>();
    }
}
