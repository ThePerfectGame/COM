using UnityEngine;

public enum UseItemType
{
    None = 0,
    AntiRadiationSuit = 1,
    MedicKit = 2
}

[System.Serializable]
public class Item
{
    public string idItem;
    public string name;
    public string description;
    public Sprite icon;
    public UseItemType useItemType = UseItemType.None;

    public string Name { get { return Localization.Get(name); } }
    public string Description { get { return Localization.Get(description); } }
}
