using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.Run
{
    public class TopColorLerp : ColorLerp, IRewindable
    {
        private static GameObject firstTop;

        private Stack<Color> currentColorStack,
                             nextColorStack;

        private Stack<float> lerpSpeedStack;

        private bool IsFirst
        {
            get { return null == firstTop || Go == firstTop; }
        }

        public bool OnHold
        {
            get { return currentColorStack.Count == 0; }
        }

        public bool Recording { get; set; }

        [UsedImplicitly]
        protected override void Start()
        {
            base.Start();

            if (!IsFirst) return;

            SubscribeRewindable();
            firstTop = Go;
        }

        protected override IEnumerator Lerp()
        {
            if (IsFirst)
            {
                while (!Game.GameInstance.HasGameStarted)
                {
                    yield return null;
                }

                #region manual iteration
                var baseLerp = base.Lerp();

                while (!Game.GameInstance.GameOver)
                {
                    if (Recording)
                    {
                        yield return baseLerp.MoveNext();                        
                    }
                    else
                    {
                        yield return null;
                    }
                }
                #endregion
            }
            else
            {
                yield return new WaitForEndOfFrame();

                while (!Game.GameInstance.GameOver)
                {
                    SpriteRend.color = firstTop.GetComponent<SpriteRenderer>().color;
                    yield return null;
                }
            }
        }

        public void SubscribeRewindable()
        {
            currentColorStack = new Stack<Color>();
            nextColorStack = new Stack<Color>();
            lerpSpeedStack = new Stack<float>();
            Game.GameInstance.BroadcastMessage("AddToCollection", this);
        }

        public void Rewind()
        {
            SpriteRend.color = currentColorStack.Pop();
            NextColor = nextColorStack.Pop();
            LerpSpeed = lerpSpeedStack.Pop();
        }

        public void Record()
        {
            //if (OnHold)
            //{
            //    currentColorStack.Push(Color.white);
            //    nextColorStack.Push(Color.white);                
            //}
            
            currentColorStack.Push(SpriteRend.color);
            nextColorStack.Push(NextColor);
            lerpSpeedStack.Push(LerpSpeed);
        }


        public void UnscribleRewindable()
        {
            Game.GameInstance.BroadcastMessage("RemoveFromCollection", this);
        }
    }
}