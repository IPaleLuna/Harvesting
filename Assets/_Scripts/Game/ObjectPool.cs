using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : Component
{
    private Queue<T> _objectPool;
    private T _prefab;

    private int _capacity;
    private Transform _poolParent;

    public int count => _objectPool.Count;
    public T prefab => _prefab;
    
    public List<T> list => _objectPool.ToList();

    public ObjectPool(int startCapacity, T prefab, Transform poolParent)
    {
        _capacity = startCapacity;
        _poolParent = poolParent;
        _prefab = prefab;

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
        if (_objectPool.Count == 0) return default;
        return _objectPool.Dequeue();
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
            T obj = CreateItemAndPop(isActive);
            
            if(_poolParent) obj.transform.SetParent(_poolParent);

            Enqueue(obj);
        }
    }
    public T CreateItemAndPop(bool isActive = false)
    {
        T obj = Object.Instantiate(_prefab).GetComponent<T>();
        obj.gameObject.SetActive(isActive);

        return obj;
    }
}
