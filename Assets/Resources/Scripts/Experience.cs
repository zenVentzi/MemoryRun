using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu.HighScore;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class Experience : ScriptableObject
    {
        #region CustomGame

        public static void Add(string gameName, int exp)
        {
            var key = gameName + "Exp";
            var expSoFar = GamePlayerPrefs.GetInt(key);
            GamePlayerPrefs.SetInt(key, expSoFar + exp);
        }

        private static int GetGameExp(string gameName)
        {
            var key = gameName + "Exp";
            return GamePlayerPrefs.GetInt(key);
        }

        public static int GetGameBar(string gameName)
        {
            return GetGameExp(gameName) % ScoreSectionMenu.FullGameExpBar;
        }

        public static int GetGameLevel(string gameName)
        {
            return GetGameExp(gameName) / ScoreSectionMenu.FullGameExpBar + 1;
        }

        #endregion

        #region CustomSection

        private static int GetSectionExp(string name)
        {
            float exp = 0;

            if (name.Contains("Calculation"))
            {
                for (int i = 0; i < GameNames.CalculationGames.Length; i++)
                {
                    exp += GetGameExp(GameNames.CalculationGames[i]);
                }

                exp /= 6;
            }
            else if (name.Contains("Logic"))
            {
                for (int i = 0; i < GameNames.LogicGames.Length; i++)
                {
                    exp += GetGameExp(GameNames.LogicGames[i]);
                }

                exp /= 6;
            }
            else if (name.Contains("Memory"))
            {
                for (int i = 0; i < GameNames.MemoryGames.Length; i++)
                {
                    exp += GetGameExp(GameNames.MemoryGames[i]);
                }

                exp /= 6;
            }
            else if (name.Contains("Visual"))
            {
                for (int i = 0; i < GameNames.VisualGames.Length; i++)
                {
                    exp += GetGameExp(GameNames.VisualGames[i]);
                }

                exp /= 6;
            }
            else
            {
                exp = GetGameExp(GameNames.Run);
            }

            return (int)exp;
        }

        public static int GetSectionBar(string name)
        {
            return GetSectionExp(name) % ScoreSectionMenu.FullGameExpBar;
        }

        public static int GetSectionLevel(string name)
        {
            return GetSectionExp(name) / ScoreSectionMenu.FullGameExpBar + 1;
        }

        #endregion

        #region WholeGame

        private static int GetWholeGameExp()
        {
            float exp = 0;

            exp += GetSectionExp("Calculation");
            exp += GetSectionExp("Logic");
            exp += GetSectionExp("Memory");
            exp += GetSectionExp("Visual");
            exp += GetSectionExp("Run");
            exp /= 5;

            return (int)exp;
        }

        public static int GetWholeGameBar()
        {
            return GetWholeGameExp() % ScoreSectionMenu.FullGameExpBar;
        }

        public static int GetWholeGameLevel()
        {
            return GetWholeGameExp() / ScoreSectionMenu.FullGameExpBar + 1;
        }

        #endregion
    }
}