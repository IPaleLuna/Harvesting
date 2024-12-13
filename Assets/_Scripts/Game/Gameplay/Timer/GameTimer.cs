using System;
using PaleLuna.Timers.Implementations;


namespace Harvesting.Game.GameTimer
{
    public class GameTimer
    {
        private const float TIMER_TICK_IN_SECONDS = 0.5F;
        
        private readonly int _gameTimeInSeconds;
        private readonly int _timeToNextScene;
    
        private AsyncTimer _timer;
        private readonly TickMachine _tickMachine;
        
        public Action onGameTimerFinished;
        public Action onAfterGameTimerFinished;
        public Action<TimeStruct> onTick;
        
        public float remainingTimeInSeconds => _timer.remainingTime;

        public GameTimer(int gameTimeInSeconds, int timeToNextScene)
        {
            _gameTimeInSeconds = gameTimeInSeconds;
            _timeToNextScene = timeToNextScene;
            
            _tickMachine = new TickMachine(TIMER_TICK_IN_SECONDS, OnTick);
        }
        
        public void StartGameTimer()
        {
            Break();
            
            _timer = new(_gameTimeInSeconds, OnGameTimeOut);

            _tickMachine.Start();
            _timer.Start();

            OnTick();
        }

        public void StartAfterGameTimer()
        {
            Break();
            
            _timer = new(_timeToNextScene, OnAfterGameTimerOut);
            
            _tickMachine.Start();
            _timer.Start();
            
            OnTick();
        }

        public void PauseGameTimer()
        {
            _timer.OnPause();
        }

        public void ResumeGameTimer()
        {
            _timer.OnResume();
        }

        private void Break()
        {
            _tickMachine.Stop();
            _timer?.Stop();
            _timer?.Reset();
        }
        
        private void OnTick() => onTick?.Invoke(new TimeStruct(remainingTimeInSeconds));
        private void OnGameTimeOut() => onGameTimerFinished?.Invoke();
        private void OnAfterGameTimerOut() => onAfterGameTimerFinished?.Invoke();
    }
    
    public struct TimeStruct
    {
        public string min { get; }
        public string sec { get; }

        public TimeStruct(float seconds)
        {
            min = NumToStringBuffer.GetIntToStringTimeHash((int)(seconds / 60));
            sec = NumToStringBuffer.GetIntToStringTimeHash((int)(seconds % 60));
        }

        public TimeStruct(string min, string sec)
        {
            this.min = min;
            this.sec = sec;
        }
    }
}




