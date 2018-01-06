using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : Form
{
    public Button continueButton;
    public Button newGameButton;
    public Button optionsButton;
    public Button aboutButton;
    public Button exitButton;

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueClick);
        newGameButton.onClick.AddListener(OnNewGameClick);
        optionsButton.onClick.AddListener(OnOptionsClick);
        aboutButton.onClick.AddListener(OnAboutClick);
        exitButton.onClick.AddListener(OnExitClick);
    }

    public override void Show()
    {
        base.Show();
        continueButton.interactable = SaveLoadController.IsSave;
    }

    private void OnContinueClick()
    {
        GameController.Instance.LoadGame();
    }

    private void OnNewGameClick()
    {
        GameController.Instance.NewGame();
    }

    private void OnOptionsClick()
    {
        UIController.Instance.ShowOptions();
    }

    private void OnAboutClick()
    {
        UIController.Instance.ShowAbout();
    }

    private void OnExitClick()
    {
        Application.Quit();
    }
}
