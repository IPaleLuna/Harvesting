using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

namespace Harvesting.Game.GameTimer
{
    public class TimeHandler : MonoBehaviour, IService, IStartable
    {
        [SerializeField]
        private Component _timerComponent;
    
        private ITimeController _timeController;
    
        public bool IsStarted { get; private set; }
    
        public ITimeController timeController => _timeController;
    
        public void OnStart()
        {
            if(IsStarted) return;
            IsStarted = true;
        
            ServiceManager.Instance.LocalServices.Registarion(this);
            _timeController = _timerComponent as ITimeController;
        }
    }
}


