using NaughtyAttributes;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Tree properties")]
    [SerializeField, MinMaxSlider(1, 6)]
    private Vector2Int _maxApplesPerSpawn;

    private int _currentApples = 0;

    [Header("SpawnArea")]
    [SerializeField]
    private float _height;
    [SerializeField]
    private float _width;
    [SerializeField]
    private Vector3 _areaPos;

    public void PlaceApple(ObjectPool<Apple> applesPool)
    {
        int amountApple = Random.Range(_maxApplesPerSpawn.x, _maxApplesPerSpawn.y + 1);

        for (int i = 0; i < amountApple; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(0, _width), Random.Range(0, _height));

            randomPos = transform.TransformPoint(randomPos + _areaPos);

            applesPool.Pop().RespawnThis(randomPos);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + _areaPos, new Vector3(_width, _height));
    }
}
