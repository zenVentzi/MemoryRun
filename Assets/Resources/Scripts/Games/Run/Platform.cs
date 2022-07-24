using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using JetBrains.Annotations;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Assets.Resources.Scripts.Games.Run
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public enum RotationType
    {
        Once,
        Loop,
        PingPong
    }

    public class Platform : ScrollingRunObj
    {
        #region variables
        private float angleToApproach;
        private GameObject normalizedGo;
        private Vector3 startAngle;

        private Stack<float> rotSpeeds,
                             angleToApproachStack,
                             firstInvokeDelayTimeStack;

        private Stack<Vector3> rotAngles;

        public Direction Dir;
        public RotationType TypeOfRotation;

        public bool IsClockwise,
                    IsRotatable,
                    IsMovable;

        public float Offset1,
                     Offset2,
                     RotSpeed,
                     MoveSpeed,
                     DegreesOffset,
                     FirstInvokeDelayTime;
        #endregion

        //private bool MovableInitialized
        //{
        //    get { return normalizedGo != null; }
        //}

        private bool RotatableInitialized
        {
            get { return rotSpeeds != null; }
        }

        private bool HasReachedOffset
        {
            get
            {
                float distance = Vector2.Distance(Tr.position, normalizedGo.transform.position);

                if (IsMovingTowardsOffset1)
                {
                    return distance >= Offset1 && IsInThirdQuadrant;
                }

                return distance >= Offset2 && IsInFirstQuadrant;
            }
        }

        private bool IsInThirdQuadrant
        {
            get
            {
                if (Dir == Direction.Horizontal)
                {
                    return Tr.position.x <= normalizedGo.transform.position.x;
                }

                return Tr.position.y <= normalizedGo.transform.position.y;
            }
        }

        private bool IsInFirstQuadrant
        {
            get
            {
                if (Dir == Direction.Horizontal)
                {
                    return Tr.position.x >= normalizedGo.transform.position.x;
                }

                return Tr.position.y >= normalizedGo.transform.position.y;
            }
        }

        private bool IsMovingTowardsOffset1
        {
            get
            {
                if (Dir == Direction.Horizontal)
                {
                    return Tr.GetComponent<Rigidbody2D>().velocity.x < -Game.GetGame<RunGame>().GameSpeed * SpeedMultiplier;
                }

                return Tr.GetComponent<Rigidbody2D>().velocity.y < 0;
            }
        }

        protected override void Start()
        {
            base.Start();

            if (IsMovable && (Offset1 != 0 || Offset2 != 0))
            {
                InitializeNormalizedGo();
                StartCoroutine(Move());
            }
        }

        [UsedImplicitly]
        private void OnBecameVisible()
        {
            if (RotatableInitialized || !IsRotatable) return;

            rotSpeeds = new Stack<float>();
            rotAngles = new Stack<Vector3>();
            angleToApproachStack = new Stack<float>();
            firstInvokeDelayTimeStack = new Stack<float>();
            StartCoroutine(Rotate());
        }

        private void InitializeNormalizedGo()
        {
            normalizedGo = new GameObject {name = "NormalizedHiddenPlatform"};
            normalizedGo.transform.parent = Tr.parent;
            normalizedGo.transform.position = Tr.position;
            normalizedGo.AddComponent<Rigidbody2D>();
            normalizedGo.GetComponent<Rigidbody2D>().isKinematic = true;
            normalizedGo.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Extrapolate;
            normalizedGo.AddComponent<Platform>();
            normalizedGo.transform.SetAsFirstSibling();
        }

        private IEnumerator Rotate()
        {
            while (!Game.GameInstance.IsGameRunning)
            {
                yield return null;
            }

            SetRotationalValuesBeginning();

            switch (TypeOfRotation)
            {
                case RotationType.Once:
                    StartCoroutine(RotateOnce());
                    break;
                case RotationType.Loop:
                    StartCoroutine(RotateLoop());
                    break;
                case RotationType.PingPong:
                    StartCoroutine(RotatePingPong());
                    break;
            }
        }

        private void SetRotationalValuesBeginning()
        {
            startAngle = Tr.eulerAngles;

            if (IsClockwise)
            {
                RotSpeed = -RotSpeed;

                if (TypeOfRotation == RotationType.Loop) return;
                DegreesOffset = (360 - DegreesOffset) + startAngle.z;
                angleToApproach = DegreesOffset;
            }
            else if(TypeOfRotation != RotationType.Loop)
            {
                DegreesOffset += startAngle.z;
                DegreesOffset %= 360;
                angleToApproach = DegreesOffset;
            }
        }

        [UsedImplicitly]
        private IEnumerator RotateOnce()
        {
            float delay = 0;

            while (!DelayFinished(ref delay))
            {
                yield return null;
            }

            while (!Game.GameInstance.GameOver && !BypassedOrEqualToOffset() && Recording)
            {
                var nextAngle = (IsClockwise
                    ? (Tr.eulerAngles.z + RotSpeed < angleToApproach && Tr.eulerAngles != startAngle ? new Vector3(0, 0, angleToApproach) : new Vector3(0, 0, Tr.eulerAngles.z + RotSpeed))                                                          
                    : (Tr.eulerAngles.z + RotSpeed > angleToApproach && Tr.eulerAngles != startAngle ? new Vector3(0, 0, angleToApproach) : new Vector3(0, 0, Tr.eulerAngles.z + RotSpeed)));


                Tr.eulerAngles = nextAngle;
                yield return null;
            }

            while ((!SpriteRend.isVisible || !Recording || BypassedOrEqualToOffset()) && !Game.GameInstance.GameOver)
            {
                yield return null;
            }

            if (!Game.GameInstance.GameOver)
                StartCoroutine(RotateOnce());                          
        }

        private bool BypassedOrEqualToOffset()
        {
            return (IsClockwise ? Tr.eulerAngles.z <= angleToApproach && Tr.eulerAngles.z != startAngle.z : Tr.eulerAngles.z >= angleToApproach);
        }

        private bool DelayFinished(ref float delay)
        {
            if (delay >= FirstInvokeDelayTime)
                return true;

            delay += Time.deltaTime;
            return false;
        }

        [UsedImplicitly]
        private IEnumerator RotateLoop()
        {
            float delay = 0;

            while (!DelayFinished(ref delay))
            {
                yield return null;
            }

            while (!Game.GameInstance.GameOver && Recording && SpriteRend.isVisible)
            {
                Tr.eulerAngles = new Vector3(0, 0, Tr.eulerAngles.z + RotSpeed);
                yield return null;
            }

            while (!Game.GameInstance.GameOver && (!Recording || !SpriteRend.isVisible))
            {
                yield return null;
            }

            if (!Game.GameInstance.GameOver)
                StartCoroutine(RotateLoop());
        }

        private IEnumerator RotatePingPong()
        {
            float delay = 0;

            while (!DelayFinished(ref delay))
            {
                yield return null;
            }

            while (!Game.GameInstance.GameOver && Recording && SpriteRend.isVisible)
            {
                if (BypassedOrEqualToOffset())
                {
                    Tr.eulerAngles = new Vector3(0, 0, angleToApproach);
                    IsClockwise = !IsClockwise;
                    RotSpeed = -RotSpeed;
                    ToggleCurrentOffsetAngle();
                }
                else
                {
                    Tr.eulerAngles = new Vector3(0, 0, Tr.eulerAngles.z + RotSpeed);
                }

                yield return null;
            }

            while (!Game.GameInstance.GameOver && (!Recording || !SpriteRend.isVisible))
            {
                yield return null;
            }

            if (!Game.GameInstance.GameOver)
                StartCoroutine(RotatePingPong());
        }

        private void ToggleCurrentOffsetAngle()
        {
            if (angleToApproach == startAngle.z)
            {
                angleToApproach = DegreesOffset;
                return;
            }

            angleToApproach = startAngle.z;
        }

        private IEnumerator Move()
        {
            while (!Game.GameInstance.IsGameRunning)
            {
                yield return null;
            }

            yield return new WaitForEndOfFrame();
            SetDirectionBeginning();

            while (Recording)
            {
                if (HasReachedOffset)
                {
                    SetDirection();
                }

                yield return null;
            }

            while (!Recording)
            {
                yield return null;
            }

            if(!Game.GameInstance.GameOver)
                StartCoroutine(Move());
        }

        private void SetDirectionBeginning()
        {
            if (Dir == Direction.Horizontal)
            {
                RigBody.velocity = Random.Range(0, 2) == 1
                    ? new Vector2(-Game.GetGame<RunGame>().GameSpeed * SpeedMultiplier - MoveSpeed, 0)
                    : new Vector2(-Game.GetGame<RunGame>().GameSpeed * SpeedMultiplier + MoveSpeed, 0);
            }
            else
            {
                MoveSpeed = (Random.Range(0, 2) == 1 ? MoveSpeed : -MoveSpeed);
                RigBody.velocity = new Vector2(RigBody.velocity.x, MoveSpeed);
            }
        }

        private void SetDirection()
        {
            if (Dir == Direction.Horizontal)
            {
                //TODO: try to make this logic without any if statements (hint -Speed; +Speed*2)
                Tr.GetComponent<Rigidbody2D>().velocity = IsMovingTowardsOffset1
                   ? new Vector2(-Game.GetGame<RunGame>().GameSpeed * SpeedMultiplier + MoveSpeed, 0)
                   : new Vector2(-Game.GetGame<RunGame>().GameSpeed * SpeedMultiplier - MoveSpeed, 0);
            }
            else
            {
                MoveSpeed = -MoveSpeed;
                Tr.GetComponent<Rigidbody2D>().velocity = new Vector2(-Game.GetGame<RunGame>().GameSpeed * SpeedMultiplier, MoveSpeed); 
            }
        }

        public override void Rewind()
        {
            base.Rewind();
            if (!RotatableInitialized || rotAngles.Count == 0 || !SpriteRend.isVisible) return;
            RewindRotation();
        }

        private void RewindRotation()
        {
            Tr.eulerAngles = rotAngles.Pop();
            FirstInvokeDelayTime = firstInvokeDelayTimeStack.Pop();

            if (TypeOfRotation == RotationType.Loop) return;

            RotSpeed = rotSpeeds.Pop();
            angleToApproach = angleToApproachStack.Pop();
        }

        public override void Record()
        {
            base.Record();
            if (!RotatableInitialized || !SpriteRend.isVisible) return;
            RecordRotation();
        }

        private void RecordRotation()
        {
            rotAngles.Push(Tr.eulerAngles);
            firstInvokeDelayTimeStack.Push(FirstInvokeDelayTime);

            if (TypeOfRotation == RotationType.Loop) return;

            rotSpeeds.Push(RotSpeed);
            angleToApproachStack.Push(angleToApproach);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Destroy(normalizedGo);
        }
    }

#if UNITY_EDITOR

    #region editor variables code

    [CustomEditor(typeof(Platform))]
    public class PlatformEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var platform = target as Platform;
            platform.IsMovable = GUILayout.Toggle(platform.IsMovable, "Is Movable");

            if (platform.IsMovable)
            {
                SetMovableEditorOptions(platform);
            }

            platform.IsRotatable = GUILayout.Toggle(platform.IsRotatable, "Is Rotatable");

            if (platform.IsRotatable)
            {
                SetRotatableEditorOptions(platform);
            }
        }

        private static void SetRotatableEditorOptions(Platform platform)
        {
            platform.IsClockwise = GUILayout.Toggle(platform.IsClockwise, "Clockwise");
            platform.TypeOfRotation =
                (RotationType) EditorGUILayout.EnumPopup("RotationalType", platform.TypeOfRotation);

            if (platform.TypeOfRotation != RotationType.Loop)
            {
                platform.DegreesOffset = EditorGUILayout.Slider("degreesOffset", platform.DegreesOffset, 5, 180);
            }

            platform.RotSpeed = EditorGUILayout.Slider("RotationalSpeed", platform.RotSpeed, 1, 60);
            platform.FirstInvokeDelayTime = EditorGUILayout.Slider("firstInvokeDelayTime in seconds",
                platform.FirstInvokeDelayTime, 0, 20);
        }

        private static void SetMovableEditorOptions(Platform platform)
        {
            platform.Dir = (Direction) EditorGUILayout.EnumPopup("Direction", platform.Dir);

            string firstOffsetLabel,
                secondOffsetLabel;
            int offsetMaxValue;

            if (platform.Dir == Direction.Horizontal)
            {
                firstOffsetLabel = "LeftOffset";
                secondOffsetLabel = "RightOffset";
                offsetMaxValue = 1000;

                platform.MoveSpeed = EditorGUILayout.Slider("MoveSpeed", platform.MoveSpeed, 0, 500);
            }
            else
            {
                firstOffsetLabel = "DownOffset";
                secondOffsetLabel = "UpOffset";
                offsetMaxValue = 300;

                platform.MoveSpeed = EditorGUILayout.Slider("MoveSpeed", platform.MoveSpeed, 0, 200);
            }

            platform.Offset1 = EditorGUILayout.Slider(firstOffsetLabel, platform.Offset1, 0, offsetMaxValue);
            platform.Offset2 = EditorGUILayout.Slider(secondOffsetLabel, platform.Offset2, 0, offsetMaxValue);
        }
    }

    #endregion

#endif
}