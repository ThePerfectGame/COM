using UnityEngine;
using UnityEngine.UI;

public class ContainerScreen : Form
{
    public Text titleText;
    public Text nameItemText;
    public Button cancelButton;

    public ItemSlot[] slots;

    private ContainerObject containerObject;

    public void SetContainer(ContainerObject containerObject)
    {
        this.containerObject = containerObject;
    }

    public override void Show()
    {
        base.Show();
        titleText.text = containerObject == null ? "" : containerObject.Name;
        foreach (ItemSlot slot in slots)
        {
            slot.onSelectSlot += OnSelectSlot;
            slot.onHover += OnHoverSlot;
        }
        cancelButton.onClick.AddListener(OnCancelClick);
        FillSlots();
    }

    private void OnHoverSlot(ItemSlot slot)
    {
        if (slot.ItemInSlot == null)
        {
            nameItemText.text = "";
        }
        else
        {
            nameItemText.text = slot.ItemInSlot.Name;
        }
    }

    private void OnSelectSlot(ItemSlot slot)
    {
        if (slot.ItemInSlot == null) return;
        InventoryController.Instance.AddItem(slot.ItemInSlot);
        containerObject.RemoveItem(slot.ItemInSlot);
        FillSlots();
    }

    private void FillSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i >= containerObject.Items.Count)
            {
                slots[i].SetItem(null, false);
            }
            else
            {
                slots[i].SetItem(containerObject.Items[i], false);
            }
        }
    }

    private void OnCancelClick()
    {
        GameController.Instance.GameState = GameStates.Game;
    }
}
