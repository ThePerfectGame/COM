public class OptionsController : Singleton<OptionsController>
{
    public event System.Action<OptionsConfig> onConfigChanged;

    public OptionsConfig Config { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        OptionsConfig optionsConfig = SaveLoadController.LoadOptions();
        if (optionsConfig == null) optionsConfig = GetDefaultOptions();
        SetOptions(optionsConfig);
    }

    public void SetOptions(OptionsConfig config)
    {
        Config = config;
        SaveLoadController.SaveOptions(Config);
        Localization.Instance.SetLanguage(Config.language);
        if (onConfigChanged != null) onConfigChanged(Config);
    }

    public OptionsConfig GetDefaultOptions()
    {
        OptionsConfig config = new OptionsConfig();
        return config;
    }
}
