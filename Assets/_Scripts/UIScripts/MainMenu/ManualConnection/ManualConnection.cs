using Harvesting.Networking;
using Harvesting.UI.ManualConnection.Model;
using Services;
using UnityEngine;

namespace Harvesting.UI.ManualConnection
{
    [RequireComponent(typeof(ManualConnectionView))]
    public class ManualConnection : MonoBehaviour
    {
        [SerializeField]
        private ManualConnectionView _view;
        
        private readonly ManualConnectionModel _model = new();

        private NetworkLobbyManager _networkLobbyManager;
        

        private void OnValidate()
        {
            _view ??= GetComponent<ManualConnectionView>();
        }

        private void Start()
        {
            _networkLobbyManager = ServiceManager.Instance
                .GlobalServices
                .Get<NetworkLobbyManager>();
            
            _view.OnIpChanged.AddListener(OnIpChanged);
            _view.OnPortChanged.AddListener(OnPortChanged);
            _view.OnLobbyCodeChanged.AddListener(OnCodeChanged);
        }

        private void OnIpChanged(string ip) => _model.ip = ip;
        private void OnPortChanged(string port) => _model.port = port;
        private void OnCodeChanged(string code) => _model.code = code;

        public void ConnectByIP()
        {
            _networkLobbyManager.JoinLANGame(ConnectionInfo.CreateConnectionInfo(_model));
        }

        public void ConnectByCode()
        {
            
        }
    }
}

public struct ConnectionInfo
{
    public string ip;
    public ushort port;

    public ConnectionInfo(string ip, ushort port)
    {
        this.ip = ip;
        this.port = port;
    }

    public static ConnectionInfo CreateConnectionInfo(ManualConnectionModel model)
    {
        return new ConnectionInfo(model.GetClearIP(), (ushort)model.GetPort());
    }
}

