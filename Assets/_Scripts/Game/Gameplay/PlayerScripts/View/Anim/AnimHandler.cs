using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    [SerializeField]
    private Component _animControllerComponent;
    
    private IAnimController _animController;
    
    public IAnimController animController => _animController;

    private void Awake()
    {
        _animController = _animControllerComponent as IAnimController;
    }
}
