using System;
using System.Collections.Generic;
using System.Linq;
using Harvesting.Utility.Spawner;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : Component
{
    private readonly Queue<T> _objectPool;
    private readonly T _prefab;

    private readonly int _capacity;
    private readonly Transform _poolParent;
    
    private readonly ISpawner<T> _spawner;

    public int count => _objectPool.Count;
    public T prefab => _prefab;
    
    public List<T> list => _objectPool.ToList();

    public ObjectPool(int startCapacity, T prefab, Transform poolParent, ISpawner<T> spawner)
    {
        _capacity = startCapacity;
        _poolParent = poolParent;
        _prefab = prefab;
        
        _spawner = spawner;

        _objectPool = new Queue<T>(_capacity);
        this._poolParent = poolParent;
    }

    public ObjectPool(int startCapacity, T prefab)
    {
        _capacity = startCapacity;
        _prefab = prefab;

        _objectPool = new Queue<T>(_capacity);
    }

    public void Enqueue(T item)
    {
        try
        {
            _objectPool.Enqueue(item);
        }
        catch(NullReferenceException ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public T Pop()
    {
        return _objectPool.Count == 0 ? default : _objectPool.Dequeue();
    }
    public bool TryPop(out T item)
    {
        bool isNotEmpty = _objectPool.Count > 0;

        if (isNotEmpty) item = _objectPool.Dequeue();
        else item = default;

        return isNotEmpty;
    }

    public void Generate(int count = 0, bool isActive = false)
    {
        int end = count == 0 ? count = _capacity - _objectPool.Count : count;

        for (int i = 0; i < count; i++)
        {
            var obj = CreateItemAndPop(isActive);
            
            if(_poolParent) obj.transform.SetParent(_poolParent);

            Enqueue(obj);
        }
    }

    private T CreateItemAndPop(bool isActive = false)
    {
        return _spawner.SpawnObject(_prefab, _poolParent, isActive);
    }
}
