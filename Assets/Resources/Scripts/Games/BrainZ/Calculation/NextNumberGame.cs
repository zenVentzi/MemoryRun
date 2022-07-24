using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.BrainZ.Calculation
{
    public enum Action
    {
        Sum,
        Substraction,
        Multiplication
    }

    public class NextNumberGame : BrainGame
    {
        #region variables

        private Action action;
        private GameButton[] buttons;
        private int differenceNum;
        private int[] nums;
        private int result;

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            buttons = Tr.GetComponentsInChildren<GameButton>();
        }

        private bool GoodEnough(int num, ICollection<int> usedNums)
        {
            return !usedNums.Contains(num) && (result % 10) == (num % 10) &&
                   (result >= 0 || usedNums.Any(n => n < 0 && n != result) || num <= 0);
        }

        private void GenerateAnswers()
        {
            var usedNumbers = new List<int>();
            var answerss = new List<int>(4);
            usedNumbers.Add(result);
            answerss.Add(result);

            for (int i = 1; i < 4; i++) //Generating the numbers
            {
                int num;

                do
                {
                    num = Random.Range(result - 30, result + 31);
                } while (!GoodEnough(num, usedNumbers));

                answerss.Add(num);
                usedNumbers.Add(num);
            }

            usedNumbers.Clear();
            foreach (var btn in buttons) //Putting them into random places
            {
                int indexer;

                do
                {
                    indexer = Random.Range(0, 4);
                } while (usedNumbers.Contains(indexer));

                usedNumbers.Add(indexer);
                btn.GetComponent<Text>().text = answerss[indexer].ToString();
            }
        }

        private void GenerateProblem()
        {
            action = (Action)Random.Range(0, 5);
            nums = new int[3];

            switch (action)
            {
                case Action.Sum:
                    GenerateSum();
                    break;
                case Action.Substraction:
                    GenerateSubstraction();
                    break;
                case Action.Multiplication:
                    GenerateMultiplication();
                    break;
                default:
                    GenerateDivision();
                    break;
            }

            GameObjectManager.GetGoInChildren(Go, "Question").GetComponent<Text>().text =
                String.Format("{0} ,  {1} ,  {2} ,  ?", nums[0], nums[1], nums[2]);
        }

        private void GenerateDivision()
        {
            do
            {
                differenceNum = Random.Range(-5, 5);
            } while (differenceNum == 0 || Mathf.Abs(differenceNum) == 1);

            nums[0] = (int)Mathf.Pow(differenceNum, Random.Range(3, 5));
            nums[1] = nums[0] / differenceNum;
            nums[2] = nums[1] / differenceNum;
            result = nums[2] / differenceNum;
        }

        private void GenerateMultiplication()
        {
            do
            {
                differenceNum = Random.Range(-5, 5);
            } while (differenceNum == 0 || Mathf.Abs(differenceNum) == 1);

            nums[0] = Random.Range(3, differenceNum * 2);
            nums[1] = nums[0] * differenceNum;
            nums[2] = nums[1] * differenceNum;
            result = nums[2] * differenceNum;
        }

        private void GenerateSubstraction()
        {
            do
            {
                differenceNum = Random.Range(40, 200);
            } while (differenceNum % 10 == 0);

            nums[0] = Random.Range(-50, 50);
            nums[1] = nums[0] - differenceNum;
            nums[2] = nums[1] - differenceNum;
            result = nums[2] - differenceNum;
        }

        private void GenerateSum()
        {
            do
            {
                differenceNum = Random.Range(50, 250);
            } while (differenceNum % 10 == 0);

            nums[0] = Random.Range(-200, 50);
            nums[1] = nums[0] + differenceNum;
            nums[2] = nums[1] + differenceNum;
            result = nums[2] + differenceNum;
        }

        protected virtual bool IsCorrect()
        {
            return int.Parse(ClickedBtn.GetComponent<Text>().text) == result;
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
            GenerateProblem();
            GenerateAnswers();
        }

        #endregion
    }
}