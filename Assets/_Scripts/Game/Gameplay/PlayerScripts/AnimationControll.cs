using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimationControll : MonoBehaviour
{
    private readonly int IDLE_ANIM_HASH = Animator.StringToHash("Idle");
    private readonly int WALK_ANIM_HASH = Animator.StringToHash("Walk");

    [Header("Autofilling components")]
    [SerializeField]
    private Animator _animator;

    private PlayerInputActions _actions;

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    private void Awake()
    {
        _actions = GetComponentInParent<PlayerController>().inputActions;
        GameEvents.timeOutEvent.AddListener(OnDisable);
    }

    private void OnEnable()
    {
        _actions.movementAction.performed += OnCharacterMove;
        _actions.movementAction.canceled += OnCharacterStand;
    }

    private void OnCharacterStand(InputAction.CallbackContext context)
    {
        if(context.ReadValue<Vector2>().Equals(Vector2.zero))
        _animator.Play(IDLE_ANIM_HASH);
    }

    private void OnCharacterMove(InputAction.CallbackContext context)
    {
        _animator.Play(WALK_ANIM_HASH);
    }

    private void OnDisable()
    {
        _actions.movementAction.performed -= OnCharacterMove;
        _actions.movementAction.canceled -= OnCharacterStand;

        _animator.Play(IDLE_ANIM_HASH);
    }
}
