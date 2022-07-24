using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Visual
{
    public class ShapeOrderGame : BrainGame
    {
        #region variables

        private int currentShapeIndex;

        #region display time
        private float displayTimeReducer,
                      displayColorsTime,
                      displayTime,
                      minDisplayTime;
        #endregion

        private GameObject[] downShapes;
        private GameButton[] buttons;

        private Sprite[] currentButtonShapeSprites,
                         currentDownShapeSprites,
                         allButtonShapeSprites,
                         allDownShapeSprites;
        #endregion

        #region methods

        protected override void Init()
        {
            base.Init();

            displayTime = 2f;
            minDisplayTime = .2f;
            displayTimeReducer = .1f;

            buttons = Go.GetComponentsInChildren<GameButton>();
            downShapes = new GameObject[4];
            InitializeSprites();

            for (int i = 0; i < downShapes.Length; i++)
            {
                downShapes[i] = GameObjectManager.GetGoInChildren(Go, "Shape" + (i + 1));
            }
        }

        private void InitializeSprites()
        {
            currentButtonShapeSprites = new Sprite[buttons.Length];
            currentDownShapeSprites = new Sprite[downShapes.Length];

            allButtonShapeSprites =
                UnityEngine.Resources.LoadAll<Sprite>("Textures/Games/BrainZ/Visual/ShapeOrder/ShapeOrderOpenSymbols");
            allDownShapeSprites =
                UnityEngine.Resources.LoadAll<Sprite>("Textures/Games/BrainZ/Visual/ShapeOrder/ShapeOrderCloseSymbols");
        }

        private IEnumerator DisplayDownShapes()
        {
            ReduceDisplayTime();
            displayColorsTime = 0;

            while (displayColorsTime < displayTime)
            {
                displayColorsTime += Time.deltaTime;
                yield return null;
            }

            Go.GetComponent<Animation>().Play("ShapeOrderButtonsAppear");
        }

        [UsedImplicitly]
        private void OnButtonsAppear()
        {
            CollidersManager.EnableGameColliders();
            AbstractTime.Instance.Resume();
            AssignButtonsSprites();
            LockButtons(false);
        }

        [UsedImplicitly]
        private void OnDownShapesAppear()
        {
            LockButtons();
            AssignDownShapesSprites();
            AbstractTime.Instance.Pause();
            StartCoroutine(DisplayDownShapes());
        }

        private void Restart()
        {
            GenerateNew();
            currentShapeIndex = 0;

            foreach (var button in buttons)
            {
                button.Reset();
            }
        }

        private void GenerateDownShapes()
        {
            ICollection<int> usedNums = new Collection<int>();

            for (int i = 0; i < currentDownShapeSprites.Length; i++)
            {
                int currentIndexer;

                do
                {
                    currentIndexer = Random.Range(0, buttons.Length);
                } while (usedNums.Contains(currentIndexer));

                usedNums.Add(currentIndexer);

                currentDownShapeSprites[i] = allDownShapeSprites[currentIndexer];
            }
        }

        private void GenerateButtonsShape()
        {
            ICollection<int> usedIndexers = new Collection<int>();

            for (int i = 0; i < currentButtonShapeSprites.Length; i++)
            {
                int random;

                do
                {
                    random = Random.Range(0, buttons.Length);
                } while (usedIndexers.Contains(random));

                usedIndexers.Add(random);

                currentButtonShapeSprites[i] = allButtonShapeSprites[random];
            }
        }

        private void AssignButtonsSprites()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<SpriteRenderer>().sprite = currentButtonShapeSprites[i];
            }
        }

        private void AssignDownShapesSprites()
        {
            for (int i = 0; i < downShapes.Length; i++)
            {
                downShapes[i].GetComponent<SpriteRenderer>().sprite = currentDownShapeSprites[i];
            }
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

        private bool IsCorrect()
        {
            string buttonSpriteName = ClickedBtn.GetComponent<SpriteRenderer>().sprite.name,
                   downSpriteName = currentDownShapeSprites[currentShapeIndex].name;

            buttonSpriteName = buttonSpriteName.Substring(0, buttonSpriteName.LastIndexOf("Open"));
            downSpriteName = downSpriteName.Substring(0, downSpriteName.LastIndexOf("Close"));

            return buttonSpriteName == downSpriteName;
        }

        private bool IsLastOne()
        {
            return currentShapeIndex == downShapes.Length;            
        }

        protected override void ValidateCorrect()
        {
            base.ValidateCorrect();
            currentShapeIndex++;
            ClickedBtn.Lock();
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

            if (IsCorrect())
            {
                ValidateCorrect();

                if (IsLastOne())
                {
                    Restart();                    
                }
            }
            else
            {
                ValidateIncorrect();                
            }
        }

        protected override void GenerateNew()
        {
            RefreshShapes();
            Anim.Play("ShapeOrderDownShapesAppear");
        }

        private void ReduceDisplayTime()
        {
            if (displayTime - displayTimeReducer >= minDisplayTime)
            {
                displayTime -= displayTimeReducer;
            }
            else if(displayTime > minDisplayTime)
            {
                displayTime = minDisplayTime;
            }
        }

        private void RefreshShapes()
        {
            GenerateButtonsShape();
            GenerateDownShapes();
        }
        #endregion
    }
}