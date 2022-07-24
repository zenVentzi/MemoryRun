using System.Collections.Generic;
using Assets.Resources.Scripts.General;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.Run
{
    public class RunGameScore : Score, IRewindable
    {
        private Stack<float> scoreStack;
        public bool Recording { get; set; }

        public bool FrameRecorded { get; set; }

        public bool OnHold
        {
            get { return scoreStack.Count <= 0; }
        }

        protected override void Start()
        {
            base.Start();

            if (RunGame.IsPreview)
            {
                Destroy(GameObjectManager.GetGoInChildren(Game.GameInstance.Go, "ScoreRect"));
                Destroy(Go);
            }
            else
            {
                SubscribeRewindable();
            }
        }

        public void Add()
        {
            Add((float)(RunGame.Difficulty + 1) * 10);
        }

        public void SubscribeRewindable()
        {
            scoreStack = new Stack<float>();
            Game.GameInstance.BroadcastMessage("AddToCollection", this);
        }

        public void Rewind()
        {
            Val = scoreStack.Pop();
        }

        public void Record()
        {
            scoreStack.Push(Val);
        }

        public void UnscribleRewindable()
        {
            Game.GameInstance.BroadcastMessage("RemoveFromCollection", this);
        }
    }
}
