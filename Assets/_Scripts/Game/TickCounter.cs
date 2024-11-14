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

    [SerializeField, ReadOnly]
    private int _currentTicks = 0;

    private UnityAction _callbackAction;

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
        _gameLoops?.Unregistration(this);
    }

    public void Start()
    {
        _gameLoops?.Registration(this);
    }
    public void ShutDown()
    {
        Pause();
        _currentTicks = 0;
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
