using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationControll : MonoBehaviour
{
    private readonly int IDLE_ANIM_HASH = Animator.StringToHash("Idle");
    private readonly int WALK_ANIM_HASH = Animator.StringToHash("Walk");

    [Header("Autofilling components")]
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private PlayerInputActions _actions;

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        _actions = GetComponentInParent<PlayerController>().inputActions;
    }

    private void OnEnable()
    {
        _actions.movementAction.performed += OnCharacterMove;
        _actions.movementAction.performed += TryFlipSprite;
        _actions.movementAction.canceled += OnCharacterStand;
    }

    private void OnCharacterStand(InputAction.CallbackContext context)
    {
        _animator.Play(IDLE_ANIM_HASH);
    }

    private void OnCharacterMove(InputAction.CallbackContext context)
    {
        _animator.Play(WALK_ANIM_HASH);
    }

    private void TryFlipSprite(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if (direction.x < 0) _spriteRenderer.flipX = true;
        else if (direction.x > 0) _spriteRenderer.flipX = false;
    }

    private void OnDisable()
    {
        _actions.movementAction.performed -= OnCharacterMove;
        _actions.movementAction.performed -= TryFlipSprite;
        _actions.movementAction.canceled -= OnCharacterStand;
    }
}
