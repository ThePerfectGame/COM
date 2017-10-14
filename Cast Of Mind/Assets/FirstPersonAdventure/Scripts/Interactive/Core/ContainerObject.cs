using System.Collections.Generic;
using System.Linq;

public class ContainerObject : Saveable, IUse
{
    public string containerName;
    public string[] itemsID;

    public string Name { get { return Localization.Get(containerName); } }
    public bool IsActive { get { return true; } }

    public List<Item> Items { get; private set; }

    void Start()
    {
        Items = new List<Item>();
        foreach (string id in itemsID)
        {
            Items.Add(ItemsManager.Instance.GetItemById(id));
        }
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    public virtual void Use()
    {
        UIController.Instance.ShowContainerScreen(this);
    }

    public override SaveInfo Save()
    {
        SaveInfo saveInfo = new SaveInfo();
        saveInfo.WriteProperty("itemsId", Items.Select(i => i.idItem).ToArray());
        return saveInfo;
    }

    public override void Load(SaveInfo saveInfo)
    {
        itemsID = saveInfo.ReadProperty<string[]>("itemsId");
    }
}
