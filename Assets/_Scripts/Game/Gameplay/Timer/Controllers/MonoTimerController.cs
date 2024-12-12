using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Harvesting.Game.GameTimer
{
    public class MonoTimerController : MonoBehaviour, ITimeController
    {
        [SerializeField]
        private int _gameTimeInSeconds;
        [SerializeField]
        private int _afterGameTimeInSeconds;
        
        private GameTimer _gameTimer;
    
        public bool isInit { get; private set; }= false;
        
        [SerializeField]
        private bool _autoRun = true;
    
    
        public void Awake()
        {
            _gameTimer = new GameTimer(_gameTimeInSeconds, _afterGameTimeInSeconds);
    
            _gameTimer.onGameTimerFinished += () => GlobalTimeEvents.onGameTimerFinished?.Invoke();
            _gameTimer.onAfterGameTimerFinished += () => GlobalTimeEvents.onAfterGameTimerFinished?.Invoke();
            _gameTimer.onTick += (time) => GlobalTimeEvents.onTick?.Invoke(time);
            
            isInit = true;
            
            if(_autoRun) StartGameTimer();
        }
    
        public void StartGameTimer()
        {
            _gameTimer.StartGameTimer(); 
        }
    
        public void StartAfterGameTimer()
        {
            _gameTimer.StartAfterGameTimer();
        }
    
        public void Pause()
        {
            _gameTimer.PauseGameTimer();
        }
    
        public void Resume()
        {
            _gameTimer.ResumeGameTimer();
        }
    }
}


