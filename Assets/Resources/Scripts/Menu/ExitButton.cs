using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public class ExitButton : AnimatedButton, IHandleEscapeKey
    {
        protected override void OnClick()
        {
            base.OnClick();
            ExitApp();
        }

        private void ExitApp()
        {
            Application.Quit();
        }

        [UsedImplicitly]
        private void OnApplicationQuit()
        {
            //PlayGamesPlatform.Instance.SignOut();
            PlayerPrefs.Save();
        }

        public void HandleEscapeKey()
        {
            if (SortingLayerManager.IsTopmost(Go, false))
                ExitApp();
        }
    }
}