using Unity.Netcode;
using UnityEngine;

namespace Harvesting.Utility.Spawner
{
    public class NetSpawner<T> : ISpawner<T> where T : Component
    {
        public T SpawnObject(T prefab, Transform parent, bool isActive = false)
        {
            var obj = Object.Instantiate(prefab, parent, true);
            obj.GetComponent<NetworkObject>().Spawn();
            
            obj.gameObject.SetActive(isActive);
            
            return obj;
        }
    }
}


