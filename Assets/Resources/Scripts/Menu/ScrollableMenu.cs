using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public class ScrollableMenu : MyMono
    {
        private const float ScrollSpeed = 2f;
        private static Stack<ScrollableMenu> deactivatedMenus;

        private bool buttonReleased,
                     velocityDown;

        private float currentBottomYpos,
            currentTopYpos,
            distanceBeforeButtonLock,
            buttonLockDistance,
            releaseVelocity;

        private float Velocity
        {
            get
            {
                return InputManager.GetDeltaPos().y * ScrollSpeed + releaseVelocity;
            }
        }

        private DateTime touchDownTime;

        [UsedImplicitly]
        protected virtual void Awake()
        {
            if (deactivatedMenus == null) deactivatedMenus = new Stack<ScrollableMenu>();
        }

        [UsedImplicitly]
        protected virtual void Start()
        {
            buttonLockDistance = 20;
            RepositionCenter();
        }

        private void RepositionCenter()
        {
            var difference = SpriteRend.bounds.max.y - SetUpSettings.TopBottomY.Key;

            Tr.position = new Vector3(Tr.position.x, Tr.position.y - difference,
                Tr.position.z);
        }

        [UsedImplicitly]
        private void Update()
        {
            SetButtonDownTime();

            if (Math.Abs(Velocity) > .1f)
            {
                if (CanScroll())
                {
                    
                }
            }
            else if (InputManager.JustReleased())
            {
                //StartCoroutine(ReleaseVelocityManager());                
            }

            if (CanScroll())
            {
                if (!buttonReleased && distanceBeforeButtonLock > buttonLockDistance)
                {
                    buttonReleased = true;
                    distanceBeforeButtonLock = 0;
                    BroadcastMessage("ReleaseOnScroll");
                }

                Scroll();
            }
            else if (buttonReleased)
            {
                buttonReleased = false;
            }
        }

        private IEnumerator ReleaseVelocityManager()
        {
            var timeTakenToRelease = (float)(DateTime.Now - touchDownTime).TotalMilliseconds;
            Debug.Log(timeTakenToRelease);
            while (timeTakenToRelease < 1000)
            {
                if (velocityDown)
                {
                    releaseVelocity = 1000 / timeTakenToRelease;
                }
                else
                {
                    releaseVelocity = -1000 / timeTakenToRelease;
                }

                timeTakenToRelease += Time.deltaTime * 1000;
                yield return null;
            }

            releaseVelocity = 0;
        }

        [UsedImplicitly]
        private void Deactivate()
        {
            deactivatedMenus.Push(this);
            enabled = false;
        }

        private void Scroll()
        {
            distanceBeforeButtonLock += Mathf.Abs(Velocity);
            transform.position = new Vector3(transform.position.x, transform.position.y + Velocity,
                transform.position.z);
        }

        private void SetButtonDownTime()
        {
#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                touchDownTime = DateTime.Now;
            }
#elif UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                touchDownTime = DateTime.Now;
            }
#endif
        }

        private bool CanScroll()
        {
            currentTopYpos = transform.position.y + SpriteRend.bounds.extents.y;
            currentBottomYpos = transform.position.y - SpriteRend.bounds.extents.y;

            return (currentTopYpos + Velocity > SetUpSettings.TopBottomY.Key &&
                    currentBottomYpos + Velocity < SetUpSettings.TopBottomY.Value);
        }

        public static void ActivatePrevious()
        {
            if (deactivatedMenus.Count > 0)
            {
                deactivatedMenus.Pop().GetComponent<ScrollableMenu>().enabled = true;
            }
        }
    }
}