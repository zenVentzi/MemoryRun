using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Games.BrainZ.Logic
{
    public abstract class SizeDependentGame : BrainGame
    {
        private int size,
                    moves;
        private Text movesTxt,
                     sizeTxt;

        protected bool MoreClicked
        {
            get
            {
                return ClickedBtn != null && ClickedBtn.name == "SizeUp";
            }
        }

        protected bool LessClicked
        {
            get
            {
                return ClickedBtn != null && ClickedBtn.name == "SizeDown";
            }
        }

        protected virtual string SizeTxt
        {
            get { return sizeTxt.text; }
            set { sizeTxt.text = value; }
        }

        protected virtual int MaxSize
        { get { return 7; } }

        private int MinSize
        { get { return 3; } }

        public int Moves
        {
            get { return moves; }
            protected set
            {
                if (movesTxt == null)
                    movesTxt = GameObjectManager.GetGoInChildren(Go, "Moves").GetComponent<Text>();

                moves = value;
                movesTxt.text = moves.ToString();
            }
        }

        public int Size
        {
            get { return size; }
            protected set
            {
                if (sizeTxt == null)
                    sizeTxt = GameObjectManager.GetGoInChildren(Go, "Size").GetComponent<Text>();

                size = value;
                SizeTxt = size.ToString();
            }
        }

        protected void DisableModifyButtons()
        {
            GameObjectManager.GetGoInChildren(Go, "SizeUp").SetActive(false);
            GameObjectManager.GetGoInChildren(Go, "SizeDown").SetActive(false);
        }

        protected override void Init()
        {
            base.Init();
            Size = 3;
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            if (MoreClicked)
            {
                if (Size >= MaxSize) return;

                Size++;
                OnSizeIncrease();
            }
            else if (LessClicked)
            {
                if (Size <= MinSize) return;

                Size--;
                OnSizeDecrease();
            }
        }

        protected abstract void OnSizeIncrease();

        protected abstract void OnSizeDecrease();
    }
}
