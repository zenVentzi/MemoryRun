using System.Collections.Generic;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Memory
{
    public class FindPairsGame : BrainGame
    {
        #region variables

        private Sprite[] allSprites;
        private List<GameButton> clickedButtons;
        private Sprite closeBox;
        private KeyValuePair<GameButton, GameObject>[,] buttonToPicMap;
        private Sprite openBox;
        private GameButton previouslyClicked;

        private Sprite[] sprites;

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            openBox = UnityEngine.Resources.Load<Sprite>("Textures/Games/BrainZ/Memory/FindPairs/FindPairsOpenBox");
            closeBox = UnityEngine.Resources.Load<Sprite>("Textures/Games/BrainZ/Memory/FindPairs/FindPairsCloseBox");
            buttonToPicMap = new KeyValuePair<GameButton, GameObject>[3, 4];
            var buttons = Go.GetComponentsInChildren<GameButton>();
            var pics = new GameObject[buttons.Length];

            for (int i = 0; i < buttons.Length; i++)
            {
                pics[i] = GameObjectManager.GetGoInChildren(Go, (i + 1) + "pic");
            }


            int indexer = 0;

            for (int i = 0; i < buttonToPicMap.GetLength(0); i++)
            {
                for (int j = 0; j < buttonToPicMap.GetLength(1); j++)
                {
                    buttonToPicMap[i, j] = new KeyValuePair<GameButton, GameObject>(buttons[indexer], pics[indexer]);
                    indexer++;
                }
            }

            GenerateNew();
        }

        private void ShowCurrentPic()
        {
            foreach (var item in buttonToPicMap)
            {
                if (item.Key != ClickedBtn) continue;
                item.Key.GetComponent<SpriteRenderer>().sprite = openBox;
                Color32 color = item.Value.GetComponent<SpriteRenderer>().material.color;
                color.a = 1;
                item.Value.GetComponent<SpriteRenderer>().material.color = color;
            }
        }

        private void HidePreviousPic()
        {
            foreach (var item in buttonToPicMap)
            {
                if (item.Key != previouslyClicked) continue;

                item.Key.GetComponent<SpriteRenderer>().sprite = closeBox;
                Color32 color = item.Value.GetComponent<SpriteRenderer>().material.color;
                color.a = 0;
                item.Value.GetComponent<SpriteRenderer>().material.color = color;
            }
        }

        private float GetPicAlpha(GameObject pic)
        {
            return pic.GetComponent<SpriteRenderer>().material.color.a;
        }

        private void UpdateAlpha()
        {
            foreach (var item in buttonToPicMap)
            {
                if (!(GetPicAlpha(item.Value) > 0) || !(GetPicAlpha(item.Value) < 1))
                    continue;

                Color color = item.Value.GetComponent<SpriteRenderer>().material.color;
                color.a += 0.1f;
                item.Value.GetComponent<SpriteRenderer>().material.color = color;
            }
        }

        private Sprite GetPicFromButton(GameButton button)
        {
            foreach (var pair in buttonToPicMap)
            {
                if (pair.Key == button)
                {
                    return pair.Value.GetComponent<SpriteRenderer>().sprite;
                }
            }

            return null;
        }

        private bool IsLastClicked()
        {
            return clickedButtons.Count == 12;
        }

        protected virtual bool IsCorrect()
        {
            return previouslyClicked != null &&
                   (GetPicFromButton(previouslyClicked).name == GetPicFromButton(ClickedBtn).name);
        }

        protected virtual bool IsIncorrect()
        {
            return previouslyClicked != null &&
                   (GetPicFromButton(previouslyClicked).name != GetPicFromButton(ClickedBtn).name);
        }

        protected override void ValidateCorrect()
        {
            ShowCurrentPic();
            base.ValidateCorrect();
            clickedButtons.Add(ClickedBtn);
            previouslyClicked = null;

            if (IsLastClicked())
            {
                GenerateNew();
            }
            else
            {
                //AudioManager.Play("correct");
            }
        }

        protected override void ValidateIncorrect()
        {
            ShowCurrentPic();
            HidePreviousPic();
            previouslyClicked = ClickedBtn;
            clickedButtons.RemoveAt(clickedButtons.Count - 1);
            clickedButtons.Add(ClickedBtn);

            //AudioManager.Play("incorrect");
        }

        private void ValidateClick()
        {
            previouslyClicked = ClickedBtn;
            clickedButtons.Add(ClickedBtn);
            ShowCurrentPic();
        }

        protected override void OnGameButtonClick(GameButton clickedButton)
        {
            base.OnGameButtonClick(clickedButton);

            if (clickedButtons.Contains(ClickedBtn)) return;

            if (IsCorrect())
            {
                ValidateCorrect();
            }
            else if (IsIncorrect())
            {
                ValidateIncorrect();
            }
            else
            {
                ValidateClick();
            }
        }

        protected override void GenerateNew()
        {
            allSprites = UnityEngine.Resources.LoadAll<Sprite>("Textures/Games/BrainZ/Memory/FindPairs/FindPairsPics");
            var usedSprites = new Dictionary<int, int>();
            clickedButtons = new List<GameButton>();
            sprites = new Sprite[6];
            int indexer;

            #region initializing the spirtes which are going to be used

            for (int i = 0; i < sprites.Length; i++)
            {
                do
                {
                    indexer = Random.Range(0, allSprites.Length);

                    if (!usedSprites.ContainsKey(indexer))
                    {
                        usedSprites.Add(indexer, 1);
                    }
                    else
                    {
                        usedSprites[indexer]++;
                    }

                } while (usedSprites[indexer] > 1);

                sprites[i] = allSprites[indexer];
            }

            #endregion

            usedSprites.Clear();

            #region adding the sprites randomly to the GOs

            for (int i = 0; i < buttonToPicMap.GetLength(0); i++)
            {
                for (int j = 0; j < buttonToPicMap.GetLength(1); j++)
                {
                    do
                    {
                        indexer = Random.Range(0, sprites.Length);
                        if (!usedSprites.ContainsKey(indexer))
                        {
                            usedSprites.Add(indexer, 1);
                        }
                        else
                        {
                            usedSprites[indexer]++;
                        }
                    } while (usedSprites[indexer] > 2);

                    buttonToPicMap[i, j].Value.GetComponent<SpriteRenderer>().sprite = sprites[indexer];
                    Color32 color = buttonToPicMap[i, j].Value.GetComponent<SpriteRenderer>().material.color;
                    color.a = 0;
                    buttonToPicMap[i, j].Value.GetComponent<SpriteRenderer>().material.color = color;
                    buttonToPicMap[i, j].Key.GetComponent<SpriteRenderer>().sprite = closeBox;
                }
            }

            #endregion
        }

        #region unity

        [UsedImplicitly]
        private void Update()
        {
            if (!IsGameRunning) return;
            UpdateAlpha();
        }

        #endregion

        #endregion
    }
}