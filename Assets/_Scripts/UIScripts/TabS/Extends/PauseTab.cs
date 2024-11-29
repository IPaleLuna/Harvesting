namespace PaleLuna.Tab
{
    public class PauseTab : ScreenTab
    {
        public override void OnStart()
        {
            if(IsStarted) return;
            base.OnStart();
            m_uiInputListener.OnInputCancel.AddListener(OnCancel);
        }

        public override void OnCancel()
        {
            if (gameObject.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }

        public void ResumeGame()
        {
            GameEvents.gameOnPauseEvent.Invoke(false);
            TabClose();
        }

        private void PauseGame()
        {
            GameEvents.gameOnPauseEvent.Invoke(true);
            TabOpen();
        }

        public void ExitSession()
        {
            GameEvents.exitSessionEvent.Invoke();
        }
    }
}
