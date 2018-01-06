using UnityEngine;
using UnityEngine.UI;

public class EffectsScreen : Form
{
    public Image radiationMask;
    public Image hitEffect;

    void Update()
    {
        //if (PlayerController.IsInstance == false) return;
        radiationMask.gameObject.SetActive(InventoryController.Instance.CurrentDress != null && InventoryController.Instance.CurrentDress.useItemType == UseItemType.AntiRadiationSuit);
        if (Time.time - PlayerController.Instance.HitTime < 1)
        {
            hitEffect.gameObject.SetActive(true);
            hitEffect.color = new Color(1f, 0, 0, 1 - (Time.time - PlayerController.Instance.HitTime) - 0.5f);
        }
        else
        {
            hitEffect.gameObject.SetActive(false);
        }
    }
}
