using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Visual
{
    public class MirrorGame : BrainGame
    {
        #region variables

        private Vector2 leftButtonPos,
                        rightButtonPos;

        private Vector2 defaultNumberScale;
        private GameObject numberGo;

        private GameButton mirrorBtn,
                           noMirrorBtn;

        [SerializeField] private Sprite[] images;

        private int mirrorInARow,
                    normalInARow,
                    lastImageIndex;

        #endregion

        #region methods

        private void SwapButtons()
        {
            var mirrorOnTheLeftSide = Random.Range(0, 2) == 1;

            if (mirrorOnTheLeftSide)
            {
                mirrorBtn.Tr.localPosition = leftButtonPos;
                noMirrorBtn.Tr.localPosition = rightButtonPos;
            }
            else
            {
                mirrorBtn.Tr.localPosition = rightButtonPos;
                noMirrorBtn.Tr.localPosition = leftButtonPos;
            }
        }

        protected override void Init()
        {
            base.Init();
            lastImageIndex = -1;
            numberGo = transform.FindChild("Number").gameObject;
            mirrorBtn = Tr.FindChild("Mirror").GetComponent<GameButton>();
            noMirrorBtn = Tr.FindChild("NoMirror").GetComponent<GameButton>();
            leftButtonPos = noMirrorBtn.Tr.localPosition;
            rightButtonPos = mirrorBtn.Tr.localPosition;
            defaultNumberScale = numberGo.transform.localScale;
        }

        private bool IsMirrorImage()
        {
            return numberGo.transform.localScale.x < 0;
        }

        protected virtual bool IsCorrect()
        {
            if (ClickedBtn == mirrorBtn && IsMirrorImage())
            {
                return true;
            }

            return ClickedBtn == noMirrorBtn && !IsMirrorImage();
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            if (IsCorrect())
            {
                ValidateCorrect();
            }
            else
            {
                ValidateIncorrect();
            }

            if(!GameOver)
                GenerateNew();
        }

        protected override void GenerateNew()
        {
            GenerateNumber();
            RotateNumber();
            SwapButtons();
        }

        private void RotateNumber()
        {
            bool mirror;
            var maxInARow = Random.Range(1, 4);

            do
            {
                mirror = Random.Range(0, 2) == 1;

                if (mirror)
                {
                    mirrorInARow++;
                    normalInARow = 0;
                }
                else
                {
                    normalInARow++;
                    mirrorInARow = 0;
                }
                
            } while (mirrorInARow > maxInARow || normalInARow > maxInARow);

            numberGo.transform.localScale = mirror
                ? new Vector3(defaultNumberScale.x, defaultNumberScale.y, 1)
                : new Vector3(-defaultNumberScale.x, defaultNumberScale.y, 1);
        }

        private void GenerateNumber()
        {
            int index;
            do
            {
                index = Random.Range(0, 8);
            } while (index == lastImageIndex);

            lastImageIndex = index;
            numberGo.GetComponent<SpriteRenderer>().sprite = images[index];
        }

        #endregion
    }
}