using UnityEngine;

[CreateAssetMenu(fileName = "playerCharacteristics", menuName = "Configs/player")]
public class PlayerCharacteristics : ScriptableObject
{
    [SerializeField]
    private float _speed;

    public float speed => _speed;
}
