using UnityEngine;

public class Generator : CatcherObject
{
    public GameObject activeIndicator;
    public GameObject inactiveIndicator;

    void Start()
    {
        RefreshIndicators();
    }

    public void OnCatchBattery()
    {
        SoundsController.Play("Correct");
        GameController.Instance.Values.restoredGenerators += 1;
        RefreshIndicators();
    }

    private void RefreshIndicators()
    {
        if (activeIndicator != null) activeIndicator.SetActive(isCaught == true);
        if (inactiveIndicator != null) inactiveIndicator.SetActive(isCaught == false);
    }
}
