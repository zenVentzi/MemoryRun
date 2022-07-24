using Assets.Resources.Scripts.Menu.RunTimeMenu;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts.Games.BrainZ.Visual
{
    public class DirectionsGame : BrainGame
    {
        #region variables
        private const int MinSlidedDistance = 30;

        private GameObject arrowGo;
        private Vector2 endTouchPos;
        private Vector2 firstClickPos;

        private int redInARow,
                    greenInARow,
                    lastRotationChoice;

        private enum Directions
        {
            Left,
            Right,
            Up,
            Down,
            None
        }

        #endregion

        #region methods

        private Directions GetSlidedDirection()
        {
            Vector2 difference;
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            if (Input.GetMouseButtonDown(0))
            {
                firstClickPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endTouchPos = Input.mousePosition;
            }
            else if (!Input.GetMouseButton(0))
            {
                firstClickPos = Vector2.zero;
                endTouchPos = Vector2.zero;
            }

            if (!(Vector2.Distance(firstClickPos, endTouchPos) > MinSlidedDistance) || endTouchPos == Vector2.zero)
                return Directions.None;

            difference = endTouchPos - firstClickPos;

            if (Mathf.Abs(difference.x) >= Mathf.Abs(difference.y))
            {
                return difference.x > 0 ? Directions.Right : Directions.Left;
            }

            return difference.y > 0 ? Directions.Up : Directions.Down;

#endif

#if UNITY_ANDROID
            if (Input.touchCount == 1)
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        firstClickPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        break;
                    case TouchPhase.Ended:
                        endTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        break;
                }

                if (Vector2.Distance(firstClickPos, endTouchPos) > MinSlidedDistance && endTouchPos != Vector2.zero)
                {
                    difference = endTouchPos - firstClickPos;

                    if (Mathf.Abs(difference.x) >= Mathf.Abs(difference.y))
                    {
                        return difference.x > 0 ? Directions.Right : Directions.Left;
                    }

                    return difference.y > 0 ? Directions.Up : Directions.Down;
                }
            }
            else if (Input.touchCount == 0 && firstClickPos != Vector2.zero)
            {
                firstClickPos = Vector2.zero;
                endTouchPos = Vector2.zero;
            }

            return Directions.None;
#endif
        }

        private Directions GetCorrectDirection()
        {
            if (SpriteRend.sprite.name == "GreenBackground")
            {
                switch ((int)arrowGo.transform.eulerAngles.z)
                {
                    case 0:
                        return Directions.Right;
                    case 90:
                        return Directions.Up;
                    case 180:
                        return Directions.Left;
                    case 270:
                        return Directions.Down;
                }
            }
            else
            {
                switch ((int)arrowGo.transform.eulerAngles.z)
                {
                    case 0:
                        return Directions.Left;
                    case 90:
                        return Directions.Down;
                    case 180:
                        return Directions.Right;
                    case 270:
                        return Directions.Up;
                }
            }

            return Directions.None;
        }

        private void MakeArrowVisible()
        {
            var isFirstGeneration = arrowGo.GetComponent<SpriteRenderer>().color.a < 1;

            if (isFirstGeneration)
                arrowGo.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        private void GenerateNewBackground()
        {
            GetComponent<SpriteRenderer>().sprite =
                UnityEngine.Resources.Load<Sprite>(GetBackgroundChoice() == 1
                    ? "Textures/Games/BrainZ/Visual/GreenBackground"
                    : "Textures/Games/BrainZ/Visual/RedBackground");
        }

        private bool IsCorrect()
        {
            return GetSlidedDirection() == GetCorrectDirection();
        }

        protected override void Init()
        {
            base.Init();
            lastRotationChoice = -1;
            //CorrectInARowAddition = 1;
            arrowGo = Tr.FindChild("Arrow").gameObject;
        }

        protected override void GenerateNew()
        {
            MakeArrowVisible();
            GenerateNewBackground();
            RotateArrow();
        }

        private void RotateArrow()
        {
            var rotationChoice = Random.Range(0, 4);
            var sameBackground = (greenInARow > 1 || redInARow > 1);

            while (sameBackground && rotationChoice == lastRotationChoice)
            {
                rotationChoice = Random.Range(0, 4);
            }

            lastRotationChoice = rotationChoice;

            switch (rotationChoice)
            {
                case 0:
                    arrowGo.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case 1:
                    arrowGo.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case 2:
                    arrowGo.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 3:
                    arrowGo.transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
            }
        }

        private int GetBackgroundChoice()
        {
            int choice;
            var maxInARow = Random.Range(2, 4);

            do
            {
                choice = Random.Range(0, 2);

                if (choice == 1)
                {
                    redInARow = 0;
                    greenInARow++;
                }
                else
                {
                    greenInARow = 0;
                    redInARow++;
                }
            } while (greenInARow > maxInARow || redInARow > maxInARow);

            return choice;
        }

        [UsedImplicitly]
        private void Update()
        {
            if (PauseButton.Instance.GamePaused || GameOver) return;

            if (GetSlidedDirection() == Directions.None) return;

            if (IsCorrect())
            {
                ValidateCorrect();
            }
            else
            {
                ValidateIncorrect();
            }

            if(!GameOver)
                GenerateNew();
        }
        #endregion
    }
}