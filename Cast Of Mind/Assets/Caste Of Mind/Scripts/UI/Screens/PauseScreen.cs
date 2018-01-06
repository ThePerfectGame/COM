using UnityEngine.UI;

public class PauseScreen : Form
{
    public Button continueButton;
    public Button optionsButton;
    public Button mainMenuButton;

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueClick);
        optionsButton.onClick.AddListener(OnOptionsClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    private void OnContinueClick()
    {
        GameController.Instance.GameState = GameStates.Game;
    }

    private void OnOptionsClick()
    {
        UIController.Instance.ShowOptions();
    }

    private void OnMainMenuClick()
    {
        GameController.Instance.SaveGame();
        GameController.Instance.ToMainMenu();
    }
}
