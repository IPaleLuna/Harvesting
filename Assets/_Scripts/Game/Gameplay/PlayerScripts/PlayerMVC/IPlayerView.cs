using UnityEngine;

public interface IPlayerView
{
    public void UpdateDirection(Vector2 direction);
    public void ResetAnimations();

    public void DestroySelf();

}
