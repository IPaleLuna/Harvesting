using UnityEngine;
using UnityEngine.Events;

namespace Harvesting.Collectable.Apple
{
    public class Apple
    {
        private readonly AppleProperties[] _appleProperties;
        private readonly GameObject[] _appleStateObj;
        
        private TickCounter _tickCounter = new();
        
        private AppleProperties _currentProperties;
        
        private readonly IAppleHandler _appleHandler;
        
        private readonly GameObject _gameObject;
        private readonly Transform _transform;

        public AppleType type => _currentProperties.appleType;
        public int cost => _currentProperties.cost;
        
        public TickCounter tickCounter => _tickCounter;

        public Apple(AppleProperties[] appleProperties, GameObject[] appleStateObj, MonoBehaviour ctx)
        {
            _appleProperties = appleProperties;
            _appleStateObj = appleStateObj;
            
            _gameObject = ctx.gameObject;
            _transform = ctx.transform;
            
            _appleHandler = ctx as IAppleHandler;
            
            _currentProperties = _appleProperties[0];
        }

        public void SetUpTickHolder(UnityAction onTick)
        {
            _tickCounter = new();
            
            _tickCounter.SetUp(onTick);
            _tickCounter.Start();
            _tickCounter.SetTarget(_currentProperties.ticksToNextState);
        }
        
        public void ChangeState()
        {
            if (_currentProperties.state == AppleState.Rotten)
            {
                _appleHandler.HideApple();
                return;
            }

            _appleHandler.ChangeAppleState((int)_currentProperties.state + 1);
        }
        
        public void SetState(int stateNum)
        {
            _currentProperties = _appleProperties[stateNum];

            _tickCounter.SetTarget(_currentProperties.ticksToNextState);

            for (int i = 0; i < _appleStateObj.Length; i++)
                _appleStateObj[i].SetActive(i == stateNum);
        }
        
        public void Respawn(Vector3 pos)
        {
            SetState(0);
            
            _transform.position = pos;
            _gameObject.SetActive(true);
            
            _tickCounter?.Start();
        }
        
        public void Hide()
        {
            _tickCounter?.Pause();
            _gameObject.SetActive(false);
        }
        
        ~Apple()
        {
            _tickCounter.ShutDown();
            _tickCounter = null;
        }
    }
}


