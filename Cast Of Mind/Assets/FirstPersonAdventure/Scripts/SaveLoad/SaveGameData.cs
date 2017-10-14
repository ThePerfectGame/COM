using System.Collections.Generic;

[System.Serializable]
public class SaveGameData
{
    public string[] itemsId;
    public string[] notesId;
    public string[] tasksId;
    public string[] completeTasksId;

    public Dictionary<string, List<SaveInfo>> locationsStates;

    public GlobalValues globalValues;
    public string locationName;
    public Vector4Serializer playerPosition;
    public Vector4Serializer playerRotation;
    public int playerHealth;
    public string equipedWearId;
}
