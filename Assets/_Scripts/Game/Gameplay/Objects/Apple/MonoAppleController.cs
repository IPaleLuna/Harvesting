using System;
using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;

namespace Harvesting.Collectable.Apple
{
    public class MonoAppleController : MonoBehaviour, IPausable, IAppleController
    {
        [Header("Apple properties"), HorizontalLine(color: EColor.Green)] [SerializeField]
        private AppleProperties[] _appleProperties;

        [SerializeField]
        private GameObject[] _appleStateObj;
        
        [SerializeField]
        private AppleProperties _currentProperties;
        
        public Action onAppleDeactivate { get; set; }

        
        private Apple _apple;

        public AppleType type => _apple.type;
        public int cost => _apple.cost;

        private void Awake()
        {
            _apple = new Apple(_appleProperties, _appleStateObj, this);
            
            _apple.SetUpTickHolder(OnTimeToChangeState);

            ServiceManager.Instance
                .GlobalServices.Get<GameLoops>()
                .pausablesHolder.Registration(this);
        }
        
        public void HideApple()
        {
            _apple.Hide();
            GameEvents.appleWasDeactivated.Invoke(this);
        }

        public void ChangeAppleState(int appleState)
        {
            _apple.SetState(appleState);
        }

        public void RespawnApple(Vector2 pos)
        {
            _apple.Respawn(pos);
        }

        private void OnTimeToChangeState()
        {
            _apple.ChangeState();
        }

        #region [ IPausable implementation ]

        public void OnPause()
        {
            _apple.tickCounter.Pause();
        }

        public void OnResume()
        {
            _apple.tickCounter.Start();
        }

        #endregion
        
        private void OnDestroy()
        {
            ServiceManager.Instance
                ?.GlobalServices?.Get<GameLoops>()
                .pausablesHolder?.Unregistration(this);
        }
    }
}
