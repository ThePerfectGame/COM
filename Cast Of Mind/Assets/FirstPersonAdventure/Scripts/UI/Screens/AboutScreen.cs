using UnityEngine;
using UnityEngine.UI;

public class AboutScreen : Form
{
    public event System.Action onBack;

    public Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(OnBackClick);
    }

    private void OnBackClick()
    {
        if (onBack != null) onBack();
    }
}
