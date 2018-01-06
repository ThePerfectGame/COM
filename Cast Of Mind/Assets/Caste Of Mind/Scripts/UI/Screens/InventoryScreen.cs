using UnityEngine;
using UnityEngine.UI;

public class InventoryScreen : Form
{
    public Sprite defaultIcon;
    public Text selectedText;
    public Image iconImage;
    public Text descriptionText;
    public Button equipButton;
    public Button takeOffButton;
    public ItemSlot[] slots;

    private ItemSlot selectedSlot;

    void Start()
    {
        foreach (ItemSlot itemSlot in slots)
        {
            itemSlot.onSelectSlot += OnSelectSlot;
        }
        equipButton.onClick.AddListener(OnEquipClick);
        takeOffButton.onClick.AddListener(OnStopUseClick);
    }

    public override void Show()
    {
        base.Show();
        SelectSlot(slots[0]);
        FillSlots();
    }

    private void OnSelectSlot(ItemSlot itemSlot)
    {
        SelectSlot(itemSlot);
        FillSlots();
    }

    private void OnEquipClick()
    {
        ItemsController.UseItem(selectedSlot.ItemInSlot);
        FillSlots();
        SelectSlot(selectedSlot);
    }

    private void OnStopUseClick()
    {
        ItemsController.StopUse(selectedSlot.ItemInSlot);
        FillSlots();
        SelectSlot(selectedSlot);
    }

    private void SelectSlot(ItemSlot slot)
    {
        selectedSlot = slot;
        if (selectedSlot != null && selectedSlot.ItemInSlot == null) selectedSlot = null;
        if (selectedSlot != null)
        {
            selectedText.text = selectedSlot.ItemInSlot.Name;
            iconImage.sprite = selectedSlot.ItemInSlot.icon ?? defaultIcon;
            descriptionText.text = selectedSlot.ItemInSlot.Description;
            equipButton.gameObject.SetActive(selectedSlot.ItemInSlot.useItemType != UseItemType.None && ItemsController.IsEquiped(selectedSlot.ItemInSlot) == false);
            if (equipButton.gameObject.activeSelf)
            {
                equipButton.enabled = ItemsController.CheckUseItem(selectedSlot.ItemInSlot);
            }
            takeOffButton.gameObject.SetActive(selectedSlot.ItemInSlot.useItemType != UseItemType.None && ItemsController.IsEquiped(selectedSlot.ItemInSlot) == true);
        }
        else
        {
            selectedText.text = "";
            iconImage.sprite = defaultIcon;
            descriptionText.text = "";
            equipButton.gameObject.SetActive(false);
            takeOffButton.gameObject.SetActive(false);
        }
    }

    private void FillSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i >= InventoryController.Instance.items.Count)
            {
                slots[i].SetItem(null, false);
            }
            else
            {
                Item item = InventoryController.Instance.items[i];
                slots[i].SetItem(item, slots[i] == selectedSlot);
            }
        }
    }
}
