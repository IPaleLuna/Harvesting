using UnityEngine;

namespace Harvesting.Utility.Spawner
{
    public interface ISpawner<T> where T : Component
    {
        public T SpawnObject(T prefab, Transform parent = null, bool isActive = false);
    }

}

