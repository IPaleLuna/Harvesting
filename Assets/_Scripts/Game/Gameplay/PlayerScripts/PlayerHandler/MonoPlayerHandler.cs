using Harvesting.Collectable.Apple;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;

namespace Harvesting.PlayerHandler
{
    [RequireComponent(typeof(PlayerController))]
    public class MonoPlayerHandler : MonoBehaviour, IPausable
    {
        [SerializeField]
        private PlayerController _playerController;
    
        private PlayerHandler _playerHandler;
        private GameLoops _gameLoops;

        private void OnValidate()
        {
            _playerController ??= GetComponent<PlayerController>();
        }

        private void Awake()
        {
            _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
            _gameLoops.pausablesHolder.Registration(this);
            
            GameEvents.timeOutEvent.AddListener(_playerHandler.DisableControl);
        
            _playerHandler = new(_playerController);
        }
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out MonoAppleController apple))
            {
                _playerController.AddScore(apple.cost);
                apple.HideApple();
                GameEvents.playerPickApple.Invoke(_playerController);
            }
        }
        
        #region [ Pausable implementation ]
        public void OnPause()
        {
            _playerHandler.DisableControl();
        }
        public void OnResume()
        {
            _playerHandler.EnableControl();
        }
        #endregion
        private void OnDestroy()
        {
            _gameLoops.pausablesHolder.Unregistration(this);
        }
    }
}


