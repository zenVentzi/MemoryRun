using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Visual
{
    public class MostFrequentColourGame : BrainGame
    {
        #region variables

        private GameObject[] boxes;
        private GameButton[] buttons;

        private Color32[] currentButtonsColors,
                          currentBoxesColors,
                          allColors;

        private int numOfBoxesToShow;

        private float displayColorsTime,
            displayTime,
            minDisplayTime,
            displayTimeReducer;

        private Color32 mostSeenColor;

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            displayTime = 2f;
            minDisplayTime = .04f;
            displayTimeReducer = .05f;

            numOfBoxesToShow = 3;

            allColors = new[]
            {
                 new Color32(59, 132, 237, 237),//blue
                 new Color32(85, 229, 60, 255),//green
                 new Color32(255, 156, 57, 255),//orange
                 new Color32(246, 50, 23, 255),//red
                 new Color32(151, 49, 99, 255),//violet
                 new Color32(255, 255, 30, 255), //yellow
            };

            buttons = Go.GetComponentsInChildren<GameButton>();
            boxes = new GameObject[18];

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = GameObjectManager.GetGoInChildren(Go, "DownColor" + (i + 1));
            }

            currentBoxesColors = new Color32[boxes.Length];
            currentButtonsColors = new Color32[buttons.Length];
        }

        private Color FindDominant()
        {
            var colorToTimesSeen = new Dictionary<Color, int>();

            for (int i = 0; i < numOfBoxesToShow; i++)
            {
                var boxColor = currentBoxesColors[i];

                if (!colorToTimesSeen.ContainsKey(boxColor))
                {
                    colorToTimesSeen.Add(boxColor, 1);
                }
                else
                {
                    colorToTimesSeen[boxColor]++;
                }
            }

            var maxSeenTimes = colorToTimesSeen.Select(color => color.Value).Max();
            var colorsMaxSeenTimes = colorToTimesSeen.Count(color => color.Value == maxSeenTimes);

            if (colorsMaxSeenTimes == 1)//if there is only 1 dominant
                return colorToTimesSeen.First(col => col.Value == maxSeenTimes).Key;

            return Color.clear;
        }

        private IEnumerator DisplayDownColors()
        {
            displayColorsTime = 0;

            while (displayColorsTime < displayTime)
            {
                displayColorsTime += Time.deltaTime;
                yield return null;
            }

            Go.GetComponent<Animation>().Play("MostFrequentColorButtonsAppear");
        }

        private void LockButtons(bool lockk = true)
        {
            foreach (var button in buttons)
            {
                if (lockk)
                {
                    button.Lock();
                }
                else
                {
                    button.Unlock();
                }
            }
        }

        private void GenerateBoxesColor()
        {
            do
            {
                for (int i = 0; i < numOfBoxesToShow; i++)
                {
                    int random = Random.Range(0, currentButtonsColors.Length);
                    currentBoxesColors[i] = currentButtonsColors[random];
                }

                mostSeenColor = FindDominant();

            } while (mostSeenColor == Color.clear);
        }

        private void GenerateButtonsColor()
        {
            var usedIndexes = new List<int>();

            for (int i = 0; i < currentButtonsColors.Length; i++)
            {
                int random;

                do
                {
                    random = Random.Range(0, allColors.Length);
                } while (usedIndexes.Contains(random));

                usedIndexes.Add(random);

                currentButtonsColors[i] = allColors[random];
            }
        }

        private void AssignButtonsColor()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<SpriteRenderer>().color = currentButtonsColors[i];
            }
        }

        private void AssignBoxesColor()
        {
            for (int i = 0; i < numOfBoxesToShow; i++)
            {
                boxes[i].GetComponent<SpriteRenderer>().color = currentBoxesColors[i];
            }
        }

        [UsedImplicitly]
        private void OnButtonsAppear()
        {
            LockButtons(false);
            AssignButtonsColor();
            AbstractTime.Instance.Resume();
        }

        [UsedImplicitly]
        private void OnDownColorsAppear()
        {
            LockButtons();
            AssignBoxesColor();
            AbstractTime.Instance.Pause();
            StartCoroutine(DisplayDownColors());
        }

        private void GenerateColors()
        {
            GenerateButtonsColor();
            GenerateBoxesColor();
            DeactivateUnused();
        }

        private void DeactivateUnused()
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].SetActive(i < numOfBoxesToShow);
            }
        }

        private void ReduceDisplayTime()
        {
            if (displayTime > minDisplayTime)
            {
                displayTime -= displayTimeReducer;
            }
        }

        protected virtual bool IsCorrect()
        {
            return ClickedBtn.Go.GetComponent<SpriteRenderer>().color == mostSeenColor;
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            ReduceDisplayTime();

            if (numOfBoxesToShow < boxes.Length)
                numOfBoxesToShow++;

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
            GenerateColors();
            Go.GetComponent<Animation>().Play("MostFrequentDownColorsAppear");
        }
        #endregion
    }
}