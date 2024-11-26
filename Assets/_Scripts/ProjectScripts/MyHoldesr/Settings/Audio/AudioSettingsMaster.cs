using GameSettings;
using NaughtyAttributes;
using PaleLuna.Utility;
using Services;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsMaster : MonoBehaviour
{
    [Header("Audio Mixer"), HorizontalLine(color: EColor.Orange)]
    [SerializeField]
    private AudioMixer _mixer;
    
    [Header("Audio sliders"), HorizontalLine(color: EColor.Pink)]
    [SerializeField]
    private SliderView _masterVolumeSlider;
    [SerializeField]
    private SliderView _musicVolumeSlider;
    [SerializeField]
    private SliderView _sfxVolumeSlider;
    
    private SettingsHolder _settingsHolder;
    private AudioSettings _audioSettings;

    private void Start()
    {
        _settingsHolder = ServiceManager.Instance
            .GlobalServices
            .Get<SettingsHolder>();
        _audioSettings = _settingsHolder.settings.audio;
        
        _masterVolumeSlider.OnValueChanged(OnMasterVolumeChanged);
        _musicVolumeSlider.OnValueChanged(OnMusicVolumeChanged);
        _sfxVolumeSlider.OnValueChanged(OnSfxVolumeChanged);
        
        _masterVolumeSlider.SetValue(_audioSettings.masterVolume);
        _musicVolumeSlider.SetValue(_audioSettings.musicVolume);
        _sfxVolumeSlider.SetValue(_audioSettings.sfxVolume);
    }

    private void OnMasterVolumeChanged(float value)
    {
        print("Master change");
        _audioSettings.masterVolume = value;
        _mixer.SetFloat(AudioSettings.MIXER_MASTER, SoundConverter.ConvertVolume(value));
    }

    private void OnMusicVolumeChanged(float value)
    {
        _audioSettings.musicVolume = value;
        _mixer.SetFloat(AudioSettings.MIXER_MUSIC, SoundConverter.ConvertVolume(value));
    }

    private void OnSfxVolumeChanged(float value)
    {
        _audioSettings.sfxVolume = value;
        _mixer.SetFloat(AudioSettings.MIXER_SFX, SoundConverter.ConvertVolume(value));
    }

    public void SaveAudioSettings()
    {
        _settingsHolder.ApplyChanges();
    }
}
