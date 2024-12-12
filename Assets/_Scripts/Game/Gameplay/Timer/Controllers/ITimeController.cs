using System;
using Harvesting.Game.GameTimer;

public interface ITimeController
{
    public void StartGameTimer();
    public void StartAfterGameTimer();
    
    public void Pause();
    public void Resume();
}
