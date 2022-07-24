using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Games.BrainZ.Memory
{
    public class NumbersOrderGame : BrainGame
    {
        private int[] nums;
        private Text mainNum;
        private GameButton[] buttons;
        private int currentNumIndex,
                    numbersToShow;

        private bool IsCorrect()
        {
            return int.Parse(ClickedBtn.GetText()) == nums[currentNumIndex];
        }

        private bool IsLastOne()
        {
            return currentNumIndex == nums.Length;
        }

        private IEnumerator DisplayNums()
        {
            yield return new WaitForSeconds(1);

            LockButtons();
            //float numDisplayTime = nums.Length * .07f;
            float elapsed = 0;
            const float numDisplayTime = 0.6f;

            mainNum.text = nums[0].ToString();

            while (true)
            {
                if (elapsed >= numDisplayTime)
                {
                    if (++currentNumIndex == nums.Length)
                        break;

                    elapsed = 0;
                    mainNum.text = nums[currentNumIndex].ToString();
                }
                else
                {
                    elapsed += Time.deltaTime;
                }

                yield return null;
            }

            LockButtons(false);
            currentNumIndex = 0;
            AssignNumsToButtons();
            mainNum.text = string.Empty;
            AbstractTime.Instance.Resume();
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

        private void AssignNumsToButtons()
        {
            var usedIndexes = new List<int>();

            AssignRealNums(usedIndexes);
            AssignFakeNums(usedIndexes);
        }

        private void AssignFakeNums(List<int> usedIndexes)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (usedIndexes.Contains(i)) continue;
                int fakeNum;

                do
                {
                    fakeNum = Random.Range(1, 100);
                } while (NumAlreadyUsed(fakeNum));

                buttons[i].SetText(fakeNum.ToString());
            }
        }

        private void AssignRealNums(List<int> usedIndexes)
        {
            foreach (int num in nums)
            {
                int buttonIndex;

                do
                {
                    buttonIndex = Random.Range(0, buttons.Length);
                } while (usedIndexes.Contains(buttonIndex));

                usedIndexes.Add(buttonIndex);
                buttons[buttonIndex].SetText(num.ToString());
            }
        }

        private bool NumAlreadyUsed(int num)
        {
            foreach (GameButton gameButton in buttons)
            {
                int gameButtonNum;

                if (int.TryParse(gameButton.GetText(), out gameButtonNum) && gameButtonNum == num)
                    return true;
            }

            return false;
        }

        private void ResetNums()
        {
            nums = new int[numbersToShow];
            mainNum.text = string.Empty;
            currentNumIndex = 0;

            foreach (var gameButton in buttons)
            {
                gameButton.SetText(string.Empty);
            }
        }

        private void ResetButtons()
        {
            foreach (var gameButton in buttons)
            {
                gameButton.Reset();
            }
        }

        protected override void Init()
        {
            base.Init();
            numbersToShow = 1;
            MaxNumOfMistakes = 10;
            mainNum = GameObjectManager.GetGoInChildren(Go, "TV").GetComponent<Text>();
            buttons = Go.GetComponentsInChildren<GameButton>();
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            ClickedBtn.Lock();
            currentNumIndex++;
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
                numbersToShow += numbersToShow < 14 ? 1 : 0;
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
            ResetNums();
            AbstractTime.Instance.Pause();

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = Random.Range(1, 100);
            }

            StartCoroutine(DisplayNums());
        }
    }
}
