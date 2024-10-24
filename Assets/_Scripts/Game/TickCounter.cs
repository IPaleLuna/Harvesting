using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TickCounter : ITickUpdatable, IPausable
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

        _gameLoops.pausablesHolder.Registration(this);
    }

    public void SetTarget(int target)
    {
        _targetTicks = target;
        ResetCounter();
    }

    public void OnPause()
    {
        _gameLoops?.Unregistration(this);
    }

    public void OnResume()
    {
        _gameLoops?.Registration(this);
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
        OnPause();
        _gameLoops.pausablesHolder.Unregistration(this);
    }
    
}
