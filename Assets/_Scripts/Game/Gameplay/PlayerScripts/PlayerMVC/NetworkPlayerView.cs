using NaughtyAttributes;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerView : NetworkBehaviour, IPlayerView
{
    [Header("Player visual components"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private SpriteFlipper _spriteFlipper;
    [SerializeField]
    private AnimationControll _animationControl;

    private NetworkVariable<Vector2> _movementDirection = new(
        Vector2.zero,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
        );

    public override void OnNetworkSpawn()
    {
        _movementDirection.OnValueChanged += (direction, newDirection) =>
        {   
            SetAnim(newDirection);
            FlipSprite(newDirection);
        };
        
        if (IsClient)
        {
            SetAnim(_movementDirection.Value);
            //FlipSprite(_movementDirection.Value);
        }
    }
    
    public void UpdateDirection(Vector2 direction)
    {
        if (IsOwner)
            _movementDirection.Value = direction;
    }

    public void ResetAnimations()
    {
        _animationControl.ResetAnim();
    }

    private void FlipSprite(Vector2 direction)
    {
        _spriteFlipper.OnInputDirectionChanged(direction);
    }

    private void SetAnim(Vector2 newDirection)
    {
        _animationControl.OnInputDirectionChanged(newDirection);
    }
    
    public void DestroySelf()
    {
        Destroy(this);
    }
}
