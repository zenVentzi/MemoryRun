using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.General;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public abstract class AbstractButton : MyMono
    {
        //protected bool Clicked { private get; set; }

        protected static bool ButtonsActive = true;

        /*
        private static T Get<T>(string name, GameObject goToSearchIn) where T : AbstractButton
        {
            return goToSearchIn.GetComponentsInChildren<T>().First(button => button.Go.name == name);
        }
*/

        protected virtual void OnClick()
        {
            EffectsAudioManager.Instance.PlayClick();
        }

        public static void DeactivateButtons()
        {
            ButtonsActive = false;
        }

        public static void ActivateButtons()
        {
            ButtonsActive = true;
        }
    }
}