using UnityEngine.UI;

public class DeathScreen : Form
{
    public Button loadButton;
    public Button mainMenuButton;

    void Start()
    {
        loadButton.onClick.AddListener(OnLoadClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    private void OnLoadClick()
    {
        GameController.Instance.LoadGame();
    }

    private void OnMainMenuClick()
    {
        GameController.Instance.ToMainMenu();
    }
}
