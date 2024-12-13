using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MonoAnimationController : MonoBehaviour, IAnimController
{
    [Header("Autofilling components")]
    [SerializeField]
    private Animator _animator;
    
    private AnimControllerBase _animController;

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    private void Awake()
    {
        _animController = new AnimControllerBase(_animator);
    }
    
    public void OnInputDirectionChanged(Vector2 direction)
    {
        _animController?.OnInputDirectionChanged(direction);
    }

    public void ResetAnim()
    {
        _animController?.ResetAnim();
    }

    public void SetSpriteLib(int index)
    {
        throw new System.NotImplementedException();
    }
}
