using NaughtyAttributes;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PaleLuna.Tab
{
    public abstract class ScreenTab : MonoBehaviour, IStartable
    {
        [Header("Objects")] 
        [SerializeField]
        protected Button m_firstButton;
        
        [SerializeField]
        protected bool _activeOnStart = false;

        [Header("Events"), HorizontalLine(color: EColor.Green)] [SerializeField]
        protected UnityEvent _OnTabOpen;

        [SerializeField] protected UnityEvent _OnTabClose;

        protected UIInputListener m_uiInputListener;

        public bool IsStarted { get; private set; } = false;

        public virtual void OnStart()
        {
            if(IsStarted) return;
            IsStarted = true;
            
            m_uiInputListener = ServiceManager.Instance
                .LocalServices.Get<UIInputListener>();

            gameObject.SetActive(_activeOnStart);
            
        }

        public void OpenOtherTab(SequentialTab other)
        {
            TabClose();
            other.TabOpen();
        }

        public virtual void TabOpen()
        {
            gameObject?.SetActive(true);

            m_firstButton.Select();
            _OnTabOpen.Invoke();
        }

        public virtual void TabClose()
        {
            gameObject.SetActive(false);
            _OnTabClose.Invoke();
        }
        protected abstract void OnCancel();

        
    }
}
