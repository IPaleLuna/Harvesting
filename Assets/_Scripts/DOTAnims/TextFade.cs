using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextFade
{
    private Tween _tween;

    public void AnimText(TextMeshProUGUI text, float duration, Color targetColor)
    {
        _tween?.Kill();
        text.DOColor(targetColor, duration).Play();
    }
}
