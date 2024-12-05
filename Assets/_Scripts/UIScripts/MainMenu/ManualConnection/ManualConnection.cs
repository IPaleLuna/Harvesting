using System;
using Harvesting.UI.ManualConnection.Model;
using UnityEngine;

namespace Harvesting.UI.ManualConnection
{
    [RequireComponent(typeof(ManualConnectionView))]
    public class ManualConnection : MonoBehaviour
    {
        [SerializeField]
        private ManualConnectionView _view;
        
        private readonly ManualConnectionModel _model = new();
        
        public ConnectionInfo ConnectionInfo => new ConnectionInfo(_model.GetClearIP(), _model.GetPort());

        private void OnValidate()
        {
            _view ??= GetComponent<ManualConnectionView>();
        }

        private void Start()
        {
            _view.OnIpChanged.AddListener(OnIpChanged);
            _view.OnPortChanged.AddListener(OnPortChanged);
        }

        private void OnIpChanged(string ip) => _model.ip = ip;
        private void OnPortChanged(string port) => _model.port = port;
    }

    public struct ConnectionInfo
    {
        public string ip;
        public int port;

        public ConnectionInfo(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
    }
}

