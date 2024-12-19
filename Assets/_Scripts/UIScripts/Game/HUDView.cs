using TMPro;
using UnityEngine;

public class HUDView : MonoBehaviour
{
    [SerializeField]
    private ScoreCard _scoreLabel;

    private void Awake()
    {
        GameEvents.onPlayerUndateScoreEvent.AddListener(UpdateScore);
    }

    private void UpdateScore(int score)
    {
        _scoreLabel.SetText(NumToStringBuffer.GetIntToStringHash(score));
        _scoreLabel.PlayAnim();
    }
}
