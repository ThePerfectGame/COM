using UnityEngine.UI;

public class PageScreen : Form
{
    public Text noteText;

    private Note note;

    public void SetNote(Note note)
    {
        this.note = note;
    }

    public override void Show()
    {
        if (note == null) return;
        base.Show();
        noteText.text = note.Text;
    }
}
