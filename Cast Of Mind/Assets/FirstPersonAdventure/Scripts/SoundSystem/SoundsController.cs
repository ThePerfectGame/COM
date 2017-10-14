using UnityEngine;

public class SoundsController : Singleton<SoundsController>
{
    public GameObject soundObjectPrefab;

    public static SoundObject Play(string id)
    {
        SoundObject sound = Instance.PlaySound(id);
        return sound;
    }

    public static SoundObject Play(string id, Vector3 pos)
    {
        SoundObject sound = Instance.PlaySound(id);
        if (sound == null) return null;

        sound.transform.position = pos;
        return sound;
    }

    public static SoundObject Play(string id, Transform tran)
    {
        SoundObject sound = Instance.PlaySound(id);
        if (sound == null) return null;

        sound.transform.parent = tran;
        sound.transform.localPosition = Vector3.zero;
        return sound;
    }

    private SoundObject PlaySound(string id)
    {
        SoundItem item = SoundsManager.Instance.GetSoundById(id);
        if (item == null) return null;
        SoundObject sound = ((GameObject)Instantiate(soundObjectPrefab)).GetComponent<SoundObject>();
        sound.SetClip(item);
        return sound;
    }
}
