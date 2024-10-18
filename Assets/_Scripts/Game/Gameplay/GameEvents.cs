using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static readonly UnityEvent<Player> playerPickApple = new();
    public static readonly UnityEvent<Apple> appleWasPicked = new();

    public static readonly UnityEvent timeOutEvent = new();
}
