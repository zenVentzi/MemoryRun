using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Visual
{
    public class ColorOrderGame : BrainGame
    {
        #region variables
        private int currentColourIndex,
                    numOfBoxesToShow;
        private GameObject[] boxes;
        private GameButton[] buttons;
        private Color32[] allColors,
                          currentBoxesColor,
                          currentButtonsColor;

        #region display time
        private float displayBoxesTime,
            displayTime,
            minDisplayTime,
            displayTimeReducer;
        #endregion

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            displayTime = 3f;
            minDisplayTime = .6f;
            displayTimeReducer = .1f;

            MaxNumOfMistakes = 8;
            numOfBoxesToShow = 3;
            boxes = new GameObject[5];
            buttons = Go.GetComponentsInChildren<GameButton>();
            allColors = new[]
            {
                 new Color32(59, 132, 237, 237),//blue
                 new Color32(85, 229, 60, 255),//green
                 new Color32(255, 156, 57, 255),//orange
                 new Color32(246, 50, 23, 255),//red
                 new Color32(151, 49, 99, 255),//violet
                 new Color32(255, 255, 30, 255), //yellow
                 new Color32(216, 51, 91, 255), //pink
            };

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = GameObjectManager.GetGoInChildren(Go, "Box" + (i + 1));
            }

            currentButtonsColor = new Color32[buttons.Length];
            currentBoxesColor = new Color32[boxes.Length];
        }

        private void ResetButtons()
        {
            foreach (var button in buttons)
            {
                button.Reset();
            }
        }

        private void GenerateBoxesColor()
        {
            var usedIndexes = new List<int>();

            for (int i = 0; i < numOfBoxesToShow; i++)
            {
                int random;

                do
                {
                    random = Random.Range(0, buttons.Length);
                } while (usedIndexes.Contains(random));

                usedIndexes.Add(random);
                currentBoxesColor[i] = currentButtonsColor[random];
            }
        }

        private void GenerateButtonsColor()
        {
            var usedIndexes = new List<int>();

            for (int i = 0; i < currentButtonsColor.Length; i++)
            {
                int random;

                do
                {
                    random = Random.Range(0, allColors.Length);
                } while (usedIndexes.Contains(random));

                usedIndexes.Add(random);
                currentButtonsColor[i] = allColors[random];
            }
        }

        private void AssignBoxesColor()
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].GetComponent<SpriteRenderer>().color = currentBoxesColor[i];
            }
        }

        private void AssignButtonsColor()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<SpriteRenderer>().color = currentButtonsColor[i];
            }
        }

        private IEnumerator DisplayBoxes()
        {
            displayBoxesTime = 0;

            while (displayBoxesTime < displayTime)
            {
                displayBoxesTime += Time.deltaTime;
                yield return null;
            }

            Go.GetComponent<Animation>().Play("ColorOrderButtonsAppear");
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

        [UsedImplicitly]
        private void OnBoxesAppear()
        {
            LockButtons();
            AssignBoxesColor();
            AbstractTime.Instance.Pause();
            StartCoroutine(DisplayBoxes());
        }

        [UsedImplicitly]
        private void OnButtonsAppear()
        {
            LockButtons(false);
            AssignButtonsColor();
            AbstractTime.Instance.Resume();            
        }

        private bool IsCorrect()
        {
            return ClickedBtn.GetComponent<SpriteRenderer>().color == currentBoxesColor[currentColourIndex];
        }

        private bool IsLastOne()
        {
            return currentColourIndex == numOfBoxesToShow;            
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            currentColourIndex++;
            ClickedBtn.Lock();
        }

        protected override void ValidateIncorrect()
        {
            base.ValidateIncorrect();

            if (GameOver) return;
            ResetButtons();
            GenerateNew();
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            if (IsCorrect())
            {
                ValidateCorrect();

                if (!IsLastOne()) return;

                if (numOfBoxesToShow < boxes.Length)
                    numOfBoxesToShow++;

                ReduceDisplayTime();
                ResetButtons();
                GenerateNew();
            }
            else
            {
                ValidateIncorrect();
            }
        }

        protected override void GenerateNew()
        {
            GenerateColors();
            DeactivateLocked();
            currentColourIndex = 0;
            Go.GetComponent<Animation>().Play("ColorOrderBoxesAppear");
        }

        private void DeactivateLocked()
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].SetActive(i < numOfBoxesToShow);
            }
        }

        private void GenerateColors()
        {
            GenerateButtonsColor();
            GenerateBoxesColor();
        }

        private void ReduceDisplayTime()
        {
            if (displayTime - displayTimeReducer > minDisplayTime)
            {
                displayTime -= displayTimeReducer;
            }
            else if (displayTime > minDisplayTime)
            {
                displayTime = minDisplayTime;
            }
        }

        #endregion
    }
}