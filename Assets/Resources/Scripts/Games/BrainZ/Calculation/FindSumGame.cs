using System;
using System.Linq;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.BrainZ.Calculation
{
    public class FindSumGame : BrainGame
    {
        #region variables

        private GameButton[] buttons;
        private int[] randomNumbers;
        private int totalSum,
                    mainNumber;
        #endregion

        #region methods

        private void CalculateTotalSum()
        {
            if (ClickedBtn.Selected)
            {
                totalSum += GetCurrentNumber();
            }
            else
            {
                totalSum -= GetCurrentNumber();
            }
        }

        private int GetCurrentNumber()
        {
            return int.Parse(ClickedBtn.GetText());
        }

        private void Restart()
        {
            GenerateNew();
            totalSum = 0;

            foreach (var button in buttons)
            {
                button.Reset();
            }
        }

        private bool IsPossible()
        {
            int maxSubsets = (int)Math.Pow(2, randomNumbers.Length) - 1;

            for (int i = 1; i <= maxSubsets; i++)
            {
                var subset = "";
                long checkingSum = 0;

                for (int j = 0; j <= randomNumbers.Length; j++)
                {
                    int mask = 1 << j,
                        nAndMask = i & mask,
                        bit = nAndMask >> j;

                    if (bit == 1)
                    {
                        checkingSum = checkingSum + randomNumbers[j];
                        subset = subset + " " + randomNumbers[j];
                    }
                }

                if (checkingSum == mainNumber)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void Init()
        {
            base.Init();
            buttons = Go.GetComponentsInChildren<GameButton>();
        }

        protected virtual bool IsCorrect()
        {
            return totalSum == mainNumber;
        }

        protected virtual bool IsIncorrect()
        {
            return totalSum > mainNumber;
        }

        protected override void ValidateIncorrect()
        {
            base.ValidateIncorrect();

            if (!GameOver)
                Restart();
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            Restart();
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);
            CalculateTotalSum();

            if (IsCorrect())
            {
                ValidateCorrect();
            }
            else if (IsIncorrect())
            {
                ValidateIncorrect();
            }
        }

        protected override void GenerateNew()
        {
            randomNumbers = new int[9];
            mainNumber = Random.Range(5, 100);

            do
            {
                for (int i = 0; i < 9; i++)
                {
                    randomNumbers[i] = Random.Range(1, mainNumber);
                }

            } while (!IsPossible());

            var currentNumberIndex = 0;

            GameObjectManager.GetGoInChildren(Go, "MainNumber").GetComponent<Text>().text = mainNumber.ToString();

            foreach (var button in buttons)
            {
                button.SetText(randomNumbers[currentNumberIndex].ToString());
                currentNumberIndex++;
            }
        }
        #endregion
    }
}