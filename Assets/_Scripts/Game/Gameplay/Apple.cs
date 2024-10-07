using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    private int _cost = 1;

    public int cost => _cost;

    public void PickThis()
    {
        gameObject.SetActive(false);
    }
    public void RespawnThis()
    {

    }
}
