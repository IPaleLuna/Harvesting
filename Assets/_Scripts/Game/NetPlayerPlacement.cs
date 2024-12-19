using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Harvesting.PlayerHandler;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Network;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetPlayerPlacement : NetworkLunaBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints;

    private List<Transform> _availablePoints;
    
    private List<NetWorkPlayerHandler> _notReadyPlayers = new(4);

    [SerializeField]
    private bool _removeUsedPoints;

    public override void InitNetworkBehaviour()
    {
        //if (IsServer) NetworkManager.Singleton.SceneManager.OnLoadComplete += OnPlayerLoaded;
    }

    public void Awake()
    {
        _availablePoints = new List<Transform>(_spawnPoints);
    }

    public void Placement()
    {
        if (!IsServer) return;
        
        var connectedClients = NetworkManager.Singleton.ConnectedClientsList;
        
        foreach (var client in connectedClients)
        {
            NetWorkPlayerHandler playerHandler = client.PlayerObject.GetComponent<NetWorkPlayerHandler>();

            PlacePlayer(playerHandler);
        }
    }

    private void OnPlayerLoaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        if(!NetworkManager.Singleton.ConnectedClients.TryGetValue(clientId, out var client)) return;
        
        PlacePlayer(client.PlayerObject.GetComponent<NetWorkPlayerHandler>());
    }
    
    private void PlacePlayer(NetWorkPlayerHandler playerHandler)
    {
        Transform randomPoint = _availablePoints[UnityEngine.Random.Range(0, _availablePoints.Count)];
        playerHandler.SetPlayerPositionClientRpc(randomPoint.position, CreateClientRpcParams(playerHandler.OwnerClientId));
                
        if(_removeUsedPoints)
            _availablePoints.Remove(randomPoint);
    }

    private ClientRpcParams CreateClientRpcParams(ulong clientId)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId } // Указываем ID клиента
            }
        };
        
        return clientRpcParams;
    }
}
