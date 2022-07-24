using System;
using System.Collections.Generic;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.Run
{
    public class ScrollingRunObj : MyMono, IRewindable
    {
        private float speedMultiplier;
        private Vector2 startVelocity;
        private Stack<Vector3> positionStack;

        private bool StackEmpty { get { return positionStack.Count == 0; } }

        protected float SpeedMultiplier
        {
            get { return (Math.Abs(speedMultiplier) < 0.01f ? 60 : speedMultiplier); }
            set { speedMultiplier = value; }
        }

        public bool OnHold
        {
            get
            {
                return StackEmpty;
            }
        }

        public bool FrameRecorded { get; set; }


        public bool Recording { get; set; }

        protected virtual void Start()
        {
            startVelocity = new Vector2(-RunGame.Speed * SpeedMultiplier, RigBody.velocity.y);
            SubscribeRewindable();
        }

        [UsedImplicitly]
        private void Update()
        {
            if (!Game.GameInstance.GameOver) return;

            Tr.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StopAllCoroutines();
            CancelInvoke();
        }

        public virtual void SubscribeRewindable()
        {
            positionStack = new Stack<Vector3>();
            Game.GameInstance.BroadcastMessage("AddToCollection", this);
        }

        public void UnscribleRewindable()
        {
            if (Game.GameInstance != null)
                Game.GameInstance.BroadcastMessage("RemoveFromCollection", this);//the error is only when exit the game so it's not a probl
        }

        public virtual void Rewind()
        {
            try
            {
                Tr.position = positionStack.Pop();
            }
            catch (Exception)
            {
                Destroy(Go);
                return;
            }

            if (StackEmpty)
                RigBody.velocity = Vector2.zero;
        }

        public virtual void Record()
        {
            if (OnHold)
                RigBody.velocity = startVelocity;//do this when new is added

            positionStack.Push(Tr.position);
        }

        [UsedImplicitly]
        protected virtual void OnDestroy()
        {
            UnscribleRewindable();
            Destroy(Go);
        }
    }
}