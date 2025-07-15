using Harvesting.Networking;
using Unity.Services.Lobbies.Models;
using UnityEngine;

[RequireComponent(typeof(LobbyItemView))]
public class LobbyItemController : ListItem
{
    [SerializeField]
    private LobbyItemView _lobbyItemView;
    
    private Lobby _currentLobby;
    
    private NetworkLobbyManager _networkLobbyManager;

    private void OnValidate()
    {
        _lobbyItemView ??= GetComponent<LobbyItemView>();
    }

    public void UpdateLobbyItemData(Lobby lobby)
    {
        _lobbyItemView.UpdateInfo(lobby);
        _currentLobby = lobby;
    }

    public void ConnectToLobby()
    {
        _networkLobbyManager.JoinLobbyById(_currentLobby.Id);
    }
    
}
