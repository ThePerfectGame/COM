using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundsController.Play("ButtonHover");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundsController.Play("ButtonClick");
    }
}
