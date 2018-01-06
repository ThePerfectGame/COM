using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : Form
{
    public event System.Action onBack;

    public Button applyButton;
    public Button defaultButton;
    public Button backButton;

    public Toggle inverseMouseY;
    public Slider soundVolume;
    public Slider mouseSence;
    public Slider language;

    public Text soundVolumeText;
    public Text mouseSenceText;
    public Text languageText;

    void Start()
    {
        applyButton.onClick.AddListener(OnApplyClick);
        defaultButton.onClick.AddListener(OnDefaultClick);
        backButton.onClick.AddListener(OnBackClick);
        soundVolume.onValueChanged.AddListener((v) =>  RefreshLabels());
        mouseSence.onValueChanged.AddListener((v) => RefreshLabels());
        language.onValueChanged.AddListener((v) => RefreshLabels());
    }

    public override void Show()
    {
        base.Show();
        RefreshValues();
    }

    private void OnApplyClick()
    {
        OptionsConfig config = new OptionsConfig();
        config.inverseMouseY = inverseMouseY.isOn;
        config.soundVolume = soundVolume.value;
        config.mouseSence = mouseSence.value;
        config.language = (int)language.value;
        OptionsController.Instance.SetOptions(config);
        if (onBack != null) onBack();
    }

    private void RefreshValues()
    {
        OptionsConfig config = OptionsController.Instance.Config;

        inverseMouseY.isOn = config.inverseMouseY;
        soundVolume.value = config.soundVolume;
        mouseSence.value = config.mouseSence;
        language.value = config.language;
        RefreshLabels();
    }

    private void RefreshLabels()
    {
        soundVolumeText.text = (Mathf.CeilToInt(soundVolume.value * 100f)) + "%";
        mouseSenceText.text = (Mathf.CeilToInt(mouseSence.value * 100f)) + "%";
        languageText.text = Localization.Instance.languages[Mathf.CeilToInt(language.value)].name;
    }

    private void OnDefaultClick()
    {
        OptionsConfig defaultConfig = OptionsController.Instance.GetDefaultOptions();
        OptionsController.Instance.SetOptions(defaultConfig);
        RefreshValues();
    }

    private void OnBackClick()
    {
        if (onBack != null) onBack();
    }
}
