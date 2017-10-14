using UnityEngine;

[System.Serializable]
public class SoundItem
{
    public string id;
    public AudioClip clip;
    public float volume = 1f;
    public bool is2d = false;
}
