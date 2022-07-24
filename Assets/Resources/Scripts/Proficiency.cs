using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.General;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class Proficiency : MyMono
    {
        #region Level enum

        public enum Level
        {
            Beginner,
            Mediocre,
            Advanced,
            Expert
        }

        #endregion

        public static Level GetGame(string gameName)
        {
            int bestScore = GamePlayerPrefs.GetInt(gameName + "HighScore");
            #region switch

            //switch (gameName)
            //{
            //    case "Calculus":
            //        {
            //            return GetCalculusGame(bestScore);
            //        }
            //    case "FindSum":
            //        {
            //            return GetFindSumGame(bestScore);
            //        }
            //    case "GreaterLess":
            //        {
            //            return GetGreaterOrLessGame(bestScore);
            //        }
            //    case "MathSign":
            //        {
            //            return GetMathSignGame(bestScore);
            //        }
            //    case "CalcExpression":
            //        {
            //            return GetCalcExpressionGame(bestScore);
            //        }
            //    case "DifferentWord":
            //        {
            //            return GetDifferentWordGame(bestScore);
            //        }
            //    case "HanoiTowers":
            //        {
            //            return GetHanoiTowersGame(bestScore);
            //        }
            //    case "LightsOut":
            //        {
            //            return GetLightsOutGame(bestScore);
            //        }
            //    case "SortNumbers":
            //        {
            //            return GetSortNumbersGame(bestScore);
            //        }
            //    case "NextNumber":
            //        {
            //            return GetNextNumberGame(bestScore);
            //        }
            //    case "OppositeWord":
            //        {
            //            return GetOppositeWordGame(bestScore);
            //        }
            //    case "MemoryMatrix":
            //        {
            //            return GetMemoryMatrixGame(bestScore);
            //        }
            //    case "WordOrder":
            //        {
            //            return GetWordOrderGame(bestScore);
            //        }
            //    case "ClickOrder":
            //        {
            //            return GetClickOrderGame(bestScore);
            //        }
            //    case "FindPairs":
            //        {
            //            return GetFindPairsGame(bestScore);
            //        }
            //    case "NumbersOrder":
            //        {
            //            return GetNumbersOrderGame(bestScore);
            //        }
            //    case "ReversedWords":
            //        {
            //            return GetReversedWordsGame(bestScore);
            //        }
            //    case "SpeedMatch":
            //        {
            //            return GetSpeedMatchGame(bestScore);
            //        }
            //    case "ColourOrder":
            //        {
            //            return GetColourOrderGame(bestScore);
            //        }
            //    case "Directions":
            //        {
            //            return GetDirectionsGame(bestScore);
            //        }
            //    case "Mirror":
            //        {
            //            return GetMirrorGame(bestScore);
            //        }
            //    case "MostFrequentColour":
            //        {
            //            return GetMostFrequentColourGame(bestScore);
            //        }
            //    case "ShapeOrder":
            //        {
            //            return GetShapeOrderGame(bestScore);
            //        }
            //    case "WrongColour":
            //        {
            //            return GetWrongColourGame(bestScore);
            //        }
            //    case "Run":
            //        {
            //            return GetRunGame(bestScore);
            //        }
            //}

            #endregion

            var minScoreForMediocre = GetMinScoreForLevel(Level.Mediocre, gameName);
            var minScoreForAdvanced = GetMinScoreForLevel(Level.Advanced, gameName);
            var minScoreForExpert = GetMinScoreForLevel(Level.Expert, gameName);

            if (bestScore < minScoreForMediocre)
            {
                return Level.Beginner;                
            }
            if (bestScore < minScoreForAdvanced)
            {
                return Level.Mediocre;
            }

            return bestScore < minScoreForExpert ? Level.Advanced : Level.Expert;
        }

        public static Level GetSection(string sectionName)
        {
            if (sectionName.Contains("Calculation"))
            {
                return GetCalculationSection();
            }
            if (sectionName.Contains("Logic"))
            {
                return GetLogicSection();
            }
            if (sectionName.Contains("Memory"))
            {
                return GetMemorySection();
            }
            return sectionName.Contains("Visual") ? GetVisualSection() : GetGame("Run");
        }

        private static Level GetCalculationSection()
        {
            int prof = (int)GetGame(GameNames.Calculus) + (int)GetGame(GameNames.FindSum) + (int)GetGame(GameNames.GreaterLess) +
                       (int)GetGame(GameNames.MathSign) + (int)GetGame(GameNames.NextNumber) + (int)GetGame(GameNames.CalcExpression);
            return (Level)(prof / 6);
        }

        private static Level GetLogicSection()
        {
            int prof = (int)GetGame(GameNames.DifferentWord) + (int)GetGame(GameNames.HanoiTowers) + (int)GetGame(GameNames.LightsOut) +
                       (int)GetGame(GameNames.OppositeWord) + (int)GetGame(GameNames.WordOrder) + (int)GetGame(GameNames.SortNumbers);

            return (Level)(prof / 6);
        }

        private static Level GetMemorySection()
        {
            int prof = (int)GetGame(GameNames.ClickOrder) + (int)GetGame(GameNames.FindPairs) + (int)GetGame(GameNames.MemoryMatrix) +
                       (int)GetGame(GameNames.SpeedMatch) + (int)GetGame(GameNames.NumbersOrder) + (int)GetGame(GameNames.ReversedWords);

            return (Level)(prof / 6);
        }

        private static Level GetVisualSection()
        {
            int prof = (int)GetGame(GameNames.ColorOrder) + (int)GetGame(GameNames.Directions) + (int)GetGame(GameNames.Mirror) +
                       (int)GetGame(GameNames.MostFrequentColor) + (int)GetGame(GameNames.ShapeOrder) + (int)GetGame(GameNames.WrongColor);

            return (Level)(prof / 6);
        }

        public static int GetMinScoreForLevel(Level lvl, string gameName)
        {
            switch (gameName)
            {
                case GameNames.Calculus:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 2000;
                                }
                            case Level.Advanced:
                                {
                                    return 8000;
                                }
                            case Level.Expert:
                                {
                                    return 13000;
                                }
                        }
                        break;
                    }
                case GameNames.FindSum:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 2000;
                                }
                            case Level.Advanced:
                                {
                                    return 5000;
                                }
                            case Level.Expert:
                                {
                                    return 9000;
                                }
                        }
                        break;
                    }
                case GameNames.GreaterLess:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 5000;
                                }
                            case Level.Advanced:
                                {
                                    return 10000;
                                }
                            case Level.Expert:
                                {
                                    return 15000;
                                }
                        }
                        break;
                    }
                case GameNames.MathSign:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 6000;
                                }
                            case Level.Advanced:
                                {
                                    return 10000;
                                }
                            case Level.Expert:
                                {
                                    return 14000;
                                }
                        }
                        break;
                    }
                case GameNames.NextNumber:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 2500;
                                }
                            case Level.Advanced:
                                {
                                    return 5000;
                                }
                            case Level.Expert:
                                {
                                    return 9000;
                                }
                        }
                        break;
                    }
                case GameNames.CalcExpression:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 2500;
                                }
                            case Level.Advanced:
                                {
                                    return 4500;
                                }
                            case Level.Expert:
                                {
                                    return 8000;
                                }
                        }
                        break;
                    }
                case GameNames.DifferentWord:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 600;
                                }
                            case Level.Advanced:
                                {
                                    return 1100;
                                }
                            case Level.Expert:
                                {
                                    return 3000;
                                }
                        }
                        break;
                    }
                case GameNames.HanoiTowers:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 7000;
                                }
                            case Level.Advanced:
                                {
                                    return 10000;
                                }
                            case Level.Expert:
                                {
                                    return 15000;
                                }
                        }
                        break;
                    }
                case GameNames.LightsOut:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 7000;
                                }
                            case Level.Advanced:
                                {
                                    return 10000;
                                }
                            case Level.Expert:
                                {
                                    return 15000;
                                }
                        }
                        break;
                    }
                case GameNames.OppositeWord:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1500;
                                }
                            case Level.Advanced:
                                {
                                    return 3000;
                                }
                            case Level.Expert:
                                {
                                    return 7000;
                                }
                        }
                        break;
                    }
                case GameNames.WordOrder:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1000;
                                }
                            case Level.Advanced:
                                {
                                    return 2500;
                                }
                            case Level.Expert:
                                {
                                    return 4000;
                                }
                        }
                        break;
                    }
                case GameNames.SortNumbers:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 7000;
                                }
                            case Level.Advanced:
                            {
                                return 10000;
                            }
                            case Level.Expert:
                                {
                                    return 15000;
                                }
                        }
                        break;
                    }
                case GameNames.ClickOrder:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1000;
                                }
                            case Level.Advanced:
                                {
                                    return 2500;
                                }
                            case Level.Expert:
                                {
                                    return 5000;
                                }
                        }
                        break;
                    }
                case GameNames.FindPairs:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 3000;
                                }
                            case Level.Advanced:
                                {
                                    return 6000;
                                }
                            case Level.Expert:
                                {
                                    return 10000;
                                }
                        }
                        break;
                    }
                case GameNames.MemoryMatrix:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1500;
                                }
                            case Level.Advanced:
                                {
                                    return 3000;
                                }
                            case Level.Expert:
                                {
                                    return 6000;
                                }
                        }
                        break;
                    }
                case GameNames.SpeedMatch:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1000;
                                }
                            case Level.Advanced:
                                {
                                    return 3000;
                                }
                            case Level.Expert:
                                {
                                    return 5000;
                                }
                        }
                        break;
                    }
                case GameNames.NumbersOrder:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 600;
                                }
                            case Level.Advanced:
                                {
                                    return 1500;
                                }
                            case Level.Expert:
                                {
                                    return 3000;
                                }
                        }
                        break;
                    }
                case GameNames.ReversedWords:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1000;
                                }
                            case Level.Advanced:
                                {
                                    return 2000;
                                }
                            case Level.Expert:
                                {
                                    return 3000;
                                }
                        }
                        break;
                    }
                case GameNames.ColorOrder:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1000;
                                }
                            case Level.Advanced:
                                {
                                    return 2000;
                                }
                            case Level.Expert:
                                {
                                    return 3000;
                                }
                        }
                        break;
                    }
                case GameNames.Directions:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 4000;
                                }
                            case Level.Advanced:
                                {
                                    return 9000;
                                }
                            case Level.Expert:
                                {
                                    return 14000;
                                }
                        }
                        break;
                    }
                case GameNames.Mirror:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 4500;
                                }
                            case Level.Advanced:
                                {
                                    return 8000;
                                }
                            case Level.Expert:
                                {
                                    return 13000;
                                }
                        }
                        break;
                    }
                case GameNames.MostFrequentColor:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1000;
                                }
                            case Level.Advanced:
                                {
                                    return 2000;
                                }
                            case Level.Expert:
                                {
                                    return 3000;
                                }
                        }
                        break;
                    }
                case GameNames.ShapeOrder:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1000;
                                }
                            case Level.Advanced:
                                {
                                    return 2000;
                                }
                            case Level.Expert:
                                {
                                    return 3000;
                                }
                        }
                        break;
                    }
                case GameNames.WrongColor:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 1000;
                                }
                            case Level.Advanced:
                                {
                                    return 2000;
                                }
                            case Level.Expert:
                                {
                                    return 3000;
                                }
                        }
                        break;
                    }
                case GameNames.Run:
                    {
                        switch (lvl)
                        {
                            case Level.Mediocre:
                                {
                                    return 300;
                                }
                            case Level.Advanced:
                                {
                                    return 1300;
                                }
                            case Level.Expert:
                                {
                                    return 3300;
                                }
                        }
                        break;
                    }
            }

            return 0;
        }

        public static Level GetGameNextLevel(string gameName)
        {
            var currentLevel = GetGame(gameName);
            return ((int)currentLevel < 3) ? currentLevel + 1 : currentLevel;
        }

        //private static Level GetCalculusGame(int score)
        //{
        //    if (score > 13000)
        //    {
        //        return Level.Expert;
        //    }
        //    if (score > 8000)
        //    {
        //        return Level.Advanced;
        //    }

        //    return score > 2000 ? Level.Mediocre : Level.Beginner;
        //}

        private static Level GetFindSumGame(int score)
        {
            if (score > 11000)
            {
                return Level.Expert;
            }
            if (score > 7000)
            {
                return Level.Advanced;
            }
            return score > 2000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetGreaterOrLessGame(int score)
        {
            if (score > 16000)
            {
                return Level.Expert;
            }
            if (score > 12000)
            {
                return Level.Advanced;
            }

            return score > 5000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetMathSignGame(int score)
        {
            if (score > 15000)
            {
                return Level.Expert;
            }
            if (score > 11000)
            {
                return Level.Advanced;
            }

            return score > 6000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetCalcExpressionGame(int score)
        {
            if (score > 11000)
            {
                return Level.Expert;
            }
            if (score > 7000)
            {
                return Level.Advanced;
            }

            return score > 3000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetDifferentWordGame(int score)
        {
            if (score > 3000)
            {
                return Level.Expert;
            }
            if (score > 1000)
            {
                return Level.Advanced;
            }

            return score > 500 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetHanoiTowersGame(int score)
        {
            #region old
            //if (PlayerPrefs.GetInt("HanoiTowersGame6dMoves", int.MaxValue) == 63 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 75 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame5dMoves", int.MaxValue) == 31 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 55 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame4dMoves", int.MaxValue) == 15 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 25 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame3dMoves", int.MaxValue) == 7 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 5)
            //{
            //    return Level.Expert;
            //}
            //if (PlayerPrefs.GetInt("HanoiTowersGame6dMoves", int.MaxValue) == 63 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 90 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame5dMoves", int.MaxValue) == 31 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 65 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame4dMoves", int.MaxValue) == 15 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 30 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame3dMoves", int.MaxValue) == 7 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 6)
            //{
            //    return Level.Advanced;
            //}
            //if (PlayerPrefs.GetInt("HanoiTowersGame6dMoves", int.MaxValue) == 63 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 100 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame5dMoves", int.MaxValue) == 31 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 80 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame4dMoves", int.MaxValue) == 15 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 50 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame3dMoves", int.MaxValue) == 7 &&
            //    PlayerPrefs.GetInt("HanoiTowersGame6dTime", int.MaxValue) < 7)
            //{
            //    return Level.Mediocre;
            //}

            //return Level.Beginner;
            #endregion

            if (score > 20000)
            {
                return Level.Expert;
            }
            if (score > 15000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetMemoryMatrixGame(int score)
        {
            if (score > 6000)
            {
                return Level.Expert;
            }
            if (score > 3000)
            {
                return Level.Advanced;
            }

            return score > 1500 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetSortNumbersGame(int score)
        {
            if (score > 30000)
            {
                return Level.Expert;
            }
            if (score > 20000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetLightsOutGame(int score)
        {
            if (score > 30000)
            {
                return Level.Expert;
            }
            if (score > 20000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetNextNumberGame(int score)
        {
            if (score > 30000)
            {
                return Level.Expert;
            }
            if (score > 20000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetReversedWordsGame(int score)
        {
            if (score > 3000)
            {
                return Level.Expert;
            }
            if (score > 2000)
            {
                return Level.Advanced;
            }

            return score > 1000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetOppositeWordGame(int score)
        {
            if (score > 7000)
            {
                return Level.Expert;
            }
            if (score > 3000)
            {
                return Level.Advanced;
            }

            return score > 1500 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetWordOrderGame(int score)
        {
            if (score > 4000)
            {
                return Level.Expert;
            }
            if (score > 2500)
            {
                return Level.Advanced;
            }

            return score > 1000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetClickOrderGame(int score)
        {
            if (score > 20000)
            {
                return Level.Expert;
            }
            if (score > 15000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetFindPairsGame(int score)
        {
            if (score > 10000)
            {
                return Level.Expert;
            }
            if (score > 6000)
            {
                return Level.Advanced;
            }

            return score > 3000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetNumbersOrderGame(int score)
        {
            if (score > 20000)
            {
                return Level.Expert;
            }
            if (score > 15000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetSpeedMatchGame(int score)
        {
            if (score > 6000)
            {
                return Level.Expert;
            }
            if (score > 3000)
            {
                return Level.Advanced;
            }

            return score > 1000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetColourOrderGame(int score)
        {
            if (score > 20000)
            {
                return Level.Expert;
            }
            if (score > 15000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetDirectionsGame(int score)
        {
            if (score > 15000)
            {
                return Level.Expert;
            }
            if (score > 10000)
            {
                return Level.Advanced;
            }

            return score > 4000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetMirrorGame(int score)
        {
            if (score > 15000)
            {
                return Level.Expert;
            }
            if (score > 10000)
            {
                return Level.Advanced;
            }

            return score > 5000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetMostFrequentColourGame(int score)
        {
            if (score > 20000)
            {
                return Level.Expert;
            }
            if (score > 15000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetShapeOrderGame(int score)
        {
            if (score > 20000)
            {
                return Level.Expert;
            }
            if (score > 15000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetWrongColourGame(int score)
        {
            if (score > 20000)
            {
                return Level.Expert;
            }
            if (score > 15000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }

        private static Level GetRunGame(int score)
        {
            if (score > 20000)
            {
                return Level.Expert;
            }
            if (score > 15000)
            {
                return Level.Advanced;
            }

            return score > 10000 ? Level.Mediocre : Level.Beginner;
        }
    }
}