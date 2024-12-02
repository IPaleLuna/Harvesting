using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Harvesting.UI.LobbySettings
{
    [RequireComponent(typeof(LobbySettingsView))]
    public class LobbySettings : MonoBehaviour
    {
        [SerializeField]
        private LobbySettingsView _view;
        private LobbySettingsModel _model;

        private void Start()
        {
            _view.OnLobbyNameChanged.AddListener(OnNameChanged);
            _view.OnPublicChanged.AddListener(OnIsPublicChanged);
            _view.OnIsLanChanged.AddListener(OnIsLanChanged);
        }

        private void OnNameChanged(string newName) => _model.lobbyName = newName;
        private void OnIsLanChanged(bool isLan) => _model.isLan = isLan;
        private void OnIsPublicChanged(bool isPublic) => _model.isPublic = isPublic;
        
        #region CreateGame
        
        public void CreateLobby()
        {
            if (!_model.isLan)
            {
                SettingUTPForLAN();
                _ = CreateLobbyAsync();
            }
            CreateHost();
        }

        private async UniTaskVoid CreateLobbyAsync()
        {
            try
            {
                // Параметры лобби
                var lobby = await LobbyService.Instance.CreateLobbyAsync(
                    _model.lobbyName,
                    LobbySettingsModel.MAX_PLAYERS,
                    new CreateLobbyOptions
                    {
                        IsPrivate = _model.isPublic, // Публичное лобби
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
            utp.SetConnectionData("0.0.0.0", LobbySettingsModel.PORT);
            Debug.Log($"UnityTransport настроен на порт {LobbySettingsModel.PORT}");
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

        #endregion

        #region StartClient

        public void ConnectToServerByIP() //TODO
        {
            if(!TryGetUnityTransport(out UnityTransport utp)) return;
            //utp.SetConnectionData(ClearIPAddress(ipAddress), LobbySettingsModel.PORT);
        }
        #endregion

        private bool TryGetUnityTransport(out UnityTransport transport)
        {
            transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
            
            if(transport == null)
                Debug.LogError("UnityTransport не найден на NetworkManager!");
            
            return transport != null;
        }

        private string ClearIPAddress(string dirtyIp) => Regex.Replace(dirtyIp, "[^A-Za-z0-9.]", "");
    }
}
