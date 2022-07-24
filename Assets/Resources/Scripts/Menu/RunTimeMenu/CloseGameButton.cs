using Assets.Resources.Scripts.Games;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu.RunTimeMenu
{
    public abstract class CloseGameButton : RunTimeMenuButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            OnMenuDisappearEvent += GetBackToMainMenu;
        }

        private void GetBackToMainMenu()
        {
            if (!Game.GameInstance.GameOver)
                PlayedGames.InterruptGame();                

            Destroy(Game.GameInstance.Go);
            Destroy(Tr.root.gameObject);
        }

        public override void HandleEscapeKey()
        {
            OnMenuDisappearEvent += GetBackToMainMenu;
        }
    }
}
