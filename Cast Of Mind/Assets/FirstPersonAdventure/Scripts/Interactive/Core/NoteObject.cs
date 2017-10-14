using UnityEngine;

public class NoteObject : MonoBehaviour, IUse
{
    public string noteId;

    public string Name { get { return Localization.Get("Objects.Note"); } }
    public bool IsActive { get { return true; } }

    public void Use()
    {
        Note note = NoteManager.Instance.GetNoteById(noteId);
        JournalController.Instance.AddNote(note);
        UIController.Instance.ReadNote(note);
    }
}
