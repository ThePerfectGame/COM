using UnityEngine;

public class Form : MonoBehaviour
{
    public bool IsShown { get; private set; }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        IsShown = true;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        IsShown = false;
    }

    public void SetState(bool show)
    {
        if (show) Show();
        else Hide();
    }
}