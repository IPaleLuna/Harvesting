using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackstageScreen : MonoBehaviour
{
    [Header("Auto filling"), HorizontalLine(color: EColor.Red)]
    [SerializeField]
    private Image _image;

    [Header("Aimation Params"), HorizontalLine(color: EColor.Violet)]
    [SerializeField]
    private float _animDurationInSeconds;
    [SerializeField]
    private Color _targetColor;
    [SerializeField]
    private Color _originColor;

    private Tween _tween;

    private void OnValidate()
    {
        _image ??= GetComponent<Image>();
    }
    private void Awake()
    {
        _image.color = _originColor;
    }

    #region [FadeIn]
    public void FadeIn()
    {
        _tween?.Kill();
        _tween = _image.DOColor(_targetColor, _animDurationInSeconds).Play();
    }

    public void FadeIn(UnityAction callback)
    {
        _tween?.Kill();
        _tween = _image.DOColor(_targetColor, _animDurationInSeconds).OnComplete(() => callback.Invoke()).Play();
    }
    #endregion

    #region [FadeOut]
    public void FadeOut()
    {
        _tween?.Kill();
        _tween = _image.DOColor(_originColor, _animDurationInSeconds).Play();
    }

    public void FadeOut(UnityAction callback)
    {
        _tween?.Kill();
        _tween = _image.DOColor(_originColor, _animDurationInSeconds).OnComplete(() => callback.Invoke()).Play();
    }
    #endregion

}
