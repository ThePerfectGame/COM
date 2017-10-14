using UnityEngine;

public abstract class Saveable : MonoBehaviour
{
    public abstract SaveInfo Save();
    public abstract void Load(SaveInfo saveInfo);
}
