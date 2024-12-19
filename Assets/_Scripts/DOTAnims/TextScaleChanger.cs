using DG.Tweening;
using UnityEngine;

public class TextScaleChanger
{
    private Tween _tween;

    public void AnimScale(Transform transform, float duration, Vector3 targetScale)
    {
        _tween?.Kill();
        transform.DOScale(targetScale, duration).Play();
    }
}
