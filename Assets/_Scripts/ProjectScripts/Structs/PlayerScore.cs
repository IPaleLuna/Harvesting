[System.Serializable]
public struct PlayerScore
{
    public int id;
    public string player_name;
    public int score;

    public PlayerScore(string player_name, int score, int id = 0)
    {
        this.id = id;
        this.player_name = player_name;
        this.score = score;
    }
}
