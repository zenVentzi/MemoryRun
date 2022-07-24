using Assets.Resources.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Menu.HighScore.Statistics
{
    public class SizeDependentGameStatistics : BrainGameStatistics
    {
        [SerializeField]
        private int maxSize = 7;


        protected override void PrintValues()
        {
            base.PrintValues();

            for (int i = 3; i <= maxSize; i++)
            {
                var moves = GamePlayerPrefs.GetInt(GameName + i + "dMoves");
                var time = GamePlayerPrefs.GetInt(GameName + i + "dTime");
                var movesTxt = Tr.FindChild("Canvas").FindChild(i + "dMoves").GetComponent<Text>();
                var timeTxt = Tr.FindChild("Canvas").FindChild(i + "dTime").GetComponent<Text>();

                movesTxt.text = (moves < int.MaxValue && moves != 0)
                    ? moves.ToString()
                    : "N/A";
                timeTxt.text = (time < int.MaxValue && time != 0)
                    ? Helper.GetFormattedTime(time)
                    : "N/A";
            }
        }
    }
}
