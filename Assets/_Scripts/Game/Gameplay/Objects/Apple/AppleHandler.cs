using Harvesting.Collectable.Apple;
using Services;
using Unity.Netcode;
using UnityEngine;

public class AppleHandler : MonoBehaviour
{
    [SerializeField]
    private Component _appleControllerComponent;
    
    private IAppleController _appleController;

    private void Awake()
    {
        _appleController = _appleControllerComponent as IAppleController;
    }

    public IAppleController appleController => _appleController;
}
