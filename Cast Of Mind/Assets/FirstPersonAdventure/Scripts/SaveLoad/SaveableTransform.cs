public class SaveableTransform : Saveable
{
    public override SaveInfo Save()
    {
        SaveInfo saveInfo = new SaveInfo();
        saveInfo.WriteProperty("position", new Vector4Serializer(transform.position));
        saveInfo.WriteProperty("rotation", new Vector4Serializer(transform.rotation));
        saveInfo.WriteProperty("scale", new Vector4Serializer(transform.localScale));
        return saveInfo;
    }

    public override void Load(SaveInfo saveInfo)
    {
        if (saveInfo == null) return;
        transform.position = saveInfo.ReadProperty<Vector4Serializer>("position");
        transform.rotation = saveInfo.ReadProperty<Vector4Serializer>("rotation");
        transform.localScale = saveInfo.ReadProperty<Vector4Serializer>("scale");
    }
}
