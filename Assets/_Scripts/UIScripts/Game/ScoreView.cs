using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private ScoreCard[] _labels;
    private int _parentPlayerIndex;

    private void Start()
    {
       _labels = transform.GetComponentsInChildren<ScoreCard>();

        _parentPlayerIndex = transform.root.GetComponentInChildren<Player>().playerID;

        GameEvents.playerPickApple.AddListener(UpdateScore);

        SortLabels();
    }

    private void SortLabels()
    {
        for (int i = 0; i < _parentPlayerIndex; i++)
        {
            ScoreCard temp = _labels[i + 1];
            _labels[i + 1] = _labels[i];
            _labels[i] = temp;
        }
    }

    private void UpdateScore(Player player)
    {
        _labels[player.playerID].SetText(NumToStringBuffer.GetIntToStringHash(player.applesAmount));
        _labels[player.playerID].PlayAnim();
    }
}
