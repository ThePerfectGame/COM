using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class JournalScreen : Form
{
    public GameObject notesPanel;
    public GameObject tasksPanel;
    public Button notesButton;
    public Button tasksButton;
    public string completeTasksColor = "71D352FF";

    public Text tasksText;
    public Text noteText;

    public NoteListItem[] noteListItems;
    private NoteListItem selecListItem;

    void Start()
    {
        foreach (NoteListItem noteListItem in noteListItems)
        {
            noteListItem.onSelect += OnSelectNote;
        }
        notesButton.onClick.AddListener(OnNotesClick);
        tasksButton.onClick.AddListener(OnTasksClick);
    }

    public void OnTasksClick()
    {
        notesPanel.gameObject.SetActive(false);
        tasksPanel.gameObject.SetActive(true);
        tasksButton.interactable = false;
        notesButton.interactable = true;
        FillTask();
    }

    public void OnNotesClick()
    {
        notesPanel.gameObject.SetActive(true);
        tasksPanel.gameObject.SetActive(false);
        tasksButton.interactable = true;
        notesButton.interactable = false;
        FillList();
        OnSelectNote(JournalController.Instance.notes.Count > 0 ? noteListItems[0] : null);
    }

    public void OnSelectNote(NoteListItem listItem)
    {
        selecListItem = listItem;
        if (selecListItem != null && selecListItem.Note == null) selecListItem = null;
        if (selecListItem == null)
        {
            noteText.text = "";
        }
        else
        {
            noteText.text = selecListItem.Note.Text;
        }
        FillList();
    }

    public override void Show()
    {
        base.Show();
        OnTasksClick();
        FillTask();
    }

    private void FillTask()
    {
        List<Task> sortedTask = new List<Task>();
        sortedTask.AddRange(JournalController.Instance.tasks.FindAll(t => t.isPrimary == true));
        sortedTask.AddRange(JournalController.Instance.tasks.FindAll(t => t.isPrimary == false));
        tasksText.text = "";
        foreach (Task task in sortedTask)
        {
            tasksText.text += "- " + task.Name + '\n';
        }
        tasksText.text += '\n';
        tasksText.text += "<color=#" + completeTasksColor + ">";
        foreach (Task task in JournalController.Instance.completeTasks)
        {
            tasksText.text += "- " + task.Name + '\n';
        }
        tasksText.text += "</color>";
    }

    private void FillList()
    {
        for (int i = 0; i < noteListItems.Length; i++)
        {
            noteListItems[i].noteButton.interactable = noteListItems[i] != selecListItem;
            if (i >= JournalController.Instance.notes.Count)
            {
                noteListItems[i].gameObject.SetActive(false);
            }
            else
            {
                noteListItems[i].gameObject.SetActive(true);
                noteListItems[i].SetNote(JournalController.Instance.notes[i]);
            }
        }
    }
}
