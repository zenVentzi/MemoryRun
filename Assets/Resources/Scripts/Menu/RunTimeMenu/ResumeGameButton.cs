namespace Assets.Resources.Scripts.Menu.RunTimeMenu
{
    public class ResumeGameButton : RunTimeMenuButton
    {
        private void Resume()
        {
            PauseButton.Instance.GamePaused = false;
            CollidersManager.EnableGameColliders();
            Destroy(transform.root.gameObject);
        }

        protected override void OnClick()
        {
            base.OnClick();
            OnMenuDisappearEvent += Resume;
        }
    }
}
