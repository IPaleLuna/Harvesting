using Harvesting.Collectable.Apple;
using NaughtyAttributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Auto filling components"), HorizontalLine(color: EColor.Gray)]
    [SerializeField]
    private PlayerMovement _movement;

    [Header("Characteristics"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private PlayerCharacteristics _characteristics;
    
    private readonly PlayerModel _model = new();
    [Header("MVC components"), HorizontalLine(color: EColor.Violet)]
    [SerializeField, Required]
    private Component _playerViewComponent;
    
    private IPlayerView _view;
    
    public PlayerInfo playerInfo => PlayerInfo.CreatePlayerInfo(_model);

    private void OnValidate()
    {
        _movement ??= GetComponent<PlayerMovement>();
    }

    private void Awake()
    {
        _view = _playerViewComponent as IPlayerView;
        
        _model.speed = _characteristics.speed;
        _model.playerID = _movement.playerInput.playerIndex;
    }

    public void SetUpMovement() => _movement.Init();

    public void Move()
    {
        _movement.Move(_model.speed);
        
        if(_movement.isDirectionChanged)
            _view.UpdateDirection(_movement.currentDirection);
    }

    public void AddScore(int score)
    {
        _model.AddApples(score);
    }

    public void IsActive(bool isActive)
    {
        if(isActive) _movement.Run();
        else
        {
            _movement.Stop();
            _view.ResetAnimations();            
        }
    }

    public void Remove()
    {
        _movement.Remove();
        Destroy(this);
    }
}