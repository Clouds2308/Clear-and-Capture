using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public enum AudioChannel { Master, Music, Sfx};

    public float masterVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
        }
        else
            instance = this;

        masterVolumePercent = 1f;
        musicVolumePercent = 1f;
        sfxVolumePercent = 0.3f;
    }

    public void PlaySound(AudioClip clip , Vector3 pos)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
    }   

    public void SetVoulme(float volumePercent,AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
        }

    }
      
}
