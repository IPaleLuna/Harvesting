using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlipper : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    private Transform _shadowSpriteTransform;

    private PlayerInputActions _actions;

    private Vector2 _shadowRight;
    private Vector2 _shadowLeft;

    private void OnValidate()
    {
        _playerSpriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        _shadowRight = _shadowSpriteTransform.localPosition;

        _shadowLeft = _shadowRight;
        _shadowLeft.x *= -1;
    }

    public void OnInputDirectionChanged(Vector2 direction)
    {
        if (direction.x < 0)
        {
            _playerSpriteRenderer.flipX = true;

            _shadowSpriteTransform.localPosition = _shadowLeft;
        }
        else if (direction.x > 0)
        {
            _playerSpriteRenderer.flipX = false;
            _shadowSpriteTransform.localPosition = _shadowRight;
        }
    }
}
