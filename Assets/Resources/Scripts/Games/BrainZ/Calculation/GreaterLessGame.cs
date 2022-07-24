using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Games.BrainZ.Calculation
{
    public class GreaterLessGame : BrainGame
    {
        #region variables

        private int currentNumber;

        private Vector2 leftButtonPos,
                        rightButtonPos;

        private GameButton greater,
                           less;

        private GameObject numberGo;
        private int previousNumber,
                    greaterInARow,
                    equalInARow,
                    lessInARow;

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            numberGo = GameObjectManager.GetGoInChildren(Go, "Number");
            greater = Tr.FindChild("Greater").GetComponent<GameButton>();
            less = Tr.FindChild("Less").GetComponent<GameButton>();
            leftButtonPos = less.Tr.localPosition;
            rightButtonPos = greater.Tr.localPosition;
            GenerateNew();
        }

        private void SwapButtons()
        {
            var greaterBtnOnTheLeftSide = Random.Range(0, 2) == 1;

            if (greaterBtnOnTheLeftSide)
            {
                greater.Tr.localPosition = leftButtonPos;
                less.Tr.localPosition = rightButtonPos;
            }
            else
            {
                greater.Tr.localPosition = rightButtonPos;
                less.Tr.localPosition = leftButtonPos;
            }
        }

        private void ModifyNumber(int number)
        {
            numberGo.GetComponent<Text>().text = number.ToString();
        }

        private bool CheckIsGreater()
        {
            return currentNumber > previousNumber;
        }

        private bool CheckIsEqual()
        {
            return currentNumber == previousNumber;
        }

        protected virtual bool IsCorrect()
        {
            return (ClickedBtn.name == "Less" && !CheckIsGreater() && !CheckIsEqual()) ||
                   (ClickedBtn.name == "Greater" && CheckIsGreater()) ||
                   (ClickedBtn.name == "Equal" && CheckIsEqual());
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            GenerateNew();
        }

        protected override void ValidateIncorrect()
        {
            base.ValidateIncorrect();

            if(!GameOver)
                GenerateNew();
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
        }

        protected override void GenerateNew()
        {
            SwapButtons();
            previousNumber = currentNumber;

            do
            {
                currentNumber = Random.Range(previousNumber - 50, previousNumber + 50);

                if (currentNumber > previousNumber)
                {
                    lessInARow = 0;
                    equalInARow = 0;
                    greaterInARow++;
                }
                else if(currentNumber < previousNumber)
                {
                    greaterInARow = 0;
                    equalInARow = 0;
                    lessInARow++;
                }
                else
                {
                    greaterInARow = 0;
                    lessInARow = 0;
                    equalInARow++;
                }
                
            } while (greaterInARow >= 3 || lessInARow >= 3 || equalInARow >= 3);

            ModifyNumber(currentNumber);
        }
        #endregion
    }
}