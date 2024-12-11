using TMPro;
using UnityEngine;


namespace Harvesting.UI.ManualConnection
{
    public class ManualConnectionView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _ipInputField;
        [SerializeField] private TMP_InputField _portInputField;
        [SerializeField] private TMP_InputField _lobbyCodeInputField;

        private TMP_InputField.OnChangeEvent _onIpChanged;
        private TMP_InputField.OnChangeEvent _onPortChanged;
        private TMP_InputField.OnChangeEvent _onLobbyCodeChanged;

        public TMP_InputField.OnChangeEvent OnIpChanged => _onIpChanged;
        public TMP_InputField.OnChangeEvent OnPortChanged => _onPortChanged;
        public TMP_InputField.OnChangeEvent OnLobbyCodeChanged => _onLobbyCodeChanged;

        private void Start()
        {
            _onIpChanged = _ipInputField.onValueChanged;
            _onPortChanged = _portInputField.onValueChanged;
            _onLobbyCodeChanged = _lobbyCodeInputField.onValueChanged;
        }
    }
}
