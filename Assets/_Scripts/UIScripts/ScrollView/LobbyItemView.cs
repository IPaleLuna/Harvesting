using NaughtyAttributes;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyItemView : MonoBehaviour
{
    [Header("UI elements"), HorizontalLine(color: EColor.Orange)]
    [SerializeField]
    private TextMeshProUGUI _playerCountLabel;
    [SerializeField]
    private TextMeshProUGUI _serverNameLabel;

    public void UpdateInfo(Lobby lobby)
    {
        _playerCountLabel.text = $"{lobby.Players.Count}/{lobby.MaxPlayers}";
        _serverNameLabel.text = lobby.Name;
    }
}


