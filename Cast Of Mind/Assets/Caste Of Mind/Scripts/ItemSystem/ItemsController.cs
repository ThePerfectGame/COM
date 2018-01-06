public static class ItemsController
{

    public static bool CheckUseItem(Item item)
    {
        if (item.useItemType == UseItemType.MedicKit && PlayerController.Instance.Health == PlayerController.MaxHealth) return false;
        return true;
    }

    public static void UseItem(Item item)
    {
        if (item.useItemType == UseItemType.MedicKit)
        {
            PlayerController.Instance.RestoreHealth();
            InventoryController.Instance.RemoveItem(item);
        }
        if (item.useItemType == UseItemType.AntiRadiationSuit)
        {
            InventoryController.Instance.SetDress(item);
            PlayerController.Instance.RadiationProtection = true;
        }
    }

    public static void StopUse(Item item)
    {
        if (item.useItemType == UseItemType.AntiRadiationSuit)
        {
            PlayerController.Instance.RadiationProtection = false;
            if (InventoryController.Instance.CurrentDress != null && InventoryController.Instance.CurrentDress.idItem == item.idItem) InventoryController.Instance.SetDress(null);
        }
    }

    public static bool IsEquiped(Item item)
    {
        if (InventoryController.Instance.CurrentDress != null && InventoryController.Instance.CurrentDress.idItem == item.idItem) return true;
        return false;
    }
}
