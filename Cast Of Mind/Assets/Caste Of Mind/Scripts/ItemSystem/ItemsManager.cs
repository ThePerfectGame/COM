using System.Collections.Generic;

public class ItemsManager : Singleton<ItemsManager>
{
    public List<Item> items;

    public Item AddNewItem()
    {
        Item item = new Item();
        items.Add(item);
        return item;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public Item GetItemById(string id)
    {
        return items.Find(i => i.idItem == id);
    }
}
