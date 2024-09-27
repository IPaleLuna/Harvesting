using UnityEngine;
using UnityEngine.InputSystem;

public class DebugJoin : MonoBehaviour
{
    public void Onjoin(PlayerInput player)
    {
        print($"Join: {player.playerIndex}");
    }
}
