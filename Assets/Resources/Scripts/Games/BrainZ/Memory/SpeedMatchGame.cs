using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Memory
{
    public class SpeedMatchGame : BrainGame
    {
        #region variables
        private const int MaxMatchNoMatchInARow = 2;

        private List<Sprite> allSprites;

        private int matchesInARow;

        private Vector2 leftButtonPos,
                        rightButtonPos;

        private GameButton match,
                           noMatch;

        private Sprite currentSprite;
        private List<Sprite> currentSprites;
        private int noMatchesInARow;

        private GameObject picGo;
        private Sprite prevSprite;
        private int roundsWithSamePics;

        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();
            allSprites =
                UnityEngine.Resources.LoadAll<Sprite>("Textures/Games/BrainZ/Memory/SpeedMatch/SpeedMatchPics").ToList();

            picGo = GameObjectManager.GetGoInChildren(Go, "Pic");
            match = Tr.FindChild("Match").GetComponent<GameButton>();
            noMatch = Tr.FindChild("NoMatch").GetComponent<GameButton>();
            leftButtonPos = match.Tr.localPosition;
            rightButtonPos = noMatch.Tr.localPosition;
            StartCoroutine(FirstGeneration());
        }

        private void SwapButtons()
        {
            var matchBtnOnTheLeftSide = Random.Range(0, 2) == 1;

            if (matchBtnOnTheLeftSide)
            {
                match.Tr.localPosition = leftButtonPos;
                noMatch.Tr.localPosition = rightButtonPos;
            }
            else
            {
                match.Tr.localPosition = rightButtonPos;
                noMatch.Tr.localPosition = leftButtonPos;
            }
        }


        private IEnumerator FirstGeneration()
        {
            while (!GameIntro.HasIntroBeenShown)
            {
                yield return null;
            }

            RefreshPics();
            GenerateNew();
        }

        private Sprite GetSprite(bool matchToPrev)
        {
            if (matchToPrev)
            {
                Sprite sprite;

                while ((sprite = currentSprites.FirstOrDefault(spr => spr == prevSprite)) == null)
                    RefreshPics();

                matchesInARow++;
                return sprite;
            }

            noMatchesInARow++;
            return currentSprites.FirstOrDefault(spr => spr != prevSprite);
        }

        private void RefreshPics()
        {
            currentSprites = new List<Sprite>();
            int startIndex = Random.Range(0, allSprites.Count - 2);

            for (int i = startIndex; i < startIndex + 2; i++)
            {
                currentSprites.Add(allSprites[i]);
            }

            roundsWithSamePics = Random.Range(1, 7);
        }

        private Sprite GenerateCurrent()
        {
            if (matchesInARow < MaxMatchNoMatchInARow && noMatchesInARow < MaxMatchNoMatchInARow)
            {
                return GetSprite(matchToPrev: Random.Range(0, 2) == 1 && prevSprite != null);
            }
            if (matchesInARow >= MaxMatchNoMatchInARow)
            {
                matchesInARow = 0;
                return GetSprite(false);
            }

            noMatchesInARow = 0;
            return GetSprite(true);
        }

        protected virtual bool IsCorrect()
        {
            return (ClickedBtn.Go.name == "Match" && currentSprite == prevSprite) ||
                   (ClickedBtn.Go.name == "NoMatch" && currentSprite != prevSprite);
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

            if (!GameOver)
                GenerateNew();
        }

        protected override void GenerateNew()
        {
            SwapButtons();
            prevSprite = currentSprite;
            currentSprite = GenerateCurrent();
            picGo.GetComponent<SpriteRenderer>().sprite = currentSprite;

            if (--roundsWithSamePics == 0)
            {
                RefreshPics();                
            }
        }
        #endregion
    }
}