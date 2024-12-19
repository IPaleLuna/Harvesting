using NaughtyAttributes;
using TMPro;
using Unity.Netcode;
using UnityEngine;


public class NetworkPlayerView : NetworkBehaviour, IPlayerView
{
    [Header("Player visual components"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private NetworkSpriteFlipper networkSpriteFlipper;
    [SerializeField]
    private NetWorkAnimController networkAnimator;

    private readonly NetworkVariable<Vector2> _movementDirection = new(
        Vector2.zero,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
        );

    private readonly NetworkVariable<int> _skinIndex = new();


    public override void OnNetworkSpawn()
    {
        _movementDirection.OnValueChanged += (direction, newDirection) =>
        {   
            SetAnim(newDirection);
            FlipSprite(newDirection);
        };
        _skinIndex.OnValueChanged += (index, newValue) =>
        {
            ApplySpriteLib(newValue);
        };

        SetAnim(_movementDirection.Value);
        ApplySpriteLib(_skinIndex.Value);
    }

    public void UpdateScore(int score)
    {
        if(IsOwner)
            GameEvents.onPlayerUndateScoreEvent.Invoke(score);
    }

    public void UpdateDirection(Vector2 direction)
    {
        if (IsOwner)
            _movementDirection.Value = direction;
    }

    public void ResetAnimations()
    {
        networkAnimator.ResetAnim();
    }

    public void SetNewSkin(int skinIndex)
    {
        _skinIndex.Value = skinIndex;
    }

    private void FlipSprite(Vector2 direction)
    {
        networkSpriteFlipper.OnInputDirectionChanged(direction);
    }
    private void SetAnim(Vector2 newDirection)
    {
        networkAnimator.OnInputDirectionChanged(newDirection);
    }
    public void ApplySpriteLib(int index)
    {
        networkAnimator.SetSpriteLib(index);
    }
}
