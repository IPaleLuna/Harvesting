using UnityEngine;

public class PlayerModel
{
    private int _score = 0;
    private float _speed = 0F;
    public short playerID = -1;

    public int score => _score;

    public float speed
    {
        get => _speed;
        set => _speed = value >= 0 ? value : 0;
    }
    
    
    public void AddApples(int count) =>
        _score = Mathf.Max(0, _score + count);
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
        return new PlayerInfo(playerModel.score, playerModel.speed, playerModel.playerID);
    }
}
