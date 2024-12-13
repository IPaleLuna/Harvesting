using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class AppleSpawnerModel : MonoBehaviour
{
    #region [inspector area]
    [Header("Apple prefabs"), HorizontalLine(color: EColor.Red)]
    [SerializeField]
    private AppleHandler _simpleApplePrefab;
    [FormerlySerializedAs("_zapApple")] [SerializeField]
    private AppleHandler _zapApplePrefab;

    [Header("Spawn properties"), HorizontalLine(color: EColor.Blue)]
    [SerializeField, Range(0f, 1f)]
    private float _percentageTreesSpawn = 0.5f;

    [SerializeField, Range(0f, 1f)]
    private float _zapAppleSpawnChance = 0.3F;

    [SerializeField, Range(0, 300)]
    private int _maxApplesOnLevel;

    [Space, SerializeField]
    private Transform _parentForSimpleApples;
    [Space, SerializeField]
    private Transform _parentForZapApples;
    
    [Header("List of tree"), HorizontalLine(color: EColor.Orange)]
    [SerializeReference]
    private List<AppleTree> _appleTrees;
    #endregion
    
    #region [ Getters zone ]
    public AppleHandler simpleApplePrefab => _simpleApplePrefab;
    public AppleHandler zapApplePrefab => _zapApplePrefab;
    
    public float percentageTreesSpawn => _percentageTreesSpawn;
    public float zapAppleSpawnChance => _zapAppleSpawnChance;
    
    public int maxApplesOnLevel => _maxApplesOnLevel;
    
    public Transform parentForSimpleApples => _parentForSimpleApples;
    public Transform parentForZapApples => _parentForZapApples;
    
    public List<AppleTree> appleTrees => _appleTrees;
    #endregion

    public void CollectTrees()
    {
        _appleTrees = new(GetComponentsInChildren<AppleTree>());
    }
}
