using System.Collections;
using System.Linq;
using Assets.Resources.Scripts.UI.Buttons;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.Run
{
    public class RewindButton : AnimatedButton
    {
        private List<IRewindable> rewindables;

        private Coroutine rewindCoroutine,
                          recordCoroutine;

        [UsedImplicitly]
        private void Awake()
        {
            rewindables = new List<IRewindable>();
        }

        [UsedImplicitly]
        private void AddToCollection(IRewindable rewindable)
        {
            rewindables.Add(rewindable);
            ToggleRecState(true);      
        }

        [UsedImplicitly]
        private void RemoveFromCollection(IRewindable rewindable)
        {
            rewindables.Remove(rewindable);
        }

        private IEnumerator RewindGame()
        {
            while (rewindables.Any(r => !r.OnHold) && rewindables.All(r => !r.Recording))
            {
                for (int i = 0; i < rewindables.Count; i++)
                {
                    rewindables[i].Rewind();
                } 

                yield return null;
            }
        }

        private void ToggleRecState(bool rec)
        {
            for (int i = 0; i < rewindables.Count; i++)
            {
                rewindables[i].Recording = rec;
            }
        }

        private IEnumerator RecordGame()
        {
            while (!Game.GameInstance.GameOver)
            {
                yield return new WaitForEndOfFrame();

                for (int i = 0; i < rewindables.Count; i++)
                {
                    rewindables[i].Record();
                }

                yield return null;
            }
        }

        protected override void OnnMouseDown()
        {
            if (Game.GameInstance.GameOver) return;

            base.OnnMouseDown();
            ToggleRecState(false);
            StopCoroutine(recordCoroutine);
            rewindCoroutine = StartCoroutine(RewindGame());
        }

        protected override void OnnMouseUp()
        {
            base.OnnMouseUp();
            ToggleRecState(true);
            if (rewindCoroutine != null) StopCoroutine(rewindCoroutine);
            recordCoroutine = StartCoroutine(RecordGame());
        }

        public void Ignite()
        {
            ToggleRecState(true);
            recordCoroutine = StartCoroutine(RecordGame());
        }
    }
}
