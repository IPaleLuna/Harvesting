using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [FormerlySerializedAs("netWorkSpriteFlipper")]
    [FormerlySerializedAs("_spriteFlipper")]
    [Header("Player visual components"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private NetworkSpriteFlipper networkSpriteFlipper;
    [SerializeField]
    private AnimationControll _animationControl;
    
    public void UpdateDirection(Vector2 direction)
    {
        networkSpriteFlipper.OnInputDirectionChanged(direction);
        _animationControl.OnInputDirectionChanged(direction);
    }

    public void ResetAnimations()
    {
        _animationControl.ResetAnim();
    }
}
