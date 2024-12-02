using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Harvesting.UI.LobbySettings
{
    public class LobbySettingsView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _lobbyNameInputField;
        [SerializeField]
        private Toggle _isPublicToggle;
        [SerializeField]
        private Toggle _isLanToggle;
        
        public readonly UnityEvent<string> OnLobbyNameChanged = new();
        public readonly UnityEvent<bool> OnIsLanChanged = new();
        public readonly UnityEvent<bool> OnPublicChanged = new();

        private void Start()
        {
            _lobbyNameInputField.onValueChanged.AddListener((string value) => OnLobbyNameChanged.Invoke(value));
            _isPublicToggle.onValueChanged.AddListener((bool value) => OnPublicChanged.Invoke(value));
            _isLanToggle.onValueChanged.AddListener((bool value) => OnIsLanChanged.Invoke(value));
        }
    }
}
