using Harvesting.Collectable.Apple;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static readonly UnityEvent<PlayerController> playerPickApple = new();
    public static readonly UnityEvent<MonoAppleController> appleWasDeactivated = new();
    
    public static readonly UnityEvent<bool> gameOnPauseEvent = new();
    
    public static readonly UnityEvent exitSessionEvent = new();

    public static readonly UnityEvent timeOutEvent = new();
}
