using Cysharp.Threading.Tasks;
using GameSettings;
using NaughtyAttributes;
using PaleLuna.Architecture.Initializer;
using PaleLuna.Utility;
using Services;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsInit : InitializerBaseMono
{
    [Header("Audio Mixer"), HorizontalLine(color: EColor.Orange)]
    [SerializeField]
    private AudioMixer _mixer;
    
    public override void StartInit()
    {
        _status = InitStatus.Initialization;

        _ = Init();
    }

    private async UniTaskVoid Init()
    {
        SettingsHolder settings = new SettingsHolder();
        if(!settings.LoadSettings()) settings.ApplyChanges();
        
        await UniTask.WaitForSeconds(0.1F);
        
        _mixer.SetFloat(AudioSettings.MIXER_MASTER, SoundConverter.ConvertVolume(settings.settings.audio.masterVolume));
        _mixer.SetFloat(AudioSettings.MIXER_MUSIC, SoundConverter.ConvertVolume(settings.settings.audio.musicVolume));
        _mixer.SetFloat(AudioSettings.MIXER_SFX, SoundConverter.ConvertVolume(settings.settings.audio.sfxVolume));
         
        ServiceManager.Instance.GlobalServices.Registarion(settings);
        
        _status = InitStatus.Done;
    }
}
