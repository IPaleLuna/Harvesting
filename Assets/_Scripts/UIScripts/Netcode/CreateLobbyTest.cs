using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class CreateLobbyTest : MonoBehaviour
{
    public void CreateLobby()
    {
        _ = CreateLobbyAsync("Test name", 4);
    }

    
    private async UniTaskVoid CreateLobbyAsync(string lobbyName, int maxPlayers)
    {
        try
        {
            // Параметры лобби
            var lobby = await LobbyService.Instance.CreateLobbyAsync(
                lobbyName,
                maxPlayers,
                new CreateLobbyOptions
                {
                    IsPrivate = false, // Публичное лобби
                    Data = new Dictionary<string, DataObject>
                    {
                        { "Map", new DataObject(DataObject.VisibilityOptions.Public, "Apple valley") }
                    }
                }
            );

            Debug.Log($"Лобби создано! ID: {lobby.Id}");
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"Ошибка создания лобби: {e.Message}");
        }
    }
}
