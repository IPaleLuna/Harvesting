using TMPro;
using DG.Tweening;
using UnityEngine;

public class ScoreCard : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private TextMeshProUGUI _scoreLabel;
    [SerializeField]
    private RectTransform _rectTransform;

    [Header("Anim propertris")]
    [SerializeField]
    private float _animDuration = 0.2F;
    [SerializeField]
    private Vector3 _targetScale;

    private Vector3 _originScale;

    private Sequence _animSequence;

    private void OnValidate()
    {
        _scoreLabel = GetComponentInChildren<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _originScale = _rectTransform.localScale;

        SetUpAnimSeq();
    }

    public void SetText(string text)
    {
        _scoreLabel.SetText( "x" + text);
    }
    public void PlayAnim()
    {
        _animSequence.Restart();
    }

    private void SetUpAnimSeq()
    {
        _animSequence = DOTween.Sequence().SetAutoKill(false);

        _animSequence.Append(_rectTransform.DOScale(_targetScale, _animDuration));
        _animSequence.Append(_rectTransform.DOScale(_originScale, _animDuration));
    }
}
