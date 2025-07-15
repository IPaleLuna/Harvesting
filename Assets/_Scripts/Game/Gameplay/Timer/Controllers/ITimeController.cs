namespace Harvesting.Game.GameTimer
{
    public interface ITimeController
    {
        public bool isInit { get; }
    
        public void StartGameTimer();
        public void StartAfterGameTimer();
    
        public void Pause();
        public void Resume();
    }
}


