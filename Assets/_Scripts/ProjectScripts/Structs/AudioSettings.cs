using System;

[Serializable]
public class AudioSettings 
{
    public const string MIXER_MASTER = "masterVolume";
    public const string MIXER_MUSIC = "musicVolume";
    public const string MIXER_SFX = "sfxVolume";
    
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    public AudioSettings(float masterVolume = 1, float musicVolume = 1, float sfxVolume = 1)
    {
        this.masterVolume = masterVolume;
        this.musicVolume = musicVolume;
        this.sfxVolume = sfxVolume;
    }

    public void Copy(AudioSettings otherAudioSettings)
    {
        this.masterVolume = otherAudioSettings.masterVolume;
        this.musicVolume = otherAudioSettings.musicVolume;
        this.sfxVolume = otherAudioSettings.sfxVolume;
    }
}
