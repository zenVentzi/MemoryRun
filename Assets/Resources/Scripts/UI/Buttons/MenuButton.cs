using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class MenuButton : AnimatedButton
    {
        private bool releasedOnScroll,
                     buttonDown;

        [UsedImplicitly]
        private void ReleaseOnScroll()
        {
            if (!buttonDown)
            {
                return;
            }

            buttonDown = false;
            releasedOnScroll = true;
            Tr.localScale = Vector3.one;
        }

        protected override void OnnMouseDown()
        {
            base.OnnMouseDown();
            buttonDown = true;
        }

        protected override void OnnMouseUp()
        {
            if (releasedOnScroll)
            {
                releasedOnScroll = false;
                return;
            }

            base.OnnMouseUp();
            buttonDown = false;
        }
    }
}
