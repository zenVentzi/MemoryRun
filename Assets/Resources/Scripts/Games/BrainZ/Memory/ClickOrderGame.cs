using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Memory
{
    public class ClickOrderGame : BrainGame
    {
        #region variables
        private GameObject area;
        private GameButton[] buttons;

        private int supposedBoxClickIndex,
                    numOfActiveButtons;

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            numOfActiveButtons = 3;
            area = GameObjectManager.GetGoInChildren(Go, "Area");
            buttons = Go.GetComponentsInChildren<GameButton>();

            for (int i = numOfActiveButtons; i < buttons.Length; i++)
            {
                buttons[i].Go.SetActive(false);
            }
        }

        private IEnumerator ButtonsAppear()
        {
            int lastAnimPlayedIndex = 0;
            var playedAnims = new List<GameButton>();

            while (lastAnimPlayedIndex < numOfActiveButtons)
            {
                if (!playedAnims.Contains(buttons[lastAnimPlayedIndex]))
                {
                    buttons[lastAnimPlayedIndex].Anim.Play("ClickOrderButtonAppear");
                    playedAnims.Add(buttons[lastAnimPlayedIndex]);
                }
                else if (!buttons[lastAnimPlayedIndex].Anim.IsPlaying("ClickOrderButtonAppear"))
                {
                    lastAnimPlayedIndex++;
                }

                yield return null;
            }

            UnlockButtons();
            supposedBoxClickIndex = 0;
            AbstractTime.Instance.Resume();
        }

        private void DeactivateButtonsColliders()
        {
            foreach (var button in buttons)
            {
                button.Colider.enabled = false;
            }
        }

        private void GenerateNewPositions()
        {
            int counter = 0;
            const int maxAttemptsPerButton = 400;

            do
            {
                DeactivateButtonsColliders();//in order to avoid checks from unspawned buttons

                foreach (var button in buttons)
                {
                    counter = 0;
                    button.Colider.enabled = true;

                    do
                    {
                        counter++;
                        button.Tr.position = GetRandomPos();
                    } while ((IntersectsAnotherButton(button) || !button.Go.IsObjectInArea(area)) && counter <= maxAttemptsPerButton);

                    if(counter > maxAttemptsPerButton)
                        break;
                }

            } while (counter > maxAttemptsPerButton);
        }

        private bool IntersectsAnotherButton(GameButton current)
        {
            return buttons.Any(button => button != current &&
                button.Colider.enabled &&
                button.Colider.bounds.Intersects(current.Colider.bounds));
        }

        private void NormalizeButtonsScale()
        {
            Tr.GetChild(0).localScale = Vector2.one;

            foreach (var button in buttons)
            {
                button.Tr.localScale = Vector2.one;
            }
        }

        private void MinimizeButtons()
        {
            foreach (var button in buttons)
            {
                button.Tr.localScale = Vector2.zero;
            }
        }

        private Vector2 GetRandomPos()
        {
            var randomPos = new Vector2(Random.Range(-907, 918), Random.Range(-362, 208));
            return randomPos;
        }

        [UsedImplicitly]
        private IEnumerator OnDisappearAnimFinish()
        {
            yield return null;

            while (PauseButton.Instance.GamePaused)
            {
                yield return null;
            }

            area.SetActive(true);
            NormalizeButtonsScale();//normalize size for the intersection with other buttons check
            GenerateNewPositions();
            area.SetActive(false);
            MinimizeButtons();//return back to 000 for the appearance TODO: if you comment it out, the game becomes much harder and unusual(might create similar one to this)
            StartCoroutine(ButtonsAppear());
        }

        private void LockButtons()
        {
            for (int i = 0; i < numOfActiveButtons; i++)
            {
                buttons[i].Lock();
            }
        }

        private void UnlockButtons()
        {
            for (int i = 0; i < numOfActiveButtons; i++)
            {
                buttons[i].Unlock();
            }
        }

        private void ResetButtons()
        {
            for (int i = 0; i < numOfActiveButtons; i++)
            {
                buttons[i].Reset();
            }
        }

        private bool IsCorrect()
        {
            return ClickedBtn == buttons[supposedBoxClickIndex];
        }

        private bool AllClicked()
        {
            for (int i = 0; i < numOfActiveButtons; i++)
            {
                if (!buttons[i].Selected)
                    return false;
            }

            return true;
        }

        private bool IsIncorrect()
        {
            return ClickedBtn != buttons[supposedBoxClickIndex];
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

                supposedBoxClickIndex++;
                ClickedBtn.Lock();

                if (AllClicked())
                {
                    if (numOfActiveButtons < buttons.Length)
                    {
                        numOfActiveButtons++;
                        buttons[numOfActiveButtons - 1].Go.SetActive(true);
                    }

                    GenerateNew();
                }                                        
            }
            else if (IsIncorrect())
            {
                ValidateIncorrect();
            }
        }

        protected override void GenerateNew()
        {
            Anim.Play("ClickOrderButtonsDisappear");
            AbstractTime.Instance.Pause();
            ResetButtons();
            LockButtons();
        }
        #endregion
    }
}