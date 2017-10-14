using UnityEngine;
using System.Collections.Generic;

public class InventoryController : Singleton<InventoryController>
{
    public List<Item> items = new List<Item>();

    public Item CurrentDress { get; private set; }

    public void SetDress(Item item)
    {
        Item oldDress = CurrentDress;
        CurrentDress = item;
        if (oldDress != null) ItemsController.StopUse(oldDress);
    }

    public void AddItem(Item item)
    {
        SoundsController.Play("Equip");
        items.Add(item);
    }

    public void AddItem(string itemId)
    {
        AddItem(ItemsManager.Instance.GetItemById(itemId));
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void RemoveItem(string id)
    {
        RemoveItem(GetItemById(id));
    }

    public Item GetItemById(string id)
    {
        return items.Find(i => i.idItem == id);
    }

    public void Reset()
    {
        items = new List<Item>();
        CurrentDress = null;
    }
}
