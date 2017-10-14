using UnityEngine;

public class DoorLocationObject : Saveable, IUse
{
    public string locationName;
    public string soundId;
    public bool isLocked = false;
    public GameObject activeIndicator;
    public GameObject inactiveIndicator;

    public string Name { get { return Localization.Get("Objects.Door"); } }
    public bool IsActive { get { return true; } }

    protected virtual void Start()
    {
        RefreshIndicators();
    }

    public void Use()
    {
        if (isLocked)
        {
            SoundsController.Play("Error");
        }
        else
        {
            GameController.Instance.LoadLevel(locationName, true, null);
            SoundObject soundObject = SoundsController.Play(soundId);
            if (soundObject != null) soundObject.dontDestroyOnLoad = true;
        }
    }

    private void RefreshIndicators()
    {
        if (activeIndicator != null) activeIndicator.SetActive(isLocked == false);
        if (inactiveIndicator != null) inactiveIndicator.SetActive(isLocked == true);
    }

    public override SaveInfo Save()
    {
        SaveInfo saveInfo = new SaveInfo();
        saveInfo.WriteProperty("isLocked", isLocked);
        return saveInfo;
    }

    public override void Load(SaveInfo saveInfo)
    {
        isLocked = saveInfo.ReadProperty<bool>("isLocked");
    }
}
