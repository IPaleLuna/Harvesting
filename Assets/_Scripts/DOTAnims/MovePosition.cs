using DG.Tweening;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    [Header("Points")]
    [SerializeField]
    private Transform _startPosition;
    [SerializeField]
    private Transform _endPosition;
    
    [Header("AnimParams")]
    [SerializeField]
    private float _speed;
    
    private Tween _tween;


    public void ToEndPoint()
    {
        if(_tween != null) _tween.Kill();
        _tween = transform.DOMove(_endPosition.position, _speed).Play();
    }

    public void ToStartPoint()
    {
        if(_tween != null) _tween.Kill();
        _tween = transform.DOMove(_startPosition.position, _speed).Play();
    }
}
