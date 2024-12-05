using NaughtyAttributes;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Player visual components"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private SpriteFlipper _spriteFlipper;
    [SerializeField]
    private AnimationControll _animationControl;
    
    public void UpdateDirection(Vector2 direction)
    {
        _spriteFlipper.OnInputDirectionChanged(direction);
        _animationControl.OnInputDirectionChanged(direction);
    }

    public void ResetAnimations()
    {
        _animationControl.ResetAnim();
    }
}
