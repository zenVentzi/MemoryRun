using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class GameButton : AnimatedButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            Go.SendMessageUpwards("OnGameButtonClick", this, SendMessageOptions.RequireReceiver);
        }

        public void SetText(string txt)
        {
            GetComponent<Text>().text = txt;
        }

        public string GetText()
        {
            return GetComponent<Text>().text;
        }
    }
}
