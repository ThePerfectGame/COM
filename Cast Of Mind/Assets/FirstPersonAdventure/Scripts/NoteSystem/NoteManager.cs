using System.Collections.Generic;

public class NoteManager : Singleton<NoteManager>
{
    public List<Note> notes = new List<Note>();

    public Note AddNote()
    {
        Note note = new Note();
        notes.Add(note);
        return note;
    }

    public void RemoveNote(Note note)
    {
        notes.Remove(note);
    }

    public Note GetNoteById(string id)
    {
        return notes.Find(i => i.idNote == id);
    }
}
