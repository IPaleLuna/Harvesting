using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private ScoreCard[] _labels;
    private int _parentPlayerIndex;

    private void Start()
    {
       _labels = transform.GetComponentsInChildren<ScoreCard>();

        _parentPlayerIndex = transform.root.GetComponentInChildren<PlayerController>().playerInfo.playerID;

        GameEvents.playerPickApple.AddListener(UpdateScore);

        SortLabels();
    }

    private void SortLabels()
    {
        for (int i = 0; i < _parentPlayerIndex; i++)
            (_labels[i + 1], _labels[i]) = (_labels[i], _labels[i + 1]);
    }

    private void UpdateScore(PlayerController playerController)
    {
        PlayerInfo playerInfo = playerController.playerInfo;
        
        _labels[playerInfo.playerID].SetText(NumToStringBuffer.GetIntToStringHash(playerInfo.appleAmount));
        _labels[playerInfo.playerID].PlayAnim();
    }
}
