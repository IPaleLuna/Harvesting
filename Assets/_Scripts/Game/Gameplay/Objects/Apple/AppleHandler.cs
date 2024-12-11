using Harvesting.Collectable.Apple;
using Services;
using UnityEngine;

public class AppleHandler : MonoBehaviour
{
    [SerializeField]
    private Component _appleControllerComponent;
    
    private IAppleController _appleController;

    private void Awake()
    {
        _appleController = _appleControllerComponent as IAppleController;
        _appleController.onAppleDeactivate += ReturnThis;
    }

    public IAppleController appleController => _appleController;

    private void ReturnThis()
    {
        ServiceManager.Instance
            .LocalServices.Get<AppleSpawner>()
            ?.ReturnToPool(this);
    }
}
