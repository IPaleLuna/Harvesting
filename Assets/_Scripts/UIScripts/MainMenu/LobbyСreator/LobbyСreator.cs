using Harvesting.Networking;
using Services;
using UnityEngine;

namespace Harvesting.UI.LobbyСreator
{
    [RequireComponent(typeof(LobbyСreatorView))]
    public class LobbyСreator : MonoBehaviour
    {
        [SerializeField]
        private LobbyСreatorView _view;
        private readonly LobbyСreatorModel _model = new();
        
        private NetworkLobbyManager _networkLobbyManager;
        
        private void OnValidate()
        {
            _view ??= GetComponent<LobbyСreatorView>();
        }

        private void Start()
        {
            _networkLobbyManager = ServiceManager.Instance
                .GlobalServices
                .Get<NetworkLobbyManager>();
            
            _view.OnLobbyNameChanged.AddListener(OnNameChanged);
            _view.OnPrivateChanged.AddListener(OnIsPrivateChanged);
            _view.OnIsLanChanged.AddListener(OnIsLanChanged);
        }

        private void OnNameChanged(string newName) => _model.lobbyName = newName;
        private void OnIsLanChanged(bool isLan) => _model.isLan = isLan;
        private void OnIsPrivateChanged(bool isPrivate) => _model.isPrivate = isPrivate;
        
        public void CreateLobby()
        {
            _networkLobbyManager.CreateLobby(LobbyInfo.CreateLobbyInfo(_model));
        }
    }

    public struct LobbyInfo
    {
        public string lobbyName;
        public bool isPrivate;
        public bool isLan;

        public LobbyInfo(string lobbyName, bool isPrivate, bool isLan)
        {
            this.lobbyName = lobbyName;
            this.isPrivate = isPrivate;
            this.isLan = isLan;
        }

        public static LobbyInfo CreateLobbyInfo(LobbyСreatorModel model)
        {
            return new LobbyInfo(model.lobbyName, model.isPrivate, model.isLan);
        }
    }
}
