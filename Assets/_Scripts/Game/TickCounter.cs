using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TickCounter : ITickUpdatable
{
    private GameLoops _gameLoops;

    [SerializeField, Range(0, 60)]
    private int _targetTicks;
    
    private CounterState _counterState = CounterState.ShutDown;

    [SerializeField, ReadOnly]
    private int _currentTicks = 0;

    private UnityAction _callbackAction;
    
    public int currentTicks => _currentTicks;

    public void SetCallback(UnityAction callback)
    {
        _callbackAction = callback;
    }

    public void SetUp(UnityAction callback)
    {
        SetCallback(callback);
        _gameLoops = ServiceManager.Instance.GlobalServices.Get<GameLoops>();
    }

    public void SetTarget(int target)
    {
        _targetTicks = target;
        ResetCounter();
    }

    public void Pause()
    {
        if(_counterState == CounterState.Paused) return;
        
        _gameLoops?.Unregistration(this);
        _counterState = CounterState.Paused;
    }

    public void Start()
    {
        if (_counterState == CounterState.Runnign) return;
        
        _gameLoops?.Registration(this);
        _counterState = CounterState.Runnign;
    }
    public void ShutDown()
    {
        if (_counterState == CounterState.ShutDown) return;
        
        Pause();
        _currentTicks = 0;
        _counterState = CounterState.ShutDown;
    }

    public void EveryTickRun()
    {
        _currentTicks += 1;
        CheckCounter();
    }

    private void CheckCounter()
    {
        if (_currentTicks != _targetTicks) return;
        
        ResetCounter();
        _callbackAction?.Invoke();
    }

    private void ResetCounter() =>
        _currentTicks = 0;

    ~TickCounter()
    {
        ShutDown();
    }
}

public enum CounterState
{
    Runnign,
    Paused,
    ShutDown
}
