using UnityEngine;

public class DoorObject : Saveable, IUse
{
    public GameObject door;
    public bool isLocked = false;
    public string soundId;
    public GameObject activeIndicator;
    public GameObject inactiveIndicator;
    public float yOffset = -2.3f;

    private bool isOpen = false;
    public string Name { get { return Localization.Get("Objects.Door"); } }
    public bool IsActive { get { return !isOpen; } }

    protected virtual void Start()
    {
        RefreshIndicators();
    }

    void Update()
    {
        if (isOpen && door.transform.localPosition.y > yOffset)
        {
            door.transform.Translate(Vector3.down * 10 * Time.deltaTime);
            if (door.transform.localPosition.y < yOffset) door.transform.SetYL(yOffset);
        }
    }

    public void Use()
    {
        if (isLocked)
        {
            SoundsController.Play("Error");
        }
        else
        {
            SoundsController.Play(soundId);
            isOpen = true;
            GetComponent<Collider>().enabled = false;
        }
    }

    public void SetLock(bool state)
    {
        isLocked = state;
        RefreshIndicators();
    }

    private void RefreshIndicators()
    {
        if (activeIndicator != null) activeIndicator.SetActive(isLocked == false);
        if (inactiveIndicator != null) inactiveIndicator.SetActive(isLocked == true);
    }

    public override SaveInfo Save()
    {
        SaveInfo saveInfo = new SaveInfo();
        saveInfo.WriteProperty("isOpen", isOpen);
        saveInfo.WriteProperty("isLocked", isLocked);
        return saveInfo;
    }

    public override void Load(SaveInfo saveInfo)
    {
        isOpen = saveInfo.ReadProperty<bool>("isOpen");
        isLocked = saveInfo.ReadProperty<bool>("isLocked");
        if (isOpen)
        {
            door.transform.SetYL(yOffset);
            GetComponent<Collider>().enabled = false;
        }
    }
}
