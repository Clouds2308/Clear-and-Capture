using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public enum AudioChannel { Master, Music, Sfx};

    float masterVolumePercent = .2f;
    float musicVolumePercent = 1f;
    float sfxVolumePercent = 1f;

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
        }
        else
            instance = this;    
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
