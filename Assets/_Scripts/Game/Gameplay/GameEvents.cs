using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static readonly UnityEvent<Player> applePickEvent = new();
}
