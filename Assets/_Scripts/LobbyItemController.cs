using Cysharp.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

[RequireComponent(typeof(LobbyItemView))]
public class LobbyItemController : ListItem
{
    [SerializeField]
    private LobbyItemView _lobbyItemView;
    
    private Lobby _currentLobby;

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
        if(_currentLobby == null) return;
        _ = ConnectAsync();
    }

    private async UniTaskVoid ConnectAsync()
    {
        try
        {
            await Lobbies.Instance.JoinLobbyByIdAsync(_currentLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogException(e);
        }
    }
}
