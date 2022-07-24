using System;
using System.Collections.Generic;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.BrainZ.Calculation
{
    public class CalculusGame : BrainGame//TODO: fix generate random division
    {
        #region variables
        private Text firstAnswerText,
                     secondAnswerText,
                     thirdAnswerText,
                     questionText;

        private int firstNumber,
                    secondNumber,
                    result,
                    sign;
        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();

            questionText = GameObjectManager.GetGoInChildren(Go, "Question").GetComponent<Text>();
            firstAnswerText = GameObjectManager.GetGoInChildren(Go, "FirstAnswer").GetComponent<Text>();
            secondAnswerText = GameObjectManager.GetGoInChildren(Go, "SecondAnswer").GetComponent<Text>();
            thirdAnswerText = GameObjectManager.GetGoInChildren(Go, "ThirdAnswer").GetComponent<Text>();
        }

        private int GetClickedNumber()
        {
            return int.Parse(ClickedBtn.GetText());
        }

        private void GenerateRandomSum()
        {
            do
            {
                firstNumber = Random.Range(15, 101);
                secondNumber = Random.Range(9, 101);
            } while (firstNumber % 10 == 0 || secondNumber % 10 == 0);

            result = firstNumber + secondNumber;
            questionText.text = String.Format("{0} + {1} = ?", firstNumber, secondNumber);
        }

        private void GenerateRandomSubstraction()
        {
            firstNumber = Random.Range(0, 101);
            do
            {
                secondNumber = Random.Range(0, 100);
            } while (secondNumber % 10 == 0); //secondNumber > firstNumber

            result = firstNumber - secondNumber;
            questionText.text = String.Format("{0} - {1}  =  ?", firstNumber, secondNumber);
        }

        private void GenerateRandomMultiplication()
        {
            firstNumber = Random.Range(2, 20);
            secondNumber = Random.Range(3, 10);
            result = firstNumber * secondNumber;
            questionText.text = String.Format("{0} * {1} = ?", firstNumber, secondNumber);
        }

        private void GenerateRandomDivision()
        {
            do
            {
                firstNumber = Random.Range(-201, 201);
                secondNumber = Random.Range(2, 201);
                
            } while (firstNumber % secondNumber != 0 || Mathf.Abs(firstNumber / secondNumber) < 3);

            result = firstNumber / secondNumber;
            questionText.text = String.Format("{0} / {1} = ?", firstNumber, secondNumber);
        }

        //private bool IsPrime(int number)
        //{
        //    for (int i = 2; i <= number / 2; i++)
        //    {
        //        if (number % i == 0)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        private void GenerateAnswers()
        {
            int secondAnswer = GetSecondAnswer(),
                thirdAnswer = GetThirdAnswer(secondAnswer);

            int resultPosition = Random.Range(0, 3),
                secondNumberPosition = GetSecondNumberPosition(resultPosition),
                thirdNumberPosition = GetThirdNumberPosition(secondNumberPosition, resultPosition);

            var answers = new int[3];
            answers[resultPosition] = result;
            answers[secondNumberPosition] = secondAnswer;
            answers[thirdNumberPosition] = thirdAnswer;

            firstAnswerText.text = answers[0].ToString();
            secondAnswerText.text = answers[1].ToString();
            thirdAnswerText.text = answers[2].ToString();
        }

        private int GetThirdAnswer(int secondAnswer)
        {
            int thirdAnswer;

            do
            {
                var rand = Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        thirdAnswer = result - 10;
                        break;
                    case 1:
                        thirdAnswer = result + 10;
                        break;
                    default:
                        thirdAnswer = result + 20;
                        break;
                }
            } while (thirdAnswer == secondAnswer);

            return thirdAnswer;
        }

        private int GetSecondAnswer()
        {
            int secondAnswer;
            var rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    secondAnswer = result - 10;
                    break;
                case 1:
                    secondAnswer = result + 10;
                    break;
                default:
                    secondAnswer = result + 20;
                    break;
            }

            return secondAnswer;
        }

        private int GetThirdNumberPosition(int secondNumberPosition, int resultPosition)
        {
            int thirdNumberPosition;
            do
            {
                thirdNumberPosition = Random.Range(0, 3);

            } while (thirdNumberPosition == secondNumberPosition || thirdNumberPosition == resultPosition);

            return thirdNumberPosition;
        }

        private int GetSecondNumberPosition(int resultPosition)
        {
            int secondNumberPosition;
            do
            {
                secondNumberPosition = Random.Range(0, 3);
            } while (secondNumberPosition == resultPosition);

            return secondNumberPosition;
        }

        protected virtual bool IsCorrect()
        {
            return GetClickedNumber() == result;
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            GenerateNew();
        }

        protected override void ValidateIncorrect()
        {
            base.ValidateIncorrect();

            if (!GameOver)
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
            sign = Random.Range(0, 5); // 0+, 1-, 2*, 3:

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

            GenerateAnswers();
        }
        #endregion
    }
}