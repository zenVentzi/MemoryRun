using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Visual
{
    public class WrongColorGame : BrainGame
    {
        #region variables

        private GameButton[] buttons;
        private string[] colorTexts;
        private int numOfElementsToClick,
                    numOfCorrectSoFar;

        private Sprite greenBackground,
            redBackground;

        private Sprite[] colors;
        #endregion

        protected override void Init()
        {
            base.Init();
            colorTexts = new[] { "black", "blue", "green", "orange", "purple", "red", "white", "yellow" };
            buttons = Go.GetComponentsInChildren<GameButton>();
            colors = UnityEngine.Resources.LoadAll<Sprite>("Textures/Games/BrainZ/Visual/WrongColor/AllColors");
            
            greenBackground =
                UnityEngine.Resources.Load<Sprite>("Textures/Games/BrainZ/Visual/GreenBackground");
            redBackground =
                UnityEngine.Resources.Load<Sprite>("Textures/Games/BrainZ/Visual/RedBackground");
        }

        private void Restart()
        {
            foreach (var button in buttons)
            {
                button.Reset();
            }

            GenerateNew();
            numOfCorrectSoFar = 0;
        }

        private Sprite GetRandomColorSprite(List<Sprite> usedColors)
        {
            Sprite randomColor;

            do
            {
                 randomColor = colors[Random.Range(0, colors.Length)];
            } while (usedColors.Contains(randomColor));
            
            usedColors.Add(randomColor);

            return randomColor;
        }

        private void GenerateRandomBackground()
        {
            SpriteRend.sprite = Random.Range(0, 2) == 1 ? greenBackground: redBackground;
        }

        private void GenerateRandomColors()
        {
            var usedColors = new List<Sprite>();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<SpriteRenderer>().sprite = GetRandomColorSprite(usedColors);
            }

            usedColors.Clear();
        }

        private void GenerateTexts()
        {
            do
            {
                var usedColorTexts = new List<string>();

                for (int i = 0; i < buttons.Length; i++)
                {
                    string currentColor;

                    do
                    {
                        currentColor = colorTexts[Random.Range(0, 8)];
                    } while (usedColorTexts.Contains(currentColor));

                    usedColorTexts.Add(currentColor);
                    buttons[i].SetText(currentColor); 
                }

            } while (!MoreThan1Correct());
        }

        private bool MoreThan1Correct()
        {
            return buttons.Count(ButtonInSeries) == numOfElementsToClick;
        }

        private bool ButtonInSeries(GameButton button)
        {
            if (CorrectSeries())
                return SameColorText(button);

            return !SameColorText(button);
        }

        private bool SameColorText(GameButton button)
        {
            var spriteColor = button.SpriteRend.sprite.name.ToLower();
            var buttonText = button.GetText().ToLower();

            return spriteColor.Contains(buttonText);
        }

        private bool CorrectSeries()
        {
            return SpriteRend.sprite == greenBackground;
        }

        protected virtual bool IsCorrect()
        {
            return ButtonInSeries(ClickedBtn);
        }

        protected override void ValidateCorrect()
        {
            if (++numOfCorrectSoFar != numOfElementsToClick) return;

            Restart();
            base.ValidateCorrect();
        }

        protected override void ValidateIncorrect()
        {
            base.ValidateIncorrect();

            if(!GameOver)
                Restart();
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            ClickedBtn.Lock(false);

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
            numOfElementsToClick = Random.Range(1, 4);
            GenerateRandomBackground();
            GenerateButtons();
        }

        private void GenerateButtons()
        {
            GenerateRandomColors();
            GenerateTexts();
        }
    }
}