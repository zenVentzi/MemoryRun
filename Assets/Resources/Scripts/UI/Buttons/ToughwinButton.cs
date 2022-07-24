using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class ToughwinButton : AnimatedButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            Application.OpenURL("https://www.facebook.com/toughwingames?ref=aymt_homepage_panel");
        }
    }
}
