using DG.Tweening;
using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgrounMusic : MonoBehaviour, IStartable
{
    [Header("Audio Clips (Auto filling)"), HorizontalLine(color: EColor.Pink)]
    [SerializeField]
    private AudioSource _audioSource;
    
    [Header("Clip"), HorizontalLine(color: EColor.Violet)]
    [SerializeField]
    private AudioClip _clip;

    [Header("Sound Settings"), HorizontalLine(color: EColor.Blue)]
    [SerializeField]
    private float _animDuration;
    [SerializeField, Range(0, 1)]
    private float _clipTargetVolume;
    
    private Tween _tween;

    public bool IsStarted { get; private set; } = false;

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }
    
    public void OnStart()
    {
        if(IsStarted) return;
        IsStarted = true;
        
        _audioSource.clip = _clip;
        _audioSource.volume = 0;
    }

    public void SmoothPlay()
    {
        if(_tween != null) _tween.Kill();
        _audioSource.Play();
        _tween = _audioSource.DOFade(_clipTargetVolume, _animDuration).Play();
    }

    public void SmoothStop()
    {
        if(_tween != null) _tween.Kill();
        _tween = _audioSource.DOFade(0f, _animDuration).OnComplete(() => _audioSource.Stop()).Play();
    }

    
}
