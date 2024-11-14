using UnityEngine;


namespace PaleLuna.Tab
{
    public class SequentialTab : ScreenTab
    {
        [SerializeField] private SequentialTab previousSequentialTab;
        
        public override void TabOpen()
        {
            base.TabOpen();
            m_uiInputListener.OnInputCancel.AddListener(OnCancel);
        }

        public override void TabClose()
        {
            base.TabClose();
            m_uiInputListener.OnInputCancel.RemoveListener(OnCancel);
        }

        protected override void OnCancel()
        {
            if (previousSequentialTab)
                OpenOtherTab(previousSequentialTab);
        }
    }
}
