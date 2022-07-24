using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.Games.Run;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu.RunTimeMenu
{
    public class RunTimeBackButton : CloseGameButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            OnMenuDisappearEvent += ActivateMenu;
            OnMenuDisappearEvent += AdsManager.ShowInterstitial;
        }

        private void ActivateMenu()
        {
            RunGame.IsPreview = false;
            GameObjectManager.ActivateMenu();

            if (Game.GameInstance.GameOver) return;
            GameAudioManager.Instance.Stop();
            MenuAudioManager.Instance.Play();
        }

        public override void HandleEscapeKey()
        {
            base.HandleEscapeKey();
            OnMenuDisappearEvent += ActivateMenu;
            OnMenuDisappearEvent += AdsManager.ShowInterstitial;
        }
    }
}
