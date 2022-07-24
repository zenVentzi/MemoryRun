using System.Collections;
using Assets.Resources.Scripts.General.Managers;
using Assets.Resources.Scripts.Menu;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class MenuBackButton : MenuButton, IHandleEscapeKey
    {
        private IEnumerator GoBack()
        {
            yield return new WaitForEndOfFrame();
            Destroy(Tr.parent.gameObject);
            ScrollableMenu.ActivatePrevious();
            this.EnableMenuColliders();
        }

        protected override void OnClick()
        {
            base.OnClick();
            StartCoroutine(GoBack());
        }

        public void HandleEscapeKey()
        {
            if (SortingLayerManager.IsTopmost(Go, false))
                StartCoroutine(GoBack());
        }
    }
}