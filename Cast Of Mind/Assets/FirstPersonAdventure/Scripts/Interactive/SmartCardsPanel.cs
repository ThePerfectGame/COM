using UnityEngine;
using System.Collections.Generic;

public class SmartCardsPanel : ActionsObject
{
    public DoorObject Door;
    public GameObject redKeyObject;
    public GameObject greenKeyObject;
    public GameObject blueKeyObject;
    public GameObject activeIndicator;
    public GameObject inactiveIndicator;

    public bool isRedPlaced = false;
    public bool isGreenPlaced = false;
    public bool isBluePlaced = false;

    void Start()
    {
        if (redKeyObject != null) redKeyObject.SetActive(isRedPlaced);
        if (greenKeyObject != null) greenKeyObject.SetActive(isGreenPlaced);
        if (blueKeyObject != null) blueKeyObject.SetActive(isBluePlaced);
        RefreshIndicators();
    }

    public override List<InteractAction> GetActions()
    {
        List<InteractAction> actions = new List<InteractAction>();
        if (isRedPlaced == false && InventoryController.Instance.GetItemById("i_key_red") != null)
        {
            actions.Add(new InteractAction(Localization.Get("Actions.PlaceRedCard"), PlaceRedKey));
        }
        if (isGreenPlaced == false && InventoryController.Instance.GetItemById("i_key_green") != null)
        {
            actions.Add(new InteractAction(Localization.Get("Actions.PlaceGreenCard"), PlaceGreenKey));
        }
        if (isBluePlaced == false && InventoryController.Instance.GetItemById("i_key_blue") != null)
        {
            actions.Add(new InteractAction(Localization.Get("Actions.PlaceBlueCard"), PlaceBlueKey));
        }
        return actions;
    }

    private void PlaceRedKey()
    {
        SoundsController.Play("Correct");
        InventoryController.Instance.RemoveItem("i_key_red");
        isRedPlaced = true;
        redKeyObject.SetActive(true);
        Check();
    }

    private void PlaceGreenKey()
    {
        SoundsController.Play("Correct");
        InventoryController.Instance.RemoveItem("i_key_green");
        isGreenPlaced = true;
        greenKeyObject.SetActive(true);
        Check();
    }

    private void PlaceBlueKey()
    {
        SoundsController.Play("Correct");
        InventoryController.Instance.RemoveItem("i_key_blue");
        isBluePlaced = true;
        blueKeyObject.SetActive(true);
        Check();
    }

    private void Check()
    {
        if (isRedPlaced && isGreenPlaced && isBluePlaced) Door.SetLock(false);
        RefreshIndicators();
    }

    private void RefreshIndicators()
    {
        if (activeIndicator != null) activeIndicator.SetActive(Door.isLocked == false);
        if (inactiveIndicator != null) inactiveIndicator.SetActive(Door.isLocked == true);
    }

    public override SaveInfo Save()
    {
        SaveInfo saveInfo = new SaveInfo();
        saveInfo.WriteProperty("isRedPlaced", isRedPlaced);
        saveInfo.WriteProperty("isGreenPlaced", isGreenPlaced);
        saveInfo.WriteProperty("isBluePlaced", isBluePlaced);
        return saveInfo;
    }

    public override void Load(SaveInfo saveInfo)
    {
        isRedPlaced = saveInfo.ReadProperty<bool>("isRedPlaced");
        isGreenPlaced = saveInfo.ReadProperty<bool>("isGreenPlaced");
        isBluePlaced = saveInfo.ReadProperty<bool>("isBluePlaced");
    }
}
