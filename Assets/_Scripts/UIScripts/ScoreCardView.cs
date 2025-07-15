using TMPro;
using UnityEngine;

public class ScoreCardView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _placeLabel;
    [SerializeField]
    private TextMeshProUGUI _nameLabel;
    [SerializeField]
    private TextMeshProUGUI _scoreLabel;

    public void UpdateCard(int place, string name, int score)
    {
        _placeLabel.text = NumToStringBuffer.GetIntToStringHash(place);
        _nameLabel.text = name;
        _scoreLabel.text = NumToStringBuffer.GetIntToStringHash(score);
    }
}
