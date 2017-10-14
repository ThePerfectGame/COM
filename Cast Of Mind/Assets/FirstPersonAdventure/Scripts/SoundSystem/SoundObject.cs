using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundObject : MonoBehaviour
{
    public bool dontDestroyOnLoad = false;

    private AudioSource source;
    private SoundItem soundItem;

    void Start()
    {
        OptionsController.Instance.onConfigChanged += OnOptionsChanged;
        if (dontDestroyOnLoad) DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (source.isPlaying == false) Destroy(gameObject);
    }

    void OnDestroy()
    {
        OptionsController.Instance.onConfigChanged -= OnOptionsChanged;
    }

    public void SetClip(SoundItem sound)
    {
        soundItem = sound;
        if (source == null) source = GetComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume * OptionsController.Instance.Config.soundVolume;
        source.spatialBlend = sound.is2d ? 0 : 1;
        source.Play();
    }

    private void OnOptionsChanged(OptionsConfig config)
    {
        source.volume = soundItem.volume * config.soundVolume;
    }
}
