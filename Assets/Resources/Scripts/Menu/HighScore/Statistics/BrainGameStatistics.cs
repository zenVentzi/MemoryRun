using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.General;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Menu.HighScore.Statistics
{
    public class BrainGameStatistics : GameStatistics
    {
        private int unfinishedGames;

        protected override void Init()
        {
            base.Init();
            unfinishedGames = PlayedGames.UnfinishedGames(GameName);
        }

        protected override void PrintValues()
        {
            base.PrintValues();

            var unfinshedGamesAsText = " unfinished games:  " + unfinishedGames;
            GameObjectManager.GetGoInChildren(Go, "UnfinishedGames").GetComponent<Text>().text = unfinshedGamesAsText;
        }
    }
}