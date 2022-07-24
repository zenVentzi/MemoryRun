using Assets.Resources.Scripts.Games.BrainZ.Logic;
using Assets.Resources.Scripts.Games.Run;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using UnityEngine;

namespace Assets.Resources.Scripts.Games
{
    internal class Result : ScriptableObject
    {
        private string gameName;

        public void Submit(out string score, out string highScore, out string exp)
        {
            gameName = Game.GameInstance.Go.name.Substring(0, Game.GameInstance.Go.name.LastIndexOf("Game"));
            PlayedGames.FinishGame();

            switch (gameName)
            {
                case "HanoiTowers":
                case "SortNumbers":
                case "LightsOut":
                    {
                        SubmitSizeDependentGame(out score, out highScore, out exp);
                        break;
                    }
                case "Run":
                {
                    SubmitRunGame(out score, out highScore, out exp);
                    break;
                }
                default:
                    {
                        SubmitBasicBrainGame(out score, out highScore, out exp);
                        break;
                    }
            }
        }

        private void SubmitRunGame(out string scoreText, out string highScoreText, out string expText)
        {
            if (!RunGame.IsPreview)
            {
                SubmitBasicBrainGame(out scoreText, out highScoreText, out expText);
            }
            else
            {
                scoreText = highScoreText = expText = string.Empty;
            }
        }

        private void SubmitBasicBrainGame(out string scoreText, out string highScoreText, out string expText)
        {
            var scoreNow = (int)Game.GameInstance.ScoreRef.Get();
            var highScoreSoFar = ScoreManager.GetHigh(gameName);
            var additionalExp = GetExpFromScore(scoreNow);
            Experience.Add(gameName, additionalExp);
            expText = "+ " + additionalExp + " XP";
            highScoreText = "High score: " + highScoreSoFar;
            scoreText = "Score: " + scoreNow;

            if (!ScoreManager.IsRecord(gameName, scoreNow)) return;

            ScoreManager.SetHigh(gameName, scoreNow);
            highScoreText = "NEW HIGH SCORE: " + scoreNow;
            MyLeaderboardsManager.Submit(gameName, scoreNow);
            scoreText = string.Empty;
        }

        private int GetExpFromScore(int score)
        {
            var divisor = (Experience.GetGameLevel(gameName) / 2) > 0 ? (Experience.GetGameLevel(gameName) / 2) : 1;
            return (score / 3) / divisor;
        }

        private void SubmitSizeDependentGame(out string scoreText, out string highScoreText, out string expText)
        {
            var size = Game.GetGame<SizeDependentGame>().Size;
            var moves = Game.GetGame<SizeDependentGame>().Moves;
            var time = Mathf.CeilToInt(AbstractTime.Instance.time);
            var score = GetSizeDependentScore(size, moves, time);
            ScoreManager.SetHigh(gameName, score);
                
            var additionalExp = GetExpFromScore(score);
            scoreText = string.Format("{0} seconds for {1} moves", time, moves);

            highScoreText = "High score: \n";
            string bestTimeKey = gameName + size + "dTime",
                   bestMovesKey = gameName + size + "dMoves";

            if (moves < GamePlayerPrefs.GetInt(bestMovesKey, int.MaxValue))
            {
                scoreText = string.Empty;
                highScoreText = "NEW HIGH SCORE: \n";
                GamePlayerPrefs.SetInt(bestMovesKey, moves);
                GamePlayerPrefs.SetInt(bestTimeKey, time);
            }
            else if (time < GamePlayerPrefs.GetInt(bestTimeKey, int.MaxValue))
            {
                scoreText = string.Empty;
                highScoreText = "NEW HIGH SCORE: \n";
                GamePlayerPrefs.SetInt(bestTimeKey, time);
            }

            time = GamePlayerPrefs.GetInt(bestTimeKey, int.MaxValue);
            highScoreText += string.Format("{0} seconds for {1} moves", time, moves);

            expText = "+ " + additionalExp + " XP";
            Experience.Add(gameName, additionalExp);
            MyLeaderboardsManager.Submit(gameName, score);
        }

        private int GetSizeDependentScore(int size, int moves, int time)
        {
            var score = 0;

            switch (gameName)
            {
                case "HanoiTowers":
                    {
                        score = (int)Mathf.Pow(size, 8.18f) / time;
                        break;
                    };
                case "SortNumbers":
                    {
                        score = (int)Mathf.Pow(size, 8.18f) / time;
                        break;
                    }
                case "LightsOut":
                    {
                        score = (int)Mathf.Pow(size, 8.18f) / time;
                        break;
                    }
            }

            return score;
        }
    }
}