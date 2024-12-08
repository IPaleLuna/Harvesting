using System;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;

namespace Harvesting.PlayerHandler
{
    public class PlayerHandler : IPlayerHandler, IFixedUpdatable
    {
        private readonly PlayerController _playerController;
        private readonly GameLoops _gameLoops;
    
        public event Action OnFixedFrame;

        public PlayerHandler(PlayerController playerController)
        {
            _playerController = playerController;
        
            _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
            _gameLoops.Registration(this);
        
            GameEvents.timeOutEvent.AddListener(DisableControl);
        
            _playerController.SetUpMovement();
        }

        public void FixedFrameRun()
        {
            _playerController.Move();
        
            OnFixedFrame?.Invoke();
        }

        public void EnableControl()
        {
            _gameLoops.Registration(this);
            _playerController.IsActive(true);
        }
        public void DisableControl()
        {
            _gameLoops.Unregistration(this);
            _playerController.IsActive(false);
        }

        public void OnDestroyThis() //TODO
        {
        
        }

        ~PlayerHandler()
        {
            _gameLoops.Unregistration(this);
        }
    }
}
