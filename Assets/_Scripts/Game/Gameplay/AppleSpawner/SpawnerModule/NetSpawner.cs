using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Harvesting.Utility.Spawner
{
    public class NetSpawner<T> : ISpawner<T> where T : Component
    {
        public T SpawnObject(T prefab, Transform parent, bool isActive = false)
        {
            var obj = Object.Instantiate(prefab, parent, true);
            obj.GetComponent<NetworkObject>().Spawn();
            obj.GetComponent<NetworkObject>().DestroyWithScene = true;
            
            obj.gameObject.SetActive(isActive);
            
            return obj;
        }
    }
}


