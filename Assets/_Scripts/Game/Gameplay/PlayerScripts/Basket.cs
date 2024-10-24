using UnityEngine;

public class Basket
{
    private int _appleAmount = 0;

    public int appleAmount => _appleAmount;

    public void AddApples(int count) =>
        _appleAmount = Mathf.Max(0, _appleAmount + count);

}
