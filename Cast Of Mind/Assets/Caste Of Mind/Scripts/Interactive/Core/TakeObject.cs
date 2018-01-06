public class TakeObject : SaveableTransform, IUse
{
    public string itemId;

    public string Name { get { return item == null ? "" : item.Name; } }
    public bool IsActive { get; protected set; }

    private Item item;

    void Start()
    {
        item = ItemsManager.Instance.GetItemById(itemId);
        IsActive = true;
    }

    public void Use()
    {
        SoundsController.Play("Correct");
        InventoryController.Instance.AddItem(item);
        Destroy(gameObject);
        IsActive = false;
    }
}
