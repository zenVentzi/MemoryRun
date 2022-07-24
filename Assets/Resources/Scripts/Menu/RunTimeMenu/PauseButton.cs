using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu.RunTimeMenu
{
    public class PauseButton : AnimatedButton, IHandleEscapeKey
    {
        private bool gamePaused;

        public bool GamePaused
        {
            get { return gamePaused; }

            set
            {
                Game.GameInstance.IsGameRunning = !value;
                gamePaused = value;
            }
        }

        public static PauseButton Instance { get; private set; }

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
        }

        protected override void OnClick()
        {
            base.OnClick();
            Pause();
        }

        private void Pause()
        {
            if (Game.GameInstance == null || Game.GameInstance.GameOver || GamePaused) return;

            GamePaused = true;
            //StartCoroutine(KeepCollidersDeactivated());
            KeepCollidersDeactivated();
            Application.LoadLevelAdditive("RunTimeMenuScene");
        }

        private void KeepCollidersDeactivated()
        {
            CollidersManager.EnableGameColliders(enabled: false);

            //while (GamePaused)
            //{
            //    if (CollidersManager.AreCollidersActivated(Game.GameInstance.Go))
            //    {
            //        CollidersManager.EnableGameColliders(enabled: false);
            //    }

            //    yield return null;
            //}
        }

        public void HandleEscapeKey()
        {
            Pause();
        }
    }
}