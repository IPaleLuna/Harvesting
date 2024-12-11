using UnityEngine;

public class PlayerModel
{
    private int _appleAmount = 0;
    private float _speed = 0F;
    public int playerID = -1;

    public int appleAmount => _appleAmount;

    public float speed
    {
        get => _speed;
        set => _speed = value >= 0 ? value : 0;
    }
    
    
    public void AddApples(int count) =>
        _appleAmount = Mathf.Max(0, _appleAmount + count);
}

public struct PlayerInfo
{
    public int appleAmount;
    public float speed;
    public int playerID;

    public PlayerInfo(int appleAmount, float speed, int playerID)
    {
        this.appleAmount = appleAmount;
        this.speed = speed;
        this.playerID = playerID;
    }

    public static PlayerInfo CreatePlayerInfo(PlayerModel playerModel)
    {
        return new PlayerInfo(playerModel.appleAmount, playerModel.speed, playerModel.playerID);
    }
}
