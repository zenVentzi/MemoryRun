using UnityEngine;

namespace Assets.Resources.Scripts.Games
{
    public class GameNames
    {
        #region calculation
        public const string Calculus = "Calculus";
        public const string FindSum = "FindSum";
        public const string GreaterLess = "GreaterLess";
        public const string MathSign = "MathSign";
        public const string NextNumber = "NextNumber";
        public const string CalcExpression = "CalcExpression";

        public readonly static string[] CalculationGames = 
        {
            Calculus,
            FindSum,
            GreaterLess,
            MathSign,
            NextNumber,
            CalcExpression
        };
        #endregion

        #region logic
        public const string DifferentWord = "DifferentWord";
        public const string HanoiTowers = "HanoiTowers";
        public const string LightsOut = "LightsOut";
        public const string OppositeWord = "OppositeWord";
        public const string WordOrder = "WordOrder";
        public const string SortNumbers = "SortNumbers";

        public readonly static string[] LogicGames = 
        {
            DifferentWord,
            HanoiTowers,
            LightsOut,
            OppositeWord,
            WordOrder,
            SortNumbers
        };
        #endregion

        #region memory
        public const string ClickOrder = "ClickOrder";
        public const string FindPairs = "FindPairs";
        public const string MemoryMatrix = "MemoryMatrix";
        public const string SpeedMatch = "SpeedMatch";
        public const string NumbersOrder = "NumbersOrder";
        public const string ReversedWords = "ReversedWords";

        public readonly static string[] MemoryGames = 
        {
            ClickOrder,
            FindPairs,
            MemoryMatrix,
            SpeedMatch,
            NumbersOrder,
            ReversedWords
        };
        #endregion

        #region visual
        public const string ColorOrder = "ColorOrder";
        public const string Directions = "Directions";
        public const string Mirror = "Mirror";
        public const string MostFrequentColor = "MostFrequentColor";
        public const string ShapeOrder = "ShapeOrder";
        public const string WrongColor = "WrongColor";

        public readonly static string[] VisualGames = 
        {
            ColorOrder,
            Directions,
            Mirror,
            MostFrequentColor,
            ShapeOrder,
            WrongColor
        };
        #endregion

        public const string Run = "Run";
    }
}
