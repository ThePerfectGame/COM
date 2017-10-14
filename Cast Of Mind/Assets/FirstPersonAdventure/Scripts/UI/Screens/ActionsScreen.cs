using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsScreen : Form
{
    public Button templateButton;
    public Text titleText;

    private ActionsObject actionsObject;
    private List<Button> buttons = new List<Button>();
    private InteractAction cancelInteractAction;

    void Awake()
    {
        cancelInteractAction = new InteractAction(Localization.Get("UI.Common.Cancel"), OnCancel);
    }

    public void SetInteractObject(ActionsObject actionsObject)
    {
        this.actionsObject = actionsObject;
    }

    public override void Show()
    {
        base.Show();
        titleText.text = actionsObject.Name;
        CreateButtons();
    }

    private void CreateButtons()
    {
        foreach (Button button in buttons) Destroy(button.gameObject);
        buttons = new List<Button>();
        List<InteractAction> actions = new List<InteractAction>(actionsObject.GetActions());
        actions.Add(cancelInteractAction);
        templateButton.gameObject.SetActive(true);
        foreach (InteractAction action in actions)
        {
            if (action.unityAction == null || action.nameAction == null) continue;
            Button newButton = Instantiate(templateButton).GetComponent<Button>();
            newButton.transform.SetParent(templateButton.transform.parent, false);
            newButton.GetComponentInChildren<Text>().text = action.nameAction;
            newButton.onClick.AddListener(action.unityAction);
            if (action != cancelInteractAction) newButton.onClick.AddListener(RefreshButtons);
            buttons.Add(newButton);
        }
        templateButton.gameObject.SetActive(false);
    }

    private void RefreshButtons()
    {
        CreateButtons();
    }

    private void OnCancel()
    {
        GameController.Instance.GameState = GameStates.Game;
    }
}
