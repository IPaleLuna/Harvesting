using UnityEngine;

public class PlayerModel
{
    private int _appleAmount = 0;
    private float _speed = 0F;

    public int appleAmount => _appleAmount;

    public float speed
    {
        get => _speed;
        set => _speed = value >= 0 ? value : 0;
    }
    
    
    public void AddApples(int count) =>
        _appleAmount = Mathf.Max(0, _appleAmount + count);
}
