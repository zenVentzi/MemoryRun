using System.Linq;
using Assets.Resources.Scripts.General.Managers;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.General
{
    public class InputManager : MyMono
    {
        #region variables

        private AbstractButton[] escapeHandlers;

        private static Vector2 previousPos;
        private static GameObject touchedButton;

        [UsedImplicitly] private static bool isBeingTouched,
                                             restartNextFrame;

        private static GameObject firstTappedGo,
                                  firstHoldedDownButton;

        #endregion

        [UsedImplicitly]
        private void Update()
        {
            HandleEscapeKey();
            UpdateInputState();
            UpdateTouchEvents();
        }

        private void HandleEscapeKey()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                escapeHandlers = FindObjectsOfType<AbstractButton>();

                foreach (var handler in escapeHandlers.OfType<IHandleEscapeKey>())
                {
                    handler.HandleEscapeKey();
                }
            }
        }

        private void UpdateTouchEvents()
        {
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
                {
                    touchedButton = SortingLayerManager.GetTopmostBelowMouse();

                    if (touchedButton != null)
                        touchedButton.SendMessage("OnnMouseDown");
                }
                else if (Input.GetTouch(0).phase.Equals(TouchPhase.Ended) && touchedButton != null)
                {
                    touchedButton.SendMessage("OnnMouseUp");
                    touchedButton = null;
                }
            }
#elif UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                touchedButton = SortingLayerManager.GetTopmostBelowMouse();

                if (touchedButton != null)
                    touchedButton.SendMessage("OnnMouseDown", SendMessageOptions.DontRequireReceiver);
            }
            else if (Input.GetMouseButtonUp(0) && touchedButton != null)
            {
                touchedButton.SendMessage("OnnMouseUp", SendMessageOptions.DontRequireReceiver);
                touchedButton = null;
            }
#endif
        }

        private static void UpdateInputState()
        {
            if (HasJustBegan())
            {
                OnTouchBegin();
            }
            else if (JustReleased())
            {
                OnTouchFinish();                                
            }
        }

        private static void OnTouchBegin()
        {
            isBeingTouched = true;
            firstHoldedDownButton = SortingLayerManager.GetTopmostBelowMouse();
        }

        private static void OnTouchFinish()
        {
            firstHoldedDownButton = null;
            isBeingTouched = false;
        }

        [UsedImplicitly]
        private void LateUpdate()
        {
            if (restartNextFrame)
            {
                firstTappedGo = null;
                restartNextFrame = false;
            }

            UpdatePreviousPos();
        }

        private static void UpdatePreviousPos()
        {
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                previousPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else
            {
                previousPos = Vector2.zero;
            }
#endif

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            previousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif
        }

        public static bool JustReleased()
        {
#if UNITY_ANDROID
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
#endif


#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            return Input.GetMouseButtonUp(0);
#endif
        }

        public static bool IsClicked(GameObject go)
        {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            if (go != null && (Input.GetMouseButtonDown(0) && SortingLayerManager.IsTopmost(go)))
            {
                firstTappedGo = go;
            }
            else if (go != null &&
                     (Input.GetMouseButtonUp(0) && firstTappedGo == go && SortingLayerManager.IsTopmost(go)))
            {
                restartNextFrame = true;
                return true;
            }
            else if ((Input.GetMouseButtonUp(0) && go == firstTappedGo))
            {
                firstTappedGo = null;
            }
#endif

#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && SortingLayerManager.IsTopmost(go))
                {
                    firstTappedGo = go;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended && firstTappedGo == go &&
                         SortingLayerManager.IsTopmost(go))
                {
                    restartNextFrame = true;
                    return true;
                }
                else if ((Input.GetTouch(0).phase == TouchPhase.Ended && go == firstTappedGo) && restartNextFrame)
                {
                    firstTappedGo = null;
                    restartNextFrame = false;
                }
            }
#endif
            return false;
        }

        public static bool IsTouchHeld()
        {
#if UNITY_ANDROID
            return isBeingTouched;
#endif

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            return Input.GetMouseButton(0);
#endif
        }

        public static bool IsTouchHeldOver(GameObject go)
        {
            return IsTouchHeld() && SortingLayerManager.IsTopmost(go);
        }

        public static bool IsTouchHeldFromBeginningOver(GameObject go)
        {
            return firstHoldedDownButton == go;
        }

        private static bool HasJustBegan()
        {
#if UNITY_ANDROID
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#endif
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            return Input.GetMouseButtonDown(0);
#endif
        }

        public static Vector2 GetDeltaPos()
        {
            Vector2 currentPos;
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                currentPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                if (previousPos == Vector2.zero)
                {
                    previousPos = currentPos;
                }

                return currentPos - previousPos;
            }

            return Vector2.zero;
#endif

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //if (!Input.GetMouseButton(0) || previousPos == currentPos) return Vector2.zero;
            //Vector2 deltaPos = currentPos - previousPos;
            //return deltaPos;

            if (!Input.GetMouseButton(0)) return Vector2.zero;
            var deltaPos = currentPos - previousPos;
            return deltaPos;
#endif
        }

        [UsedImplicitly]
        private void OnMouseDown()
        {
            
        }
    }
}