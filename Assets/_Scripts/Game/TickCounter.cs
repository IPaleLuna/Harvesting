using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable]
public class TickCounter : ITickUpdatable
{
    private GameLoops _gameLoops;

    [SerializeField, Range(0, 60)]
    private int _targetTicks;

    private int _currentTicks = 0;

    private UnityAction _callbackAction;

    public void SetCallback(UnityAction callback)
    {
        _callbackAction = callback;
    }

    public void EveryTickRun()
    {
        _currentTicks++;
        CheckCounter();
    }

    private void CheckCounter()
    {
        if (_currentTicks != _targetTicks) return;
        
        ResetCounter();
        _callbackAction?.Invoke();
    }

    private void ResetCounter()
    {
        _currentTicks = 0;
    }
}
