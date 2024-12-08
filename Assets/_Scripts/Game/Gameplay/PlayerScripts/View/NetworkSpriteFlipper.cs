using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class NetworkSpriteFlipper : NetworkBehaviour
{
    [SerializeField]
    private SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    private Transform _shadowSpriteTransform;

    private PlayerInputActions _actions;

    private Vector2 _shadowRight;
    private Vector2 _shadowLeft;
    
    
    private NetworkVariable<bool> _isFlipped = new(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    private void OnValidate()
    {
        _playerSpriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        _isFlipped.OnValueChanged += (value, newValue) =>
        {
            ApplyFlip(newValue);
        };

        _shadowRight = _shadowSpriteTransform.localPosition;
        _shadowLeft = _shadowRight;
        _shadowLeft.x *= -1;
        
        ApplyFlip(_isFlipped.Value);
    }

    public void OnInputDirectionChanged(Vector2 direction)
    {
        if(!IsOwner) return;
        
        if (direction.x < 0)
            _isFlipped.Value = true;
        else if (direction.x > 0)
            _isFlipped.Value = false;
    }

    private void ApplyFlip(bool value)
    {
        print($"playerID: {this.OwnerClientId}/flipped: {value}");
        
        _playerSpriteRenderer.flipX = value;
        _shadowSpriteTransform.localPosition = value ? _shadowLeft : _shadowRight;
    }
}
