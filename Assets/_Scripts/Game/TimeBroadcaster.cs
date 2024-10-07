using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using PaleLuna.Timers.Implementations;
using Services;
using UnityEngine;
using UnityEngine.Events;

public class TimeBroadcaster : MonoBehaviour, IStartable, IService
{
    private const int TIMER_TICK_IN_SECONDS = 1;

    public readonly UnityEvent<TimeStruct> timeTickEvent = new();

    private GameManager _gameManager;
    private TickMachine _tickMachine;

    private bool _isStarted = false;
    public bool IsStarted => _isStarted;

    public void OnStart()
    {
        if (_isStarted) return;
        _isStarted = true;

        _gameManager = ServiceManager.Instance
            .SceneLocator
            .Get<GameManager>();

        _tickMachine = new TickMachine(TIMER_TICK_IN_SECONDS, OnTimeTick);
        _tickMachine.Start();
    }

    private void OnTimeTick()
    {
        string min = NumToStringBuffer.GetIntToStringTimeHash((int)(_gameManager.timer.remainingTime / 60));
        string sec = NumToStringBuffer.GetIntToStringTimeHash((int)(_gameManager.timer.remainingTime % 60));

        TimeStruct currentTime = new(min, sec);

        timeTickEvent.Invoke(currentTime);
    }
}


public struct TimeStruct
{
    private string _min;
    private string _sec;

    public string min => _min;
    public string sec => _sec;

    public TimeStruct(string min, string sec)
    {
        _min = min;
        _sec = sec;
    }
}
