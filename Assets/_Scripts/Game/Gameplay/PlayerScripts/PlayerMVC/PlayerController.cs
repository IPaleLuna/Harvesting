using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [Header("Auto filling components"), HorizontalLine(color: EColor.Gray)]
    [SerializeField]
    private PlayerMovement _movement;

    [Header("Characteristics"), HorizontalLine(color: EColor.Violet)]
    [SerializeField]
    private PlayerCharacteristics _characteristics;
    
    private readonly PlayerModel _model = new();
    private PlayerView _view;
    
    public PlayerInfo playerInfo => PlayerInfo.CreatePlayerInfo(_model);

    private void OnValidate()
    {
        _movement ??= GetComponent<PlayerMovement>();
    }

    private void Awake()
    {
        _model.speed = _characteristics.speed;
        _model.playerID = _movement.playerInput.playerIndex;
        
        _movement.SetModel(_model);
    }

    public void Move()
    {
        _movement.Move();
    }

    public void CollectApple(Apple apple)
    {
        _model.AddApples(apple.cost);
    }

    public void IsActive(bool isActive)
    {
        if(isActive) _movement.Run();
        else _movement.Stop();
    }
}