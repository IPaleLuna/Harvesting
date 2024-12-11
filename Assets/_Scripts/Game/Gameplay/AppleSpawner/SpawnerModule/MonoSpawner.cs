using UnityEngine;

namespace Harvesting.Utility.Spawner
{
    public class MonoSpawner<T> : ISpawner<T> where T : Component
    {
        public T SpawnObject(T prefab, Transform parent, bool isActive = false)
        {
            var obj = Object.Instantiate(prefab).GetComponent<T>();
            obj.gameObject.SetActive(isActive);

            return obj;
        }
    }
}


