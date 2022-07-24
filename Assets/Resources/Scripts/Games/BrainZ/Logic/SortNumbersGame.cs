using System.Linq;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Logic
{
    public class SortNumbersGame : SizeDependentGame
    {
        private const int XDistance = 74,
                          YDistance = 58;

        private SortNumbersButton[] buttons;
        private Vector2 emptyPos;

        private bool Solved
        {
            get
            {
                return buttons.All(button => button.IsCorrectPosition);
            }
        }

        protected override string SizeTxt
        {
            get { return base.SizeTxt; }
            set { base.SizeTxt = string.Format("{0} x {0}", value); }
        }

        private Vector2 GetLastBoxPos()
        {
            var lastBox = buttons.Last().Tr;
            return lastBox.localPosition;
        }

        private int GetMixSuccess()
        {
            var movedBoxesCount = GetNumberOfMovedBoxes();
            var percent = (movedBoxesCount * 100) / (float)(Size * Size - 1);

            return (int)percent;
        }

        private int GetNumberOfMovedBoxes()
        {
            return buttons.Count(b => !b.IsCorrectPosition);
        }

        private void MixUpBoxes()
        {
            do
            {
                MixPositions();
            } while (GetMixSuccess() < 50);
        }

        private void MixPositions()
        {
            foreach (var button in buttons)
            {
                var swap = Random.Range(0, 2) == 1;

                if (swap)
                {
                    var other = buttons[Random.Range(0, buttons.Length)];

                    while (other == button)
                    {
                        other = buttons[Random.Range(0, buttons.Length)];
                    }

                    SwapBoxes(button, other);
                }
                else
                {
                    PutOnEmpty(button);
                }
            }
        }

        private void PutOnEmpty(GameButton button)
        {
            var temp = button.Tr.localPosition;
            button.Tr.localPosition = emptyPos;
            emptyPos = temp;
        }

        private static void SwapBoxes(SortNumbersButton current, SortNumbersButton other)
        {
            var temp = current.Tr.position;
            current.Tr.position = other.Tr.position;
            other.Tr.position = temp;
        }

        private void LockButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Lock(false);
            }
        }

        private void UpdateMovableBoxes()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (NextToEmpty(buttons[i]))
                {
                    buttons[i].Unlock(false);
                }
                else
                {
                    buttons[i].Lock(false);
                }
            }
        }

        private bool NextToEmpty(SortNumbersButton box)
        {
            var distanceToEmpty = Vector2.Distance(box.Tr.localPosition, emptyPos);
            var maxDistance = Mathf.Max(XDistance, YDistance) + .5f;//+.5f because of float comparison problems
            return distanceToEmpty <= maxDistance;
        }

        protected override void Init()
        {
            base.Init();
            Moves = 0;
            Size = 3;
            SetupNewCollection();
            AbstractTime.Instance.Pause();
            CollidersManager.EnableGameColliders();
        }

        private void SetupNewCollection()
        {
            RemoveOldPrefab();
            var gridPrefab = GetGridPrefab();
            buttons = gridPrefab.GetComponentsInChildren<SortNumbersButton>();
            CalculatePositions();
            LockButtons();
        }

        private void CalculatePositions()
        {
            var lastPos = GetLastBoxPos();
            lastPos.x += XDistance;
            emptyPos = lastPos;
        }

        private void RemoveOldPrefab()
        {
            if (buttons != null)
                Destroy(buttons[0].Tr.parent.gameObject);
        }

        private GameObject GetGridPrefab()
        {
            var gridPath = string.Format("Prefabs/BrainZ/SortNumbersGame/Grid{0}x{0}", Size);
            var gridPrefab = Instantiate(UnityEngine.Resources.Load<GameObject>(gridPath));
            gridPrefab.transform.parent = Tr;
            gridPrefab.transform.localScale = Vector2.one;
            gridPrefab.name = gridPrefab.name.Substring(0, gridPrefab.name.LastIndexOf("(Clone)"));
            return gridPrefab;
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            if (clickedButton.name == "StartButton")
            {
                clickedButton.Go.SetActive(false);
                DisableModifyButtons();
                HasGameStarted = true;
                MixUpBoxes();
                UpdateMovableBoxes();
                AbstractTime.Instance.Resume();
            }
            else if(ClickedBtn is SortNumbersButton)
            {
                Moves++;
                PutOnEmpty(ClickedBtn);
                UpdateMovableBoxes();

                if (Solved)
                    GameOverMenu.Load();
            }
        }

        protected override void OnSizeIncrease()
        {
            SetupNewCollection();
        }

        protected override void OnSizeDecrease()
        {
            SetupNewCollection();
        }
    }
}
