[System.Serializable]
public class Task
{
    public string idTask;
    public string name;
    public bool isPrimary = false;

    public string Name { get { return Localization.Get(name); } }
}
