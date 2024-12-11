using System;
using Harvesting.Utility.Spawner;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;

public class MonoAppleSpawnerHandler : MonoBehaviour, IPausable
{
    [SerializeField]
    private AppleSpawnerModel _model;
    
    [Space, SerializeField]
    private TickCounter _tickHolder = new();
    
    private AppleSpawner _spawner;

    private void Start()
    {
        _model.CollectTrees();
        _spawner = new AppleSpawner(_model, new MonoSpawner<AppleHandler>());
        
        _tickHolder.SetUp(OnTick);
        
        OnTick();
        
        ServiceManager.Instance
            .GlobalServices.Get<GameLoops>()
            .pausablesHolder.Registration(this);
    }

    private void OnTick()
    {
        _spawner.Spawn();
    }

    #region [ Pausable implementation ]

    public void OnPause()
    {
        _tickHolder.Pause();
    }

    public void OnResume()
    {
        _tickHolder.Start();
    }

    #endregion
}
