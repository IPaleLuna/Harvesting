using PaleLuna.Architecture.Services;
using UnityEngine;

public class SwitchPlayerCount : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playerCards;

    public void OnModeChange(IOptionalData<int> data)
    {
        int playerAmount = data.GetData(StringKeys.PLAYER_COUNT_KEY);
        SetPlayerCards(playerAmount);
    }

    private void SetPlayerCards(int cardsToActive)
    {
        for (int i = 0; i < _playerCards.Length; i++)
            _playerCards[i].SetActive(i < cardsToActive);
    }
}
