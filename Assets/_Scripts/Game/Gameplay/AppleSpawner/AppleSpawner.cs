using System.Collections.Generic;
using Harvesting.Utility.Spawner;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

public class AppleSpawner : IService
{
    private readonly AppleSpawnerModel _model;
    
    private ObjectPool<AppleHandler> _simpleApplePool;
    private ObjectPool<AppleHandler> _zapApplePool;

    private int _treeStep;
    private int _totalSteps;
    
    public List<AppleHandler> simpleAppleList => _simpleApplePool.list;
    public List<AppleHandler> zapAppleList => _zapApplePool.list;

    public AppleSpawner(AppleSpawnerModel model, ISpawner<AppleHandler> spawner)
    {
        _model = model;
        
        PoolInit(spawner);
        TreeInit();

        ServiceManager.Instance.LocalServices.Registarion(this);
    }
    
    public void Spawn()
    {
        for (int i = 0; i < _totalSteps; i++)
        {
            AppleTree tree = PeekRandomTree(i);

            if (Random.Range(0f, 1f) < _model.zapAppleSpawnChance)
                tree.PlaceApple(_zapApplePool);
            else
                tree.PlaceApple(_simpleApplePool);
        }
    }

    public void ReturnToPool(AppleHandler appleHandler)
    {
        switch (appleHandler.appleController.type)
        {
            case AppleType.SimpleApple:
                _simpleApplePool.Enqueue(appleHandler);
                break;
            case AppleType.ZapApple:
                _zapApplePool.Enqueue(appleHandler);
                break;
        }
    }
    
    private void PoolInit(ISpawner<AppleHandler> spawner)
    {
        _simpleApplePool = new(
            _model.maxApplesOnLevel,
            _model.simpleApplePrefab,
            _model.parentForSimpleApples,
            spawner
            );
        
        _zapApplePool = new(
            (int)(
            _model.maxApplesOnLevel * _model.zapAppleSpawnChance), 
            _model.zapApplePrefab,
            _model.parentForZapApples,
            spawner
            );

        _simpleApplePool.Generate();
        _zapApplePool.Generate();
    }
    private void TreeInit()
    {
        _treeStep = Mathf.RoundToInt(1 / _model.percentageTreesSpawn);
        _totalSteps = Mathf.RoundToInt(_model.appleTrees.Count * _model.percentageTreesSpawn);
        
        Debug.Log($"steps: {_treeStep}/{_totalSteps}");
    }

    private AppleTree PeekRandomTree(int currentStep)
    {
        int start = currentStep * _treeStep;
        int end = currentStep == _totalSteps - 1 ? _model.appleTrees.Count : start + (_treeStep - 1);

        int randomIndex = Random.Range(start, end);

        return _model.appleTrees[randomIndex];
    }
}
