using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ
{
    public class BrainGameScore : Score
    {
        private float pointsAdditionCorrectDefault;

        [SerializeField]
        private float correctInARowAddition = 2,
            pointsAdditionCorrect = 1;

        protected override void Start()
        {
            base.Start();
            pointsAdditionCorrectDefault = pointsAdditionCorrect;
        }

        public void AddAdditionalPoints()
        {
            Add(pointsAdditionCorrect);
            pointsAdditionCorrect += correctInARowAddition;
        }

        public void ResetAdditionalPoints()
        {
            pointsAdditionCorrect = pointsAdditionCorrectDefault;
        }
    }
}
