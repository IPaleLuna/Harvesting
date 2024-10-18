using NaughtyAttributes;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [Header("Type of apple"), HorizontalLine(color: EColor.Red)]
    [SerializeField]
    private AppleType _type;

    [Header("Apple properties"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private int _cost = 1;

    public AppleType type => _type;
    public int cost => _cost;

    public void PickThis()
    {
        gameObject.SetActive(false);
        GameEvents.appleWasPicked.Invoke(this);
    }
    public void RespawnThis(Vector2 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
