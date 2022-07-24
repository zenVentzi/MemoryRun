using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.Menu;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.BrainZ.Logic
{
    public class LightsOutGame : SizeDependentGame
    {
        private int numOfLightsOn,
                    minNumOfLightsOn,
                    maxNumOfLightsOn;

        private LightsOutButton[] buttons;

        private bool Solved
        {
            get
            {
                return !buttons.Any(b => b.LightOn);
            }
        }
        protected override string SizeTxt
        {
            get { return base.SizeTxt; }
            set { base.SizeTxt = string.Format("{0}x{0}", value); }
        }

        private GameObject GetGridPrefab()
        {
            var path = string.Format("Prefabs/BrainZ/LightsOut/Grid{0}x{0}", Size);
            var gridPrefab = Instantiate(UnityEngine.Resources.Load<GameObject>(path));
            gridPrefab.transform.parent = Tr;
            gridPrefab.transform.localScale = Vector2.one;
            gridPrefab.name = gridPrefab.name.Substring(0, gridPrefab.name.LastIndexOf("(Clone)"));
            return gridPrefab;
        }

        private void FillNeighbours()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i - 1 >= 0 && Math.Abs(buttons[i - 1].Tr.position.y - buttons[i].Tr.position.y) <= 1)
                    buttons[i].Neighbours.Add(buttons[i - 1]);
                if (i + 1 < buttons.Length && Math.Abs(buttons[i + 1].Tr.position.y - buttons[i].Tr.position.y) <= 1)
                    buttons[i].Neighbours.Add(buttons[i + 1]);
                if (i - Size >= 0)
                    buttons[i].Neighbours.Add(buttons[i - Size]);
                if (i + Size < buttons.Length)
                    buttons[i].Neighbours.Add(buttons[i + Size]);
            }
        }

        private void TurnLightsOnBeginning()
        {
            minNumOfLightsOn = buttons.Length / Size;
            maxNumOfLightsOn = buttons.Length - minNumOfLightsOn;
            numOfLightsOn = Random.Range(minNumOfLightsOn, maxNumOfLightsOn + 1);

            var usedIndexes = new List<int>();
            var count = 0;

            while (count <= numOfLightsOn)
            {
                int index;

                do
                {
                    index = Random.Range(0, buttons.Length);
                } while (usedIndexes.Contains(index));

                buttons[index].TurnLightOn();
                usedIndexes.Add(index);
                count++;
            }
        }

        protected override void Init()
        {
            base.Init();
            SetupNewCollection();
            AbstractTime.Instance.Pause();
            CollidersManager.EnableGameColliders();
        }

        private void SetupNewCollection()
        {
            RemoveOldCollection();
            var prefab = GetGridPrefab();
            buttons = prefab.GetComponentsInChildren<LightsOutButton>();
            FillNeighbours();
            TurnLightsOnBeginning();
        }

        private void RemoveOldCollection()
        {
            if (buttons != null)
                Destroy(buttons[0].Tr.parent.gameObject);
        }

        protected override void OnGameBegin()
        {
            base.OnGameBegin();
            DisableModifyButtons();
            AbstractTime.Instance.Resume();
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            if (!(ClickedBtn is LightsOutButton)) return;

            if (!HasGameStarted)
                HasGameStarted = true;

            Moves++;
            if (Solved)
                GameOverMenu.Load();
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
