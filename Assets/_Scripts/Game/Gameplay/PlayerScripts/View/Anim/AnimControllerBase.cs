using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControllerBase
{
    private readonly int IS_WALK_BOOL = Animator.StringToHash("IsWalk");

    private readonly Animator _animator;

    public AnimControllerBase(Animator animator)
    {
        _animator = animator;
    }

    public void OnInputDirectionChanged(Vector2 direction)
    {
        _animator.SetBool(IS_WALK_BOOL, direction.sqrMagnitude > 0);
    }

    public void ResetAnim()
    {
        _animator.SetBool(IS_WALK_BOOL, false);
    }

    public void SetSpriteLib(int index)
    {
        throw new System.NotImplementedException();
    }
}
