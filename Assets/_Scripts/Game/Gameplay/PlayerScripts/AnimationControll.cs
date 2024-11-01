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

    public void OnInputDirectionChanged(Vector2 direction)
    {
        _animator.SetBool(IS_WALK_BOOL, direction.sqrMagnitude > 0);
    }
}
