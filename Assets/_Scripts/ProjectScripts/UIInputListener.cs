using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class UIInputListener : MonoBehaviour, IService, IStartable
{
    [Header("InputMap")]
    [SerializeField]
    private GameInputsClass _gameInputs;
    
    [Header("Events"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private UnityEvent _onInputSubmit = new();
    [SerializeField]
    private UnityEvent _onInputCancel = new();
    

    public UnityEvent OnInputSubmit => _onInputSubmit;
    public UnityEvent OnInputCancel => _onInputCancel;

    public bool IsStarted { get; private set; } = false;

    public void OnStart()
    {
        if (IsStarted) return;
        IsStarted = true;

        ServiceManager.Instance.LocalServices.Registarion(this);
    }

    private void CreateGameInputs()
    {
        _gameInputs = new GameInputsClass();
        _gameInputs.UI.Enable();
    }

    #region [ Enable/Disable ]

    private void OnEnable()
    {
        if(_gameInputs == null) CreateGameInputs();
        _gameInputs.UI.Submit.performed += OnSubmit;
        _gameInputs.UI.Cancel.performed += OnCancel;
    }

    private void OnDisable()
    {
        if(_gameInputs == null) return;
        _gameInputs.UI.Submit.performed -= OnSubmit;
        _gameInputs.UI.Cancel.performed -= OnCancel;
    }

    #endregion

    private void OnSubmit(InputAction.CallbackContext context)
    {
        _onInputSubmit.Invoke();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        _onInputCancel.Invoke();
    }

   
}
