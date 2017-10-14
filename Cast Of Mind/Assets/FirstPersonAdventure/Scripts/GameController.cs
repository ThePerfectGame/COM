using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum GameStates
{
    MainMenu = 0,
    Game = 1,
    Notes = 2,
    Inventory = 3,
    Pause = 4,
    ReadNote = 5,
    Container = 6,
    ActionsList = 7,
    Death = 8,
    Loading = 9,
    GameComplete = 10,
    PasswordInput = 11
}

public class GameController : Singleton<GameController>
{
    public event System.Action<GameStates> onStateChanged;

    public string mainMenuScene;
    public string startScene;

    private GameStates gameState;
    public GameStates GameState
    {
        get { return gameState; }
        set
        {
            if (value == gameState) return;
            gameState = value;
            Time.timeScale = value == GameStates.Game || value == GameStates.Loading || value == GameStates.MainMenu ? 1 : 0;
            if (onStateChanged != null) onStateChanged(value);
        }
    }

    public string PrevLocationName { get; private set; }
    public Room CurrentRoom { get; private set; }
    public GlobalValues Values { get; set; }
    public Dictionary<string, List<SaveInfo>> locationsStates = new Dictionary<string, List<SaveInfo>>();

    protected override void Awake()
    {
        base.Awake();
        Values = new GlobalValues();
    }

    void Start()
    {
        gameState = GameStates.Pause;
        GameState = Application.loadedLevelName == mainMenuScene ? GameStates.MainMenu : GameStates.Game;
    }

    private void Update()
    {
        if (InputController.Notebook)
        {
            if (GameState == GameStates.Game || GameState == GameStates.Inventory) GameState = GameStates.Notes;
            else if (GameState == GameStates.Notes) GameState = GameStates.Game;
        }
        if (InputController.Inventory)
        {
            if (GameState == GameStates.Game || GameState == GameStates.Notes) GameState = GameStates.Inventory;
            else if (GameState == GameStates.Inventory) GameState = GameStates.Game;
        }
        if (GameState == GameStates.ReadNote && InputController.Use)
        {
            GameState = GameStates.Game;
        }
        if (InputController.Escape)
        {
            if (GameState == GameStates.Game) GameState = GameStates.Pause;
            else if (GameState == GameStates.Pause || GameState == GameStates.ReadNote || GameState == GameStates.ActionsList || GameState == GameStates.Container) GameState = GameStates.Game;
        }
    }

    public void NewGame()
    {
        ResetGame();
        JournalController.Instance.AddTask("t_escape");
        LoadLevel(startScene, false, null);
    }

    public void LoadGame()
    {
        if (Application.isWebPlayer) return;
        ResetGame();
        SaveGameData data = SaveLoadController.LoadGame();
        if (data == null) return;

        locationsStates = data.locationsStates;
        Values = data.globalValues;

        foreach (string id in data.itemsId) InventoryController.Instance.AddItem(id);
        foreach (string id in data.notesId) JournalController.Instance.AddNote(id, false);
        foreach (string id in data.tasksId) JournalController.Instance.AddTask(id);
        foreach (string id in data.completeTasksId)
        {
            JournalController.Instance.AddTask(id);
            JournalController.Instance.CompleteTask(id);
        }

        LoadLevel(data.locationName, false, () =>
        {
            PlayerController.Instance.MoveToPoint(data.playerPosition.GetVector3(), data.playerRotation.GetQuaternion());
            PlayerController.Instance.Health = data.playerHealth;
            if (data.equipedWearId != "") ItemsController.UseItem(ItemsManager.Instance.GetItemById(data.equipedWearId));
        });
    }

    public void SaveGame()
    {
        if (Application.isWebPlayer) return;
        CurrentRoom = FindObjectOfType<Room>();
        if (CurrentRoom != null)
        {
            if (locationsStates.ContainsKey(Application.loadedLevelName))
                locationsStates[Application.loadedLevelName] = CurrentRoom.SaveState();
            else
                locationsStates.Add(Application.loadedLevelName, CurrentRoom.SaveState());
        }

        SaveGameData data = new SaveGameData();
        data.itemsId = InventoryController.Instance.items.Select(i => i.idItem).ToArray();
        data.notesId = JournalController.Instance.notes.Select(n => n.idNote).ToArray();
        data.tasksId = JournalController.Instance.tasks.Select(t => t.idTask).ToArray();
        data.completeTasksId = JournalController.Instance.completeTasks.Select(t => t.idTask).ToArray();

        data.locationsStates = locationsStates;

        data.globalValues = Values;
        data.locationName = Application.loadedLevelName;
        data.playerPosition = new Vector4Serializer(PlayerController.Instance.transform.position);
        data.playerRotation = new Vector4Serializer(PlayerController.Instance.transform.rotation);
        data.playerHealth = PlayerController.Instance.Health;
        data.equipedWearId = InventoryController.Instance.CurrentDress == null ? "" : InventoryController.Instance.CurrentDress.idItem;

        SaveLoadController.SaveGame(data);
    }

    public void CompleteGame()
    {
        GameState = GameStates.GameComplete;
    }

    public void ToMainMenu()
    {
        LoadLevel(mainMenuScene, false, null);
    }

    public void LoadLevel(string locationName, bool saveCurrentLocationState, System.Action onLoadComplete)
    {
        if (saveCurrentLocationState)
        {
            CurrentRoom = FindObjectOfType<Room>();
            if (CurrentRoom != null)
            {
                if (locationsStates.ContainsKey(Application.loadedLevelName))
                    locationsStates[Application.loadedLevelName] = CurrentRoom.SaveState();
                else
                    locationsStates.Add(Application.loadedLevelName, CurrentRoom.SaveState());
            }
        }
        StartCoroutine(Loading(locationName, onLoadComplete));
    }

    private void AutoSave()
    {
        SaveGame();
        UIController.Instance.ShowNotification(Localization.Get("Notifications.AutoSave"));
    }

    private void ResetGame()
    {
        InventoryController.Instance.Reset();
        JournalController.Instance.Reset();
        if (PlayerController.IsInstance) Destroy(PlayerController.Instance);
        locationsStates = new Dictionary<string, List<SaveInfo>>();
    }

    private IEnumerator Loading(string locationName, System.Action onLoadComplete)
    {
        GameState = GameStates.Loading;
        PrevLocationName = Application.loadedLevelName;

        yield return Application.LoadLevelAsync(locationName);
        Debug.Log("Loading complete: " + Application.loadedLevelName);
        CurrentRoom = FindObjectOfType<Room>();
        if (CurrentRoom != null)
        {
            CurrentRoom.Enter(PrevLocationName);
            if (locationsStates.ContainsKey(Application.loadedLevelName))
                CurrentRoom.LoadState(locationsStates[Application.loadedLevelName]);
        }
        if (onLoadComplete != null) onLoadComplete();
        if (Application.loadedLevelName == mainMenuScene && PlayerController.IsInstance) Destroy(PlayerController.Instance.gameObject);

        yield return new WaitForSeconds(0.1f);
        if (Application.loadedLevelName == mainMenuScene)
        {
            GameState = GameStates.MainMenu;
        }
        else
        {
            GameState = GameStates.Game;
            if (PrevLocationName != mainMenuScene) AutoSave();
        }
    }
}
