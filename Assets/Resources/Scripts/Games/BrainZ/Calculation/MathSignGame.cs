using System;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.BrainZ.Calculation
{
    public class MathSignGame : BrainGame//TODO: FIX GENERATE RANDOM DIVISION
    {
        #region variables
        private GameObject equationGo;
        private Text questionText;
        private int result,
                    firstNumber,
                    secondNumber,
                    sign;
        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            equationGo = GameObjectManager.GetGoInChildren(Go, "Equation").gameObject;
            questionText = equationGo.GetComponent<Text>();
        }

        private bool PlusClck()
        {
            return ClickedBtn.name == "Plus";
        }

        private bool MinusClck()
        {
            return ClickedBtn.name == "Minus";
        }

        private bool MultiplicationClck()
        {
            return ClickedBtn.name == "Times";
        }

        private bool DivClck()
        {
            return ClickedBtn.name == "Div";
        }

        private bool IsThereSpecialCase()
        {
            return (Mathf.Abs(secondNumber) == 1 && (DivClck() || MultiplicationClck()) ||
                   (firstNumber == 0 && (DivClck() || MultiplicationClck()) ||
                   ((firstNumber == secondNumber && Mathf.Abs(result) == 4) &&
                    (MultiplicationClck() || PlusClck())) ||
                   ((firstNumber == 4 && secondNumber == 2 && result == 2) &&
                    (DivClck() || MinusClck())) ||
                   (firstNumber == secondNumber && secondNumber == 0) &&
                   (PlusClck() || MinusClck() || MultiplicationClck()) ||
                   (secondNumber == 0 && result == firstNumber) &&
                   (PlusClck() || MinusClck())));
        }

        private void GenerateRandomSum()
        {
            firstNumber = Random.Range(-100, 101);
            secondNumber = Random.Range(-100, 101);
            result = firstNumber + secondNumber;
        }

        private void GenerateRandomSubstraction()
        {
            firstNumber = Random.Range(-100, 101);
            secondNumber = Random.Range(-100, 100);
            result = firstNumber - secondNumber;
        }

        private void GenerateRandomMultiplication()
        {
            firstNumber = Random.Range(-10, 10);
            secondNumber = Random.Range(-10, 10);
            result = firstNumber * secondNumber;
        }

        private void GenerateRandomDivision()
        {
            do
            {
                firstNumber = Random.Range(-100, 101);
            } while (IsPrime(firstNumber));

            do
            {
                secondNumber = Random.Range(-100, 101);
            } while (secondNumber == 0 || firstNumber % secondNumber != 0);

            result = firstNumber / secondNumber;
        }

        private bool IsPrime(int number)
        {
            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected virtual bool IsCorrect()
        {
            return (PlusClck() && sign == 0) ||
                   (MinusClck() && sign == 1) ||
                   (MultiplicationClck() && sign == 2) ||
                   (DivClck() && sign == 3) ||
                   (IsThereSpecialCase());
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

            if (!GameOver)
                GenerateNew();
        }

        protected override void GenerateNew()
        {
            sign = Random.Range(0, 4);

            switch (sign)
            {
                case 0:
                    GenerateRandomSum();
                    break;
                case 1:
                    GenerateRandomSubstraction();
                    break;
                case 2:
                    GenerateRandomMultiplication();
                    break;
                default:
                    GenerateRandomDivision();
                    break;
            }

            questionText.text = String.Format("{0}     {1} = {2}", firstNumber, secondNumber, result);
        }
        #endregion
    }
}