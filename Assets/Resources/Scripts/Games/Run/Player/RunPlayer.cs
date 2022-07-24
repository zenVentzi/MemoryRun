using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.Run.Player
{
    public class RunPlayer : MyMono, IRewindable
    {
        #region variables

        private const float Gravity = 400;

        private Vector3 startPos;

        #region rewinding
        private Stack<float> gravityStack,
                             targetAngleStack;

        private Stack<Vector3> rotationStack,
                               positionStack;
        private Stack<int> timesJumpedStack;
        #endregion

        private GameObject currentPlatformLandedOn,
                           prevPlatformLandedOn;

        [SerializeField]
        private GameObject explosion;

        private float jumpHeight,
                      targetAngle;

        private int timesJumped;

        private bool CanJump
        {
            get
            {
#if UNITY_ANDROID
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && timesJumped < 2)
                {
                    return Recording;
                }

                return false;
#endif

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
                return Input.GetButtonDown("Jump") && timesJumped < 2;
#endif
            }
        }

        public bool FrameRecorded { get; set; }

        #endregion

        #region initialization
        [UsedImplicitly]
        private void Start()
        {
            if (RunGame.IsPreview)
            {
                Destroy(Go);
            }
            else
            {
                StartCoroutine(SetGravityBeginning());
            }

            startPos =
                new Vector3(-GamePlayerPrefs.GetFloat("CameraWidth") / 5, -GamePlayerPrefs.GetFloat("CameraHeight") / 3.4f,
                    Tr.position.z);
            Tr.position = startPos;
            StartCoroutine(CheckForDrawback());
            jumpHeight = 1055;
            SubscribeRewindable();
        }

        #endregion

        #region updates

        [UsedImplicitly]
        private void Update()
        {
            if (!Recording) return;

            JumpUpdate();
            UpdateRotation();
            PreventFurtherMovement();
        }

        private IEnumerator SetGravityBeginning()
        {
            RigBody.gravityScale = 0;

            while (!Game.GameInstance.HasGameStarted)
            {
                yield return null;
            }

            RigBody.gravityScale = Gravity;
        }

        [UsedImplicitly]
        private IEnumerator CheckForDrawback()
        {
            while (!Game.GameInstance.HasGameStarted)
            {
                yield return null;
            }

            while (!Game.GameInstance.GameOver)
            {
                if (Tr.position.x < startPos.x - 1)
                {
                    if (Recording)
                    {
                        Explode();
                        continue;
                    }

                    Tr.position = startPos;
                }

                yield return null;
            }
        }

        private void PreventFurtherMovement()
        {
            if (Tr.position.x > startPos.x)
            {
                Tr.position = new Vector2(startPos.x, Tr.position.y);
            }
        }

        private void Explode()
        {
            if (!Game.GameInstance.GameOver)
            {
                EffectsAudioManager.Instance.PlayExplosion();
                Game.GameInstance.GameOver = true;
                var expl = Instantiate(explosion, Tr.position, Quaternion.identity) as GameObject;
                expl.transform.parent = Tr.parent;
                ReduceScale();
                Invoke("Explode", Time.deltaTime * 30);
                return;
            }

            GameOverMenu.Load();
        }

        private void ReduceScale()
        {
            if (Tr.localScale.x <= 0.01f) return;

            Tr.localScale /= 10;
            Invoke("ReduceScale", 0.1f);
        }

        private void JumpUpdate()
        {
            if (!CanJump) return;

            int framesLatency = timesJumped == 0 ? 4 : 0;
            Invoke("SetNewJumpAngle", Time.deltaTime * framesLatency);

            float y = jumpHeight * (RigBody.gravityScale / Mathf.Abs(RigBody.gravityScale));

            RigBody.velocity = new Vector2(0, y);
            timesJumped++;
        }

        [UsedImplicitly]
        private void SetNewJumpAngle()
        {
            Tr.eulerAngles = new Vector3(0, 0, (int)Tr.eulerAngles.z);
            targetAngle += 90;
            targetAngle = targetAngle == 360 ? 0 : targetAngle;
        }

        private void UpdateRotation()
        {
            if (Math.Abs((int)Tr.eulerAngles.z - targetAngle) > 0.0001f)
            {
                Tr.eulerAngles = new Vector3(0, 0, Tr.eulerAngles.z + 18);
            }
        }

        private void SetGravityScale(Collider2D colliderr)
        {
            if (Colider.bounds.max.y > colliderr.bounds.max.y)
            {
                RigBody.gravityScale = 400;
            }
            else
            {
                RigBody.gravityScale = -400;
            }
        }

        private bool IsOutOfBounds(Collider2D otherCollider)
        {
            return otherCollider.name.Contains("Limit");
        }

        private bool LandedOnNewPlatform()
        {
            return prevPlatformLandedOn != null && prevPlatformLandedOn.name.Contains("Platform") && currentPlatformLandedOn != prevPlatformLandedOn;
        }

        #endregion

        [UsedImplicitly]
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!Recording) return;//add menu if unexpeced behaviour

            if (IsOutOfBounds(collision.collider) && !Game.GameInstance.GameOver)
            {
                Explode();
                return;
            }

            prevPlatformLandedOn = currentPlatformLandedOn;
            currentPlatformLandedOn = collision.gameObject;

            if (LandedOnNewPlatform() && !Game.GameInstance.GameOver)
            {
                var runScore = Game.GameInstance.ScoreRef as RunGameScore;
                runScore.Add();
            }

            timesJumped = 0;
            SetGravityScale(collision.collider);
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            UnscribleRewindable();
        }
        public bool OnHold
        {
            get { return positionStack.Count == 0; }
        }

        public bool Recording { get; set; }

        public void SubscribeRewindable()
        {
            positionStack = new Stack<Vector3>();
            rotationStack = new Stack<Vector3>();
            targetAngleStack = new Stack<float>();
            timesJumpedStack = new Stack<int>();
            gravityStack = new Stack<float>();

            Game.GameInstance.BroadcastMessage("AddToCollection", this);
        }

        public void Rewind()
        {
            Tr.position = positionStack.Pop();
            Tr.eulerAngles = rotationStack.Pop();
            targetAngle = targetAngleStack.Pop();
            timesJumped = timesJumpedStack.Pop();
            RigBody.gravityScale = gravityStack.Pop();
        }

        public void Record()
        {
            gravityStack.Push(OnHold ? Gravity : RigBody.gravityScale);
            rotationStack.Push(Tr.eulerAngles);
            targetAngleStack.Push(targetAngle);
            timesJumpedStack.Push(timesJumped);
            positionStack.Push(Tr.position);
        }

        public void UnscribleRewindable()
        {
            if (Game.GameInstance != null) Game.GameInstance.BroadcastMessage("RemoveFromCollection", this);
        }
    }
}