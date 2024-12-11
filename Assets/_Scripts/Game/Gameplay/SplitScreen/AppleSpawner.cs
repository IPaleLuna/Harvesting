using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using System.Collections.Generic;
using Harvesting.Collectable.Apple;
using PaleLuna.Architecture.Loops;
using Services;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AppleSpawner : MonoBehaviour, IStartable, IPausable
{
    #region [inspector area]
    [FormerlySerializedAs("simpleMonoAppleHandler")]
    [FormerlySerializedAs("simpleMonoApple")]
    [FormerlySerializedAs("_simpleApple")]
    [Header("Apple prefabs"), HorizontalLine(color: EColor.Red)]
    [SerializeField]
    private MonoAppleController simpleMonoAppleController;
    [FormerlySerializedAs("zapMonoAppleHandler")] [FormerlySerializedAs("zapMonoApple")] [FormerlySerializedAs("_zapApple")] [SerializeField]
    private MonoAppleController zapMonoAppleController;

    [Header("Spawn properties"), HorizontalLine(color: EColor.Blue)]

    [SerializeField, Range(0f, 1f)]
    private float _percentageTreesSpawn = 0.5f;

    [SerializeField, Range(0f, 1f)]
    private float _zapAppleSpawnChance = 0.3F;

    [SerializeField, Range(0, 300)]
    private int _maxApplesOnLevel;

    [Space, SerializeField, Required]
    private Transform _paretnForSimpleApples;
    [Space, SerializeField, Required]
    private Transform _paretnForZapApples;

    [Space, SerializeField]
    private TickCounter _tickHolder = new();

    [Header("List of tree"), HorizontalLine(color: EColor.Orange)]
    [SerializeReference]
    private List<AppleTree> _appleTrees;
    #endregion

    private ObjectPool<MonoAppleController> _simpeApplePool;
    private ObjectPool<MonoAppleController> _zapApplePool;

    private int _treeStep;
    private int _totalSteps;
    
    public bool IsStarted { get; private set; } = false;

    public void OnStart()
    {
        if (IsStarted) return;
        IsStarted = true;

        PoolInit();
        TreeInit();

        _tickHolder.SetUp(OnTimeToSpawn);

        GameEvents.appleWasDeactivated.AddListener(ReturnAppleInPool);

        OnTimeToSpawn();
        
        ServiceManager.Instance
            .GlobalServices.Get<GameLoops>()
            .pausablesHolder.Registration(this);
    }

    private void PoolInit()
    {
        _simpeApplePool = new(_maxApplesOnLevel, simpleMonoAppleController, _paretnForSimpleApples);
        _zapApplePool = new((int)(_maxApplesOnLevel * _zapAppleSpawnChance), zapMonoAppleController, _paretnForZapApples);

        _simpeApplePool.Generate();
        _zapApplePool.Generate();
    }
    private void TreeInit()
    {
        _appleTrees = new(GetComponentsInChildren<AppleTree>());

        _treeStep = Mathf.RoundToInt(1 / _percentageTreesSpawn);
        _totalSteps = Mathf.RoundToInt(_appleTrees.Count * _percentageTreesSpawn);
    }

    private void OnTimeToSpawn()
    {
        for (int i = 0; i < _totalSteps; i++)
        {
            AppleTree tree = PeekRandomTree(i);

            if (Random.Range(0f, 1f) < _zapAppleSpawnChance)
                tree.PlaceApple(_zapApplePool);
            else
                tree.PlaceApple(_simpeApplePool);
        }
    }

    private AppleTree PeekRandomTree(int currentStep)
    {
        int start = currentStep * _treeStep;
        int end = currentStep == _totalSteps - 1 ? _appleTrees.Count : start + (_treeStep - 1);

        int randomIndex = Random.Range(start, end);

        return _appleTrees[randomIndex];
    }

    private void ReturnAppleInPool(MonoAppleController monoAppleController)
    {
        switch (monoAppleController.type)
        {
            case AppleType.SimpleApple:
                _simpeApplePool.Enqueue(monoAppleController);
                break;
            case AppleType.ZapApple:
                _zapApplePool.Enqueue(monoAppleController);
                break;
        }
    }

    private void OnDestroy()
    {
        _tickHolder.ShutDown();
        ServiceManager.Instance
            ?.GlobalServices?.Get<GameLoops>()
            ?.pausablesHolder?.Unregistration(this);
        
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
