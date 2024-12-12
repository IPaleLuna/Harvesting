using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [Header("Player visual components"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private MonoSpriteFlipper _monoSpriteFlipper;
    [SerializeField]
    private AnimationControll _animationControl;
    
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _playerScoreText;

    public void UpdateScore(int score)
    {
        _playerScoreText.text = NumToStringBuffer.GetIntToStringHash(score);
    }

    public void UpdateDirection(Vector2 direction)
    {
        _monoSpriteFlipper.OnInputDirectionChanged(direction);
        _animationControl.OnInputDirectionChanged(direction);
    }

    public void ResetAnimations()
    {
        _animationControl.ResetAnim();
    }
}
