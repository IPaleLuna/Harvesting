using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [Header("Player visual components"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private MonoSpriteFlipper _monoSpriteFlipper;
    [FormerlySerializedAs("_animationControl")] [SerializeField]
    private MonoAnimationController monoAnimationControl;
    
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _playerScoreText;

    public void UpdateScore(int score)
    {
        
        //_playerScoreText.text = NumToStringBuffer.GetIntToStringHash(score);
    }

    public void UpdateDirection(Vector2 direction)
    {
        _monoSpriteFlipper.OnInputDirectionChanged(direction);
        monoAnimationControl.OnInputDirectionChanged(direction);
    }

    public void ResetAnimations()
    {
        monoAnimationControl.ResetAnim();
    }
}
