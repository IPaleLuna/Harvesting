using Unity.Netcode;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class NetWorkAnimController : NetworkBehaviour
{
    [Header("Autofilling components")]
    [SerializeField]
    private Animator _animator;
    
    [SerializeField]
    private SpriteLibrary _spriteLibrary;
    [SerializeField]
    private SpriteLibraryAsset[] _libs;
    
    
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
        if(index >= 0 && index < _libs.Length) _spriteLibrary.spriteLibraryAsset = _libs[index];
        else _spriteLibrary.spriteLibraryAsset = _libs[0];
    }

    
}
