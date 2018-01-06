using UnityEngine.UI;

public class GameCompleteScreen : Form
{
    public Button restartButton;
    public Button mainMenuButton;

    void Start()
    {
        restartButton.onClick.AddListener(OnRestartClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    private void OnRestartClick()
    {
        GameController.Instance.NewGame();
    }

    private void OnMainMenuClick()
    {
        GameController.Instance.ToMainMenu();
    }
}
