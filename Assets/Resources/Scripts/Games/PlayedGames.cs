using Assets.Resources.Scripts.General;
using UnityEngine;

namespace Assets.Resources.Scripts.Games
{
    public class PlayedGames
    {
        public static void FinishGame()
        {
            var gameName = Game.GameInstance.Go.name.Substring(0, Game.GameInstance.Go.name.LastIndexOf("Game"));
            var finishedGames = FinishedGames(gameName) + 1;
            GamePlayerPrefs.SetInt(gameName + "FinishedGames", finishedGames);
        }

        public static void InterruptGame()
        {
            var gameName = Game.GameInstance.Go.name.Substring(0, Game.GameInstance.Go.name.LastIndexOf("Game"));
            var unfinishedGames = UnfinishedGames(gameName) + 1;
            GamePlayerPrefs.SetInt(gameName + "UnfinishedGames", unfinishedGames);
        }

        public static int FinishedGames(string gameName)
        {
            return GamePlayerPrefs.GetInt(gameName + "FinishedGames");
        }

        public static int UnfinishedGames(string gameName)
        {
            return GamePlayerPrefs.GetInt(gameName + "UnfinishedGames");
        }
    }
}
