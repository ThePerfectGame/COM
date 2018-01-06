using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnterPoint
{
    public string locationName;
    public GameObject point;
}

[ExecuteInEditMode]
public class Room : MonoBehaviour
{
    public EnterPoint[] enterPoints;
    public Saveable[] saveables;

    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false) FindSavebleObjects();
#endif
    }

    public void Enter(string prevLocationName)
    {
        foreach (EnterPoint point in enterPoints)
        {
            if (point.locationName == prevLocationName)
            {
                PlayerController.Instance.MoveToPoint(point.point.transform.position, point.point.transform.rotation);
            }
        }
    }

    public List<SaveInfo> SaveState()
    {
        List<SaveInfo> saveInfos = new List<SaveInfo>();
        for (int i = 0; i < saveables.Length; i++)
        {
            if (saveables[i] == null || saveables[i].gameObject == null)
            {
                saveInfos.Add(new SaveInfo(true));
            }
            else
            {
                saveInfos.Add(saveables[i].Save());
            }
        }
        return saveInfos;
    }

    public void LoadState(List<SaveInfo> saveInfos)
    {
        for (int i = 0; i < saveables.Length; i++)
        {
            if (saveInfos[i].IsDestroyed)
            {
                Destroy(saveables[i].gameObject);
            }
            else
            {
                saveables[i].Load(saveInfos[i]);
            }
        }
    }

    private void FindSavebleObjects()
    {
        saveables = FindObjectsOfType<Saveable>();
    }
}
