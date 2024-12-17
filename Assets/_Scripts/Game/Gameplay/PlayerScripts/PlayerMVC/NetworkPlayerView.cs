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


    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _playerScoreText;

    private readonly NetworkVariable<Vector2> _movementDirection = new(
        Vector2.zero,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
        );

    private readonly NetworkVariable<ulong> _skinIndex = new(
        0,
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
        _skinIndex.OnValueChanged += (index, newValue) =>
        {
            ApplySpriteLib((int)newValue);
        };
        
        _skinIndex.Value = OwnerClientId;
        
        if (IsClient)
        {
            SetAnim(_movementDirection.Value);
            ApplySpriteLib((int)_skinIndex.Value);
            //FlipSprite(_movementDirection.Value);
        }
    }

    public void UpdateScore(int score)
    {
        _playerScoreText.text = NumToStringBuffer.GetIntToStringHash(score);
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

    public void SetNewSkin(ulong skinIndex)
    {
        if (IsOwner)
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
    private void ApplySpriteLib(int index)
    {
        print("Change");
        networkAnimator.SetSpriteLib(index);
    }
}
