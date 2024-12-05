using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Harvesting.UI.LobbyСreator;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Harvesting.Networking
{
    public class NetworkLobbyManager : IStartable, IService
    {
        private NetworkSceneManager _networkSceneManager;

        public bool IsStarted { get; private set; } = false;

        
        public void OnStart()
        {
            if(IsStarted) return;
            IsStarted = true;
            
            _networkSceneManager = ServiceManager.Instance
                .GlobalServices
                .Get<NetworkSceneManager>();
            
        }

        #region [ CreateGame ]

        public void CreateLobby(LobbyInfo lobbyInfo)
        {
            if (!lobbyInfo.isLan)
                _ = CreateLobbyAsync(lobbyInfo);
            else
                SettingUtpForLan();

            CreateHost();
            LoadLobbyScene(lobbyInfo);
        }

        private async UniTaskVoid CreateLobbyAsync(LobbyInfo lobbyInfo)
        {
            try
            {
                // Параметры лобби
                var lobby = await LobbyService.Instance.CreateLobbyAsync(
                    lobbyInfo.lobbyName,
                    LobbyСreatorModel.MAX_PLAYERS,
                    new CreateLobbyOptions
                    {
                        IsPrivate = lobbyInfo.isPrivate, // Публичное лобби
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

        private void CreateHost()
        {
            if (NetworkManager.Singleton.StartHost()) Debug.Log("Хост успешно запущен!");
            else Debug.LogError("Ошибка запуска хоста!");
        }

        private void LoadLobbyScene(LobbyInfo lobbyInfo)
        {
            _networkSceneManager.RegisterCallback();
            _networkSceneManager
                .CurrentSceneBaggage
                .AddBaggage(StringKeys.IS_LAN_KEY, new BoolBaggage(lobbyInfo.isLan));

            _networkSceneManager.SwitchScene("Lobby");
        }

        private void SettingUtpForLan()
        {
            if (TryGetUnityTransport(out UnityTransport utp))
            {
                Debug.LogError("UnityTransport не найден на NetworkManager!");
                return;
            }

            // Установка порта для сервера
            utp.SetConnectionData("0.0.0.0", LobbyСreatorModel.PORT);
            Debug.Log($"UnityTransport настроен на порт {LobbyСreatorModel.PORT}");
        }

        #endregion

        #region [ JoinGame ]

        #region [ ConnectLan ]

        public void JoinLANGame(ConnectionInfo cInfo)
        {
            if (!TryGetUnityTransport(out UnityTransport utp)) return;
            
            utp.SetConnectionData(cInfo.ip, cInfo.port);
                
            if(!NetworkManager.Singleton.StartClient())
                Debug.LogError("Failled to connect to server by IP!");
        }
        
        #endregion

        #region [ ConnectLobbyA ]
        public void JoinLobbyByCode(string lobbyCode)
        {
            _ = ConnectAsyncByCode(lobbyCode);
        }
        public void JoinLobbyById(string id)
        {
            _ = ConnectAsyncById(id);
        }
        
        private async UniTaskVoid ConnectAsyncById(string id)
        {
            try
            {
                await Lobbies.Instance.JoinLobbyByIdAsync(id);
            }
            catch (LobbyServiceException e)
            {
                Debug.LogException(e);
            }
        }
        private async UniTaskVoid ConnectAsyncByCode(string code)
        {
            try
            {
                await Lobbies.Instance.JoinLobbyByCodeAsync(code);
            }
            catch (LobbyServiceException e)
            {
                Debug.LogException(e);
            }
        }
        #endregion
        
        #endregion

        private bool TryGetUnityTransport(out UnityTransport transport)
        {
            transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;

            if (transport == null)
                Debug.LogError("UnityTransport не найден на NetworkManager!");

            return transport != null;
        }
    }
}
