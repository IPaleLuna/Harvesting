using PaleLuna.Architecture.Services;
using UnityEngine;

public class SwitchPlayerCount : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _lastPlayerCards;

    public void OnModeChange(IOptionalData<int> data)
    {
        PlayerCount gameMode = (PlayerCount)data.GetData(PlayerPrefsKeys.PLAYER_COUNT_KEY);

        switch (gameMode)
        {
            case PlayerCount.Two:
                SwitchCards(false);
                break;
            case PlayerCount.Four:
                SwitchCards(true);
                break;
        }
    }
    
    private void SwitchCards(bool flag)
    {
        for (int i = 0; i < _lastPlayerCards.Length; i++)
            _lastPlayerCards[i].SetActive(flag);
    }
}
