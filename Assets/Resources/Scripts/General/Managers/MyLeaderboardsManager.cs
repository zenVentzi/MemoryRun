using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

namespace Assets.Resources.Scripts.General.Managers
{
    public class MyLeaderboardsManager : MonoBehaviour
    {
        private static readonly Dictionary<string, string> SectionNamesToIds = new Dictionary<string, string>
        {
            {"Calculation", "CgkI9IGIn-kSEAIQIw"},
            {"Memory", "CgkI9IGIn-kSEAIQJQ"},
            {"Logic", "CgkI9IGIn-kSEAIQJA"},
            {"Visual", "CgkI9IGIn-kSEAIQJg"},
            {"Concentration", "CgkI9IGIn-kSEAIQJw"}
        };

        private static readonly Dictionary<string, string> GameNameToIdMap = new Dictionary<string, string>
        {
            {"CalcExpression", "CgkI9IGIn-kSEAIQCw"},
            {"Calculus", "CgkI9IGIn-kSEAIQDA"},
            {"FindSum", "CgkI9IGIn-kSEAIQEg"},
            {"GreaterLess", "CgkI9IGIn-kSEAIQEw"},
            {"MathSign", "CgkI9IGIn-kSEAIQFg"},
            {"DifferentWord", "CgkI9IGIn-kSEAIQDw"},
            {"NextNumber", "CgkI9IGIn-kSEAIQGg"},
            {"OppositeWord", "CgkI9IGIn-kSEAIQHA"},
            {"WordOrder", "CgkI9IGIn-kSEAIQIQ"},
            {"ClickOrder", "CgkI9IGIn-kSEAIQDQ"},
            {"FindPairs", "CgkI9IGIn-kSEAIQEQ"},
            {"MemoryMatrix", "CgkI9IGIn-kSEAIQFw"},
            {"NumbersOrder", "CgkI9IGIn-kSEAIQGw"},
            {"SpeedMatch", "CgkI9IGIn-kSEAIQIA"},
            {"ReversedWords", "CgkI9IGIn-kSEAIQHQ"},
            {"ColorOrder", "CgkI9IGIn-kSEAIQDg"},
            {"Directions", "CgkI9IGIn-kSEAIQEA"},
            {"Mirror", "CgkI9IGIn-kSEAIQGA"},
            {"MostFrequentColor", "CgkI9IGIn-kSEAIQGQ"},
            {"ShapeOrder", "CgkI9IGIn-kSEAIQHg"},
            {"WrongColor", "CgkI9IGIn-kSEAIQIg"},
            {"HanoiTowers", "CgkI9IGIn-kSEAIQFA"},
            {"LightsOut", "CgkI9IGIn-kSEAIQFQ"},
            {"SortNumbers", "CgkI9IGIn-kSEAIQHw"},
            {"Run", "CgkI9IGIn-kSEAIQKA"}
        };

        public static void Submit(string gameName, int score)
        {
            var gameId = GameNameToIdMap[gameName];
            PlayGamesPlatform.Instance.ReportScore(score, gameId, success =>
            {
                if (!success)
                {
                    SaveLocally(gameName, score);
                }
            });
        }

        private static void SubmitSections()
        {
            foreach (var sectionNamesToId in SectionNamesToIds)
            {
                var lvl = Experience.GetSectionLevel(sectionNamesToId.Key);

                PlayGamesPlatform.Instance.ReportScore(lvl, sectionNamesToId.Value, success =>
                {
                });
            }
        }

        public static void UpdateLeaderboards()
        {
            SubmitSections();
            SubmitLocallySaved();
        }

        private static void SubmitLocallySaved()
        {
            foreach (var namesToId in GameNameToIdMap)
            {
                var localScore = GamePlayerPrefs.GetInt(namesToId.Key + "LocalSafe");
                //Debug.Log(sectionNamesToId.Key + "LocalSafe " + localScore);

                if (localScore > 0)
                {
                    PlayGamesPlatform.Instance.ReportScore(localScore, namesToId.Value, success =>
                    {
                    }); 
                }
            }
        }

        private static void SaveLocally(string gameName, int score)
        {
            GamePlayerPrefs.SetInt(gameName + "LocalSafe", score);
        }
    }
}
