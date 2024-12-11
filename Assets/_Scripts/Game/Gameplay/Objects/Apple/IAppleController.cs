using UnityEngine;

namespace Harvesting.Collectable.Apple
{
    public interface IAppleController
    {
        public AppleType type { get; }
        public int cost { get; }
        
        public void RespawnApple(Vector2 pos);
        public void HideApple();
        public void ChangeAppleState(int appleState);
    }
}


