using UnityEngine;

public class DontDestroyWhileChildren : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded(int level)
    {
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
