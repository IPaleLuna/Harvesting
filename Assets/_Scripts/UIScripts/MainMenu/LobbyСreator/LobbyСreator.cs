using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.Services;
using Services;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using NetworkSceneManager = NetWorking.NetworkSceneManager;

namespace Harvesting.UI.LobbyСreator
{
    [RequireComponent(typeof(LobbyСreatorView))]
    public class LobbyСreator : MonoBehaviour
    {
        [SerializeField]
        private LobbyСreatorView _view;
        private readonly LobbyСreatorModel _model = new();
        
        private NetworkSceneManager _networkSceneManager; 

        private void OnValidate()
        {
            _view ??= GetComponent<LobbyСreatorView>();
        }

        private void Start()
        {
            _view.OnLobbyNameChanged.AddListener(OnNameChanged);
            _view.OnPrivateChanged.AddListener(OnIsPrivateChanged);
            _view.OnIsLanChanged.AddListener(OnIsLanChanged);

            _networkSceneManager = ServiceManager.Instance
                .GlobalServices
                .Get<NetworkSceneManager>();
        }

        private void OnNameChanged(string newName) => _model.lobbyName = newName;
        private void OnIsLanChanged(bool isLan) => _model.isLan = isLan;
        private void OnIsPrivateChanged(bool isPrivate) => _model.isPrivate = isPrivate;
        
        #region CreateGame
        
        public void CreateLobby()
        {
            if (!_model.isLan)
                _ = CreateLobbyAsync();
            else
                SettingUTPForLAN();
            
            CreateHost();
            LoadLobbyScene();
        }

        private async UniTaskVoid CreateLobbyAsync()
        {
            try
            {
                // Параметры лобби
                var lobby = await LobbyService.Instance.CreateLobbyAsync(
                    _model.lobbyName,
                    LobbyСreatorModel.MAX_PLAYERS,
                    new CreateLobbyOptions
                    {
                        IsPrivate = _model.isPrivate, // Публичное лобби
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
        private void SettingUTPForLAN()
        {
            UnityTransport utp = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
            if (utp == null)
            {
                Debug.LogError("UnityTransport не найден на NetworkManager!");
                return;
            }

            // Установка порта для сервера
            utp.SetConnectionData("0.0.0.0", LobbyСreatorModel.PORT);
            Debug.Log($"UnityTransport настроен на порт {LobbyСreatorModel.PORT}");
        }
        private void CreateHost()
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Хост успешно запущен!");
            }
            else
            {
                Debug.LogError("Ошибка запуска хоста!");
            }
        }

        private void LoadLobbyScene()
        {
            _networkSceneManager.RegisterCallback();
            _networkSceneManager
                .CurrentSceneBaggage
                .AddBaggage(StringKeys.IS_LAN_KEY, new BoolBaggage(_model.isLan));
            
            _networkSceneManager.SwitchScene("Lobby");
        }

        #endregion

        private bool TryGetUnityTransport(out UnityTransport transport)
        {
            transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
            
            if(transport == null)
                Debug.LogError("UnityTransport не найден на NetworkManager!");
            
            return transport != null;
        }
    }
}
