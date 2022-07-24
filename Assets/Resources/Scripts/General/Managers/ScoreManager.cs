using UnityEngine;

namespace Assets.Resources.Scripts.General.Managers
{
    public static class ScoreManager
    {
        private const string Key = "HighScore";
        public static int GetHigh(string gameName)
        {
            return GamePlayerPrefs.GetInt(gameName + Key);
        }

        public static void SetHigh(string gameName, int score)
        {
            if(IsRecord(gameName, score))
                GamePlayerPrefs.SetInt(gameName + Key, score);
        }

        public static bool IsRecord(string gameName, int score)
        {
            var current = GetHigh(gameName);
            return score > current;
        }
    }
}
