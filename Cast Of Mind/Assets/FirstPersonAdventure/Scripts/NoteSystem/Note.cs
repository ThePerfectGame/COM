[System.Serializable]
public class Note
{
    public string idNote;
    public string name;
    public string text;

    public string Name { get { return Localization.Get(name); } }
    public string Text { get { return Localization.Get(text); } }
}
