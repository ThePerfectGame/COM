using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float autoLoadafter;

    private void Start()
    {
        if (autoLoadafter > 0)
        {
            Invoke("LoadNextLevel", autoLoadafter);
        }

    }
    public void LoadLevel(string name)
    {

        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {

        Application.Quit();
    }

    public void LoadNextLevel()
    {
        int currentSceenIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceenIndex + 1);
    }
}

