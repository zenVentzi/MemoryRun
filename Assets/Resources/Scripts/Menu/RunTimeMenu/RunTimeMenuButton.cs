using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;

namespace Assets.Resources.Scripts.Menu.RunTimeMenu
{
    public delegate void OnMenuDisappearEventHandler();
    public abstract class RunTimeMenuButton : AnimatedButton, IHandleEscapeKey
    {
        protected OnMenuDisappearEventHandler OnMenuDisappearEvent;
        protected override void OnClick()
        {
            base.OnClick();
            Tr.root.GetComponent<UnityEngine.Animation>().Play("RunTimeMenuDisappear");
        }

        [UsedImplicitly]
        private void OnMenuDisappear()
        {
            if (OnMenuDisappearEvent != null) OnMenuDisappearEvent();
        }

        public virtual void HandleEscapeKey()
        {
            Tr.root.GetComponent<UnityEngine.Animation>().Play("RunTimeMenuDisappear");
        }
    }
}
