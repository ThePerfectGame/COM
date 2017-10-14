using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler
{
    public event System.Action<ItemSlot> onSelectSlot;
    public event System.Action<ItemSlot> onHover;

    public Image selected;
    public Image icon;
    public Image equipedMarker;
    public Button button;

    public Item ItemInSlot { get; private set; }
    public bool IsSelected { get; private set; }

    void Start()
    {
        button.onClick.AddListener(OnClickSlot);
    }

    public void OnClickSlot()
    {
        if (onSelectSlot != null) onSelectSlot(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onHover != null) onHover(this);
    }

    public void SetItem(Item item, bool isSelected)
    {
        ItemInSlot = item;
        IsSelected = isSelected;
        if (ItemInSlot == null)
        {
            button.interactable = false;
            icon.gameObject.SetActive(false);
            equipedMarker.gameObject.SetActive(false);
            selected.gameObject.SetActive(false);
        }
        else
        {
            button.interactable = IsSelected == false;
            icon.gameObject.SetActive(true);
            icon.sprite = item.icon;
            equipedMarker.gameObject.SetActive(ItemsController.IsEquiped(item));
            selected.gameObject.SetActive(IsSelected);
        }
    }
}
