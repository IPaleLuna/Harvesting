using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabScreen : MonoBehaviour
{
    [SerializeField]
    private Button _firstButton;

    [SerializeField]
    private TabScreen _previousTab;
    
    [Header("Events"), HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private UnityEvent _OnTabOpen;
    
    [SerializeField]
    private UnityEvent _OnTabClose;
    
    private UIInputListener _uiInputListener;
    
    public void Awake()
    {
        _uiInputListener = ServiceManager.Instance
            .LocalServices.Get<UIInputListener>();
    }
    
    public void OpenOtherTab(TabScreen other)
    {
        TabClose();
        other.TabOpen();
    }

    public void TabOpen()
    {
        gameObject.SetActive(true);

        _uiInputListener.OnInputCancel.AddListener(OnCancel);
        _firstButton.Select();
        _OnTabOpen.Invoke();
    }

    public void TabClose()
    {
        _uiInputListener.OnInputCancel.RemoveListener(OnCancel);
        gameObject.SetActive(false);
        _OnTabClose.Invoke();
    }

    private void OnCancel()
    {
        if(_previousTab)
            OpenOtherTab(_previousTab);
    }

    
}
