using UnityEngine.UI;

public class PasswordScreen : Form
{
    public Button clearButton;
    public Button enterButton;
    public Button cancelButton;
    public Button[] numbersButton;

    public Text inputText;

    private Safe safe;

    void Start()
    {
        enterButton.onClick.AddListener(OnEnterClick);
        clearButton.onClick.AddListener(OnClearClick);
        cancelButton.onClick.AddListener(OnCancelClick);
    }

    // from inpector
    public void OnClickNumber(int num)
    {
        if (inputText.text.Length < 4)
        {
            inputText.text = inputText.text + num;
        }
        else
        {
            SoundsController.Play("Error");
        }
    }

    public override void Show()
    {
        base.Show();
        inputText.text = "";
    }

    public void SetSafe(Safe safe)
    {
        this.safe = safe;
    }

    private void OnEnterClick()
    {
        if (inputText.text == safe.password)
        {
            SoundsController.Play("Correct");
            safe.Unlock();
        }
        else
        {
            SoundsController.Play("Error");
            inputText.text = "";
        }
    }

    private void OnClearClick()
    {
        inputText.text = "";
    }

    private void OnCancelClick()
    {
        GameController.Instance.GameState = GameStates.Game;
    }
}
