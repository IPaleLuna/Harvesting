using Harvesting.Collectable.Apple;
using UnityEngine;

public class AppleHandler : MonoBehaviour
{
    [SerializeField]
    private Component _appleController;
    
    public IAppleController appleController => _appleController as IAppleController;
}
