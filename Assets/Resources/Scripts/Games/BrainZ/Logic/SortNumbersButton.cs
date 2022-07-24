using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Games.BrainZ.Logic
{
    public class SortNumbersButton : GameButton
    {
        private Vector3 startPos;

        public bool IsCorrectPosition
        {
            get { return Tr.localPosition == startPos; }
        }

        public Vector3 StartPos
        {
            get { return startPos; }
        }

        [UsedImplicitly]
        private void Awake()
        {
            var buttonNumber = name.Substring(3);
            GetComponent<Text>().text = buttonNumber;
            startPos = Tr.position;
        }
    }
}
