using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Harvesting.UI.LobbyСreator
{
    public class LobbyСreatorView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _lobbyNameInputField;
        [FormerlySerializedAs("_isPublicToggle")] [SerializeField]
        private Toggle _isPrivateToggle;
        [SerializeField]
        private Toggle _isLanToggle;
        
        public readonly UnityEvent<string> OnLobbyNameChanged = new();
        public readonly UnityEvent<bool> OnIsLanChanged = new();
        public readonly UnityEvent<bool> OnPrivateChanged = new();

        private void Start()
        {
            _lobbyNameInputField.onValueChanged.AddListener((string value) => OnLobbyNameChanged.Invoke(value));
            _isPrivateToggle.onValueChanged.AddListener((bool value) => OnPrivateChanged.Invoke(value));
            _isLanToggle.onValueChanged.AddListener((bool value) => OnIsLanChanged.Invoke(value));
            _isLanToggle.onValueChanged.AddListener(OnLANToggled);
        }

        private void OnLANToggled(bool isOn)
        {
            if (isOn)
            {
                _isPrivateToggle.isOn = false;
                _isPrivateToggle.interactable = false;
            }
            else
                _isPrivateToggle.interactable = true;
        }
    }
}
