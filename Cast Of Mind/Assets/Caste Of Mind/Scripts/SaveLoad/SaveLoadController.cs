using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadController : MonoBehaviour
{
    public static string Path { get { return Application.persistentDataPath; } }

    public static bool IsSave
    {
        get
        {
            if (Application.isWebPlayer) return false;
            else return File.Exists(Path + "/game.save");
        }
    }

    public static void SaveGame(SaveGameData data)
    {
        if (Application.isWebPlayer) return;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Path + "/game.save", FileMode.Create);
            bf.Serialize(fs, data);
            fs.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Load Game: " + e.Message);
        }
    }

    public static SaveGameData LoadGame()
    {
        if (Application.isWebPlayer) return null;
        if (IsSave == false) return null;

        Debug.Log("Start loading");
        SaveGameData data = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Path + "/game.save", FileMode.Open);
            data = (SaveGameData)bf.Deserialize(fs);
            fs.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Load Game: " + e.Message);
            return null;
        }

        return data;
    }

    public static void DeleteSaveGame()
    {
        if (IsSave == false) return;
        try
        {
            File.Delete(Path + "/game.save");
        }
        catch (Exception e)
        {
            Debug.LogWarning("Load Game: " + e.Message);
        }
    }


    public static void SaveOptions(OptionsConfig config)
    {
        if (Application.isWebPlayer) return;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Path + "/options.save", FileMode.Create);
            bf.Serialize(fs, config);
            fs.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Load Game: " + e.Message);
        }
    }

    public static OptionsConfig LoadOptions()
    {
        if (Application.isWebPlayer) return null;
        if (!File.Exists(Application.persistentDataPath + "/options.save")) return null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Path + "/options.save", FileMode.Open);
            OptionsConfig data = (OptionsConfig)bf.Deserialize(fs);
            fs.Close();
            return data;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Load Game: " + e.Message);
            return null;
        }
    }
}
