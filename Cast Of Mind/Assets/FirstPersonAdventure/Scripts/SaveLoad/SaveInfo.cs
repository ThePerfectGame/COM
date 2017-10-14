using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveInfo
{
    public bool IsDestroyed { get; private set; }
    private Dictionary<string, object> properties = new Dictionary<string, object>();

    public SaveInfo() { }

    public SaveInfo(bool isDestroyed)
    {
        IsDestroyed = isDestroyed;
    }

    public void WriteProperty(string key, object value)
    {
        properties.Add(key, value);
    }

    public object ReadProperty(string key)
    {
        if (properties.ContainsKey(key) == false)
        {
            Debug.LogWarning("Can't find property: " + key);
            return null;
        }
        return properties[key];
    }

    public T ReadProperty<T>(string key)
    {
        return (T) ReadProperty(key);
    }
}
