using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public abstract class OrderButton : MenuButton
    {
        protected abstract bool Available { get; }

        [UsedImplicitly]
        private void Start()
        {
            StartCoroutine(ToggleAvailability());
        }

        private IEnumerator ToggleAvailability()
        {
            while (true)
            {
                if (!Available && !IsLocked)
                {
                    Lock();
                }
                else if(Available && IsLocked)
                {
                    Unlock();
                }

                yield return new WaitForSeconds(1.5f);
            }
        }

        protected virtual void Purchase()
        {
        }

        protected override void OnClick()
        {
            base.OnClick();
            Purchase();
        }
    }
}
