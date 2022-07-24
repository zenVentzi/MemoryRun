using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Memory
{
    public class MemoryMatrixGame : BrainGame
    {
        [SerializeField]
        private Sprite xSprite,
                       defaultSprite;
        private GameButton[] buttons,
                             correctButtons;

        private int numOfCorrect;

        private bool Correct()
        {
            return correctButtons.Contains(ClickedBtn);
        }

        private void LockButtons(bool lockk = true)
        {
            foreach (var gameButton in buttons)
            {
                if (lockk)
                {
                    gameButton.Lock();
                }
                else
                {
                    gameButton.Unlock();
                }
            }
        }

        private IEnumerator DisplayCorrectButtons()
        {
            LockButtons();
            AbstractTime.Instance.Pause();
            yield return new WaitForSeconds(1);

            foreach (var correctButton in correctButtons)
            {
                correctButton.SpriteRend.color = Color.white;
            }

            var secs = 1 + numOfCorrect / 10f;
            yield return new WaitForSeconds(secs);

            foreach (var correctButton in correctButtons)
            {
                correctButton.SpriteRend.color = Color.white;
            }

            AbstractTime.Instance.Resume();
            LockButtons(false);
        }

        private void ResetButtons()
        {
            foreach (var gameButton in buttons)
            {
                gameButton.Reset();
                gameButton.SpriteRend.sprite = defaultSprite;
            }
        }

        private bool RunFinished()
        {
            return correctButtons.All(b => b.Selected);
        }

        private bool AnyMistakes()
        {
            return buttons.Any(b => b.SpriteRend.sprite == xSprite);
        }

        protected override void Init()
        {
            base.Init();
            numOfCorrect = 1;
            MaxNumOfMistakes = int.MaxValue;
            buttons = Tr.GetChild(0).GetComponentsInChildren<GameButton>();
        }

        protected override void ValidateCorrect()
        {
            if (RunFinished())
            {
                if (!AnyMistakes())
                {
                    numOfCorrect++;
                }

                ResetButtons();
                GenerateNew();
            }
            else
            {
                ReduceAlpha(ClickedBtn);
            }

            base.ValidateCorrect();
        }

        private void ReduceAlpha(GameButton btn)
        {
            var color = btn.SpriteRend.color;
            color.a *= .4f;
            btn.SpriteRend.color = color;
        }

        protected override void ValidateIncorrect()
        {
            ((BrainGameScore)ScoreRef).ResetAdditionalPoints();
            ClickedBtn.SpriteRend.sprite = xSprite;
            AbstractTime.Instance.TimeProp -= 1;
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);
            ClickedBtn.Lock(false);

            if (Correct())
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
            correctButtons = new GameButton[numOfCorrect];
            var usedIndexes = new List<int>();

            for (int i = 0; i < numOfCorrect; i++)
            {
                int index;

                do
                {
                    index = Random.Range(0, buttons.Length);
                } while (usedIndexes.Contains(index));

                correctButtons[i] = buttons[index];
                usedIndexes.Add(index);
            }

            StartCoroutine(DisplayCorrectButtons());
        }
    }
}
