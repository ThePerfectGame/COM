using UnityEngine;

public class UIController : Singleton<UIController>
{
    public GameScreen gameScreen;
    public JournalScreen JournalScreen;
    public PageScreen pageScreen;
    public InventoryScreen inventorySceen;
    public ContainerScreen containerScreen;
    public PauseScreen pauseScreen;
    public MainMenuScreen mainMenuScreen;
    public AboutScreen aboutScreen;
    public OptionsScreen optionsScreen;
    public LoadingScreen loadingScreen;
    public ActionsScreen actionsScreen;
    public DeathScreen deathScreen;
    public EffectsScreen effectsScreen;
    public GameCompleteScreen gameComplete;
    public PasswordScreen passwordScreen;

    protected override void Awake()
    {
        base.Awake();
        GameController.Instance.onStateChanged += OnGameStateChanged;
        aboutScreen.onBack += BackFromAbout;
        optionsScreen.onBack += BackFromOptions;
    }

    void OnDestroy()
    {
        if (GameController.IsInstance) GameController.Instance.onStateChanged -= OnGameStateChanged;
    }

    public void ReadNote(Note note)
    {
        pageScreen.SetNote(note);
        GameController.Instance.GameState = GameStates.ReadNote;
    }

    public void ShowContainerScreen(ContainerObject containerObject)
    {
        containerScreen.SetContainer(containerObject);
        GameController.Instance.GameState = GameStates.Container;
    }

    public void ShowPasswordScreen(Safe safe)
    {
        passwordScreen.SetSafe(safe);
        GameController.Instance.GameState = GameStates.PasswordInput;
    }

    public void ShowActionsScreen(ActionsObject actionsObject)
    {
        actionsScreen.SetInteractObject(actionsObject);
        GameController.Instance.GameState = GameStates.ActionsList;
    }

    public void ShowAbout()
    {
        mainMenuScreen.Hide();
        aboutScreen.Show();
    }

    public void ShowOptions()
    {
        mainMenuScreen.Hide();
        pauseScreen.Hide();
        optionsScreen.Show();
    }

    public void ShowNotification(string text)
    {
        gameScreen.AddNotification(text);
    }

    private void OnGameStateChanged(GameStates state)
    {
        Cursor.lockState = state == GameStates.Game ? CursorLockMode.Locked : CursorLockMode.None;
        aboutScreen.Hide();
        optionsScreen.Hide();
        effectsScreen.SetState(state != GameStates.MainMenu && state != GameStates.Loading);
        loadingScreen.SetState(state == GameStates.Loading);
        gameScreen.SetState(state == GameStates.Game);
        JournalScreen.SetState(state == GameStates.Notes);
        pageScreen.SetState(state == GameStates.ReadNote);
        inventorySceen.SetState(state == GameStates.Inventory);
        containerScreen.SetState(state == GameStates.Container);
        pauseScreen.SetState(state == GameStates.Pause);
        mainMenuScreen.SetState(state == GameStates.MainMenu);
        actionsScreen.SetState(state == GameStates.ActionsList);
        deathScreen.SetState(state == GameStates.Death);
        gameComplete.SetState(state == GameStates.GameComplete);
        passwordScreen.SetState(state == GameStates.PasswordInput);
    }

    private void BackFromAbout()
    {
        aboutScreen.Hide();
        mainMenuScreen.Show();
    }

    private void BackFromOptions()
    {
        optionsScreen.Hide();
        if (GameController.Instance.GameState == GameStates.Pause) pauseScreen.Show();
        if (GameController.Instance.GameState == GameStates.MainMenu) mainMenuScreen.Show();
    }
}
