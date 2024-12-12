using UnityEngine;

public interface IPlayerView
{
    public void UpdateScore(int score);
    public void UpdateDirection(Vector2 direction);
    public void ResetAnimations();
    
}
