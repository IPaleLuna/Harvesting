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
        _actions = GetComponentInParent<PlayerController>().inputActions;

        _shadowRight = _shadowSpriteTransform.localPosition;

        _shadowLeft = _shadowRight;
        _shadowLeft.x *= -1;
    }

    private void FlipPlayer(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
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

    private void OnEnable()
    {
        _actions.movementAction.performed += FlipPlayer;
    }
    private void OnDisable()
    {
        _actions.movementAction.performed -= FlipPlayer;
    }
}
