using System.Collections.Generic;
using Harvesting.Collectable.Apple;
using PaleLuna.Architecture.Loops;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Harvesting.PlayerHandler
{
    public class NetWorkPlayerHandler : NetworkBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private NetworkPlayerView _networkPlayerView;
    
        [SerializeField]
        private GameObject _playerCamera;
        
        private PlayerHandler _playerHandler;
    
        private GameLoops _gameLoops;
        
        [SerializeReference]
        private List<GameObject> _objectsToDestroyIfNotOwner;

        private void OnValidate()
        {
            _playerController ??= GetComponent<PlayerController>();
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _networkPlayerView.SetNewSkin(NetworkManager.Singleton.ConnectedClients.Count-1);
            }
            
            if (!IsOwner)
            {
                _playerController.Remove();
                _objectsToDestroyIfNotOwner.ForEach(Destroy);
                Destroy(this);
                return;
            }
            
            _playerHandler = new(_playerController);
            _playerHandler.EnableControl();
        }
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out NetworkAppleController apple))
            {
                _playerController.AddScore(apple.cost);
                apple.HideApple();
            }
        }

        public override void OnDestroy()
        {
            _playerHandler?.OnDestroyThis();
        }
    }
}


