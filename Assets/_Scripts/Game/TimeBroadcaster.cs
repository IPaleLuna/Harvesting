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
        ServiceManager.Instance
            .SceneLocator
            .Registarion(this);


        _tickMachine = new TickMachine(TIMER_TICK_IN_SECONDS, OnTimeTick);
        _tickMachine.Start();

        GameEvents.timeBroadcasterInitEvent.Invoke();
    }

    private void OnTimeTick()
    {
        print("Tick");

        string min = NumToStringBuffer.GetIntToStringHash((int)(_gameManager.timer.remainingTime / 60));
        string sec = NumToStringBuffer.GetIntToStringHash((int)(_gameManager.timer.remainingTime % 60));

        TimeStruct currentTime = new(min, sec);

        print(currentTime.min);

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
