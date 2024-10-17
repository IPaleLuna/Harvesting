using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimationControll : MonoBehaviour
{
    private readonly int IS_WALK_BOOL = Animator.StringToHash("IsWalk");

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

    private void OnInputDirectionChange(InputAction.CallbackContext context)
    {
        Vector2 inputDirection = context.ReadValue<Vector2>();

        _animator.SetBool(IS_WALK_BOOL, inputDirection.sqrMagnitude > 0);
    }

    #region [On Enable/Disable]
    private void OnEnable()
    {
        _actions.movementAction.performed += OnInputDirectionChange;
        _actions.movementAction.canceled += OnInputDirectionChange;
    }

    private void OnDisable()
    {
        _actions.movementAction.performed -= OnInputDirectionChange;
        _actions.movementAction.canceled -= OnInputDirectionChange;

        _animator.SetBool(IS_WALK_BOOL, false);
    }
    #endregion
}
