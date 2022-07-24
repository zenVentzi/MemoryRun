using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class AnimatedButton : AbstractButton
    {
        [SerializeField]
        private bool selectable;

        private bool isLocked;

        protected bool IsLocked { get { return isLocked; } }
        public bool Selected { get; private set; }

        private void ToggleButtonAlpha(bool reduce)
        {
            var currentColor = SpriteRend.color;
            currentColor.a = (reduce ? currentColor.a / 1.8f : currentColor.a * 1.8f);
            SpriteRend.color = currentColor;
        }

        [UsedImplicitly]
        protected virtual void OnnMouseDown()
        {
            if (isLocked || !ButtonsActive) return;
            Tr.localScale = new Vector3(.95f, .85f, 1);
        }

        private bool ReleasedOverButton()
        {
            var scale = Tr.localScale;
            Tr.localScale = Vector3.one;
            var releasedOverButton = SortingLayerManager.IsTopmost(Go);
            Tr.localScale = scale;

            return releasedOverButton;
        }

        [UsedImplicitly]
        protected virtual void OnnMouseUp()
        {
            if (isLocked || !ButtonsActive) return;

            if (ReleasedOverButton())
            {
                if (selectable)
                {
                    if (Selected)
                    {
                        Tr.localScale = Vector3.one;                        
                    }

                    Selected = !Selected;
                }
                else if (!selectable)
                {
                    Tr.localScale = Vector3.one;
                }

                OnClick();
            }
            else
            {
                if (selectable)
                {
                    if (!Selected)
                    {
                        Tr.localScale = Vector3.one;                        
                    }
                }
                else
                {
                    Tr.localScale = Vector3.one;
                }
            }
        }

        public void Reset()
        {
            isLocked = false;
            Selected = false;
            Tr.localScale = Vector3.one;
            SpriteRend.color = Color.white;
        }

        public void Lock(bool toggleAlpha = true)
        {
            isLocked = true;

            if(toggleAlpha)
                ToggleButtonAlpha(true);
        }

        public void Unlock(bool toggleAlpha = true)
        {
            isLocked = false;

            if(toggleAlpha)
                ToggleButtonAlpha(false);
        }
    }
}
