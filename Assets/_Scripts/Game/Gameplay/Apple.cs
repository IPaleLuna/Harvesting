using NaughtyAttributes;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [Header("Apple properties"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private AppleProperties[] _appleProperties;
    [SerializeField]
    private GameObject[] _appleStateObj;

    [SerializeField]
    private AppleProperties _currentProperties;

    [Header("TickHolde"), HorizontalLine(color: EColor.Gray)]
    [SerializeField]
    private TickCounter _tickCounter = new();

    public AppleType type => _currentProperties.appleType;
    public int cost => _currentProperties.cost;

    private void Start()
    { 
        _currentProperties = _appleProperties[0];

        _tickCounter.SetUp(OnTimeToChangeState);
        _tickCounter.OnResume();

        _tickCounter.SetTarget(_currentProperties.ticksToNextState);
    }

    public void DeactivateThis()
    {
        gameObject.SetActive(false);
        _tickCounter.OnPause();
        GameEvents.appleWasDeactivated.Invoke(this);
    }

    public void RespawnThis(Vector2 pos)
    {
        ChangeState(0);
        transform.position = pos;
        gameObject.SetActive(true);
        _tickCounter.OnResume();
    }

    private void OnTimeToChangeState()
    {
        if (_currentProperties.state == AppleState.Rotten)
        {
            DeactivateThis();
            return;
        }

        ChangeState((int)_currentProperties.state + 1);
    }
    private void ChangeState(int stateNum)
    {
        _currentProperties = _appleProperties[stateNum];

        _tickCounter.SetTarget(_currentProperties.ticksToNextState);

        for (int i = 0; i < _appleStateObj.Length; i++)
            _appleStateObj[i].SetActive(i == stateNum);
    }

    private void OnDestroy()
    {
        _tickCounter = null;
    }
}
