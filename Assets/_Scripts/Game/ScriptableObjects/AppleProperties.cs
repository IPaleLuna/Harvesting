using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "AppleProperties", menuName = "Game/objectProperties/Apple")]
public class AppleProperties : ScriptableObject
{
    [Header("Properties"), HorizontalLine(color: EColor.Yellow)]
    [SerializeField]
    private AppleType _type;
    [SerializeField]
    private AppleState _state;
    [SerializeField]
    private int _cost;

    [Header("Timings"), HorizontalLine(color: EColor.Red)]
    [SerializeField, MinMaxSlider(0, 30)]
    private Vector2Int _ticksToNextState;

    public AppleType appleType => _type;
    public AppleState state => _state;
    public int cost => _cost;

    public int ticksToNextState =>
            Random.Range(_ticksToNextState.x, _ticksToNextState.y);


    public void Copy(AppleProperties other)
    {
        _type = other._type;
        _state = other._state;
        _cost = other._cost;

        _ticksToNextState = other._ticksToNextState;
    }
}
