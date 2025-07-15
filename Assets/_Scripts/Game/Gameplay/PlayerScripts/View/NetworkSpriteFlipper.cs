using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NetworkSpriteFlipper : NetworkBehaviour, IViewComponent
{
    [SerializeField]
    private SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    private Transform _shadowSpriteTransform;
    
    private SpriteFlipper _flipper;
    
    private readonly NetworkVariable<bool> _isFlipped = new(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    private void OnValidate()
    {
        _playerSpriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        _flipper = new(_playerSpriteRenderer, _shadowSpriteTransform);
        
        _isFlipped.OnValueChanged += (value, newValue) => { _flipper.ApplyFlip(newValue); };
        
        _flipper.ApplyFlip(_isFlipped.Value);
    }

    public void OnInputDirectionChanged(Vector2 direction)
    {
        if(!IsOwner) return;
        _isFlipped.Value = _flipper.IsFlip(direction);
    }
}
