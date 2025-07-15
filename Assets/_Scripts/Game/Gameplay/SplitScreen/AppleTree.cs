using Harvesting.Collectable.Apple;
using NaughtyAttributes;
using PaleLuna.Randomizers;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Tree properties")]
    [SerializeField, MinMaxSlider(1, 6)]
    private Vector2Int _maxApplesPerSpawn;

    [Header("SpawnArea")]
    [SerializeField]
    private float _height;
    [SerializeField]
    private float _width;
    [SerializeField]
    private Vector3 _areaPos;

    [Header("CheckArea")]
    [SerializeField]
    private CheckSphere _checkSphere;

    public void PlaceApple(ObjectPool<AppleHandler> applesPool)
    {
        int amountApple = Random.Range(_maxApplesPerSpawn.x, _maxApplesPerSpawn.y + 1);

        for (int i = 0; i < amountApple; i++)
        {
            if (!applesPool.TryPop(out AppleHandler appleHandler)) return;

            Vector3 area = new Vector3(_width, _height);
            Vector3 randomPos = VectorRandomizer.RandomPoint(area, transform.TransformPoint(_areaPos), _checkSphere);;

            appleHandler.appleController.RespawnApple(randomPos);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = transform.TransformPoint(_areaPos);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + _areaPos, new Vector3(_width, _height));
        Gizmos.DrawWireSphere(center, 0.2F);
    }
}
