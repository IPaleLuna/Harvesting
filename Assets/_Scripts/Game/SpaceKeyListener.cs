using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceKeyListener : MonoBehaviour
{
    [SerializeField]
    private InputAction _spaceAction;

    public InputAction spaceAction => _spaceAction;

    private void OnEnable()
    {
        _spaceAction.Enable();
    }

    private void OnDisable()
    {
        _spaceAction.Disable();
    }
}