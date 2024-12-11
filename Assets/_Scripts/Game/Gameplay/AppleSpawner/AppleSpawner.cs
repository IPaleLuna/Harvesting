using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using System.Collections.Generic;
using Harvesting.Collectable.Apple;
using PaleLuna.Architecture.Loops;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AppleSpawner : IService
{
    private readonly AppleSpawnerModel _model;
    
    private ObjectPool<AppleHandler> _simpleApplePool;
    private ObjectPool<AppleHandler> _zapApplePool;

    private int _treeStep;
    private int _totalSteps;
    
    public bool IsStarted { get; private set; } = false;

    public AppleSpawner(AppleSpawnerModel model)
    {
        _model = model;
        
        PoolInit();
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
    
    private void PoolInit()
    {
        _simpleApplePool = new(
            _model.maxApplesOnLevel,
            _model.simpleApplePrefab,
            _model.parentForSimpleApples
            );
        
        _zapApplePool = new(
            (int)(_model.maxApplesOnLevel * _model.zapAppleSpawnChance), 
            _model.zapApplePrefab, 
            _model.parentForZapApples
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
