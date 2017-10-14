using UnityEngine;
using UnityEngine.UI;

public class NoteListItem : MonoBehaviour
{
    public event System.Action<NoteListItem> onSelect;

    public Button noteButton;
    public Text noteName;

    public Note Note { get; private set; }

    void Start()
    {
        noteButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (onSelect != null) onSelect(this);
    }

    public void SetNote(Note note)
    {
        Note = note;
        noteName.gameObject.SetActive(note != null);
        if (Note != null)
        {
            noteName.text = Note.Name;
        }
    }
}
