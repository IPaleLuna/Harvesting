using PaleLuna.Architecture.Services;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace NetWorking
{
    public class NetworkSceneManager : IService
    {
        private SceneBaggage _currentSceneBaggage = new();
        private readonly SceneBaggage _baggageForNextScene = new();
        
        public SceneBaggage CurrentSceneBaggage => _currentSceneBaggage;
        public SceneBaggage BaggageForNextScene => _baggageForNextScene;
        
        private int _numberOfClientsLoaded = 0;
        
        public readonly UnityEvent OnSceneLoadedEvent = new();
        public readonly UnityEvent<ulong> OnClientLoadedEvent = new();
        

        public void SwitchScene(string sceneName)
        {
            if (NetworkManager.Singleton.IsListening)
            {
                _numberOfClientsLoaded = 0;
                SwapBaggage();
                
                NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                Debug.LogWarning("NetworkManager.Singleton.IsListening is false");
            }
        }

        public void ExitAndLoadMenu()
        {
            if (NetworkManager.Singleton != null && NetworkManager.Singleton.SceneManager != null)
            {
                NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnLoadComplite;
            }
            
            SceneManager.LoadScene(1);
        }
        
        public void RegisterCallback()
        {
            NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadComplite;
        }
        
        public bool IsAllClientsLoaded() => _numberOfClientsLoaded == NetworkManager.Singleton.ConnectedClients.Count;

        private void SwapBaggage()
        {
            _currentSceneBaggage = _baggageForNextScene;
        }
        
        private void OnLoadComplite(ulong clientid, string scenename, LoadSceneMode loadscenemode)
        {
            _numberOfClientsLoaded++;
            OnClientLoadedEvent.Invoke(clientid);
        }
        
        
    }
}

