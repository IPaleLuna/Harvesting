using DG.Tweening;
using PaleLuna.Architecture.GameComponent;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TreeFade : MonoBehaviour, IStartable
{
    private const string _PLAYER_TAG = "Player";

    [Header("Auto filling components")]
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [Header("Properties")]
    [SerializeField, Tooltip("anim duration on seconds")]
    private float _duration = 0.5F;
    [SerializeField]
    private Color _targetColor;

    private Color _originColor;

    private int _playerStayInTree = 0;

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    private void OnValidate()
    {
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    public void OnStart()
    {
        if (_isStart) return;
        _isStart = true;

        _originColor = _spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(_PLAYER_TAG))
        {
            _playerStayInTree++;

            if(_playerStayInTree == 1)
            _spriteRenderer.DOColor(_targetColor, _duration).Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_PLAYER_TAG))
        {
            _playerStayInTree--;
            if (_playerStayInTree == 0)
                _spriteRenderer.DOColor(_originColor, _duration).Play();
        }
    }
}
