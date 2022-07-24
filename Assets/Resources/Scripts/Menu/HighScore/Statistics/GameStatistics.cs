using System;
using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Menu.HighScore.Statistics
{
    public class GameStatistics : ScrollableMenu
    {
        private int finishedGames,
                    highScore;
        protected string GameName { get; private set; }

        [UsedImplicitly]
        protected override void Start()
        {
            base.Start();

            Init();
            SetValues();
            PrintValues();
            PrintBadges();
        }

        protected virtual void Init()
        {
            GameName = Tr.name.Substring(0, Tr.name.LastIndexOf("Statistics"));
        }

        protected virtual void SetValues()
        {
            finishedGames = PlayedGames.FinishedGames(GameName);
            highScore = ScoreManager.GetHigh(GameName);
        }

        protected virtual void PrintValues()
        {
            var finishedGamesAsText = " finished games:  " + finishedGames;
            var levelAsText = " skill:  " + Experience.GetGameLevel(GameName);

            GameObjectManager.GetGoInChildren(Go, "FinishedGames").GetComponent<Text>().text = finishedGamesAsText;
            GameObjectManager.GetGoInChildren(Go, "Level").GetComponent<Text>().text = levelAsText;
            GameObjectManager.GetGoInChildren(Go, "BestScore").GetComponent<Text>().text =
                " best score:  " + highScore;
        }

        private void PrintBadges()
        {
            GameObjectManager.GetGoInChildren(Go, "Badge").GetComponent<SpriteRenderer>().sprite = Badge.GetGame(GameName);

            var scoreLeftToNextLevel = GameObjectManager.GetGoInChildren(Go, "ScoreLeftTo").GetComponent<Text>();

            if (Proficiency.GetGame(GameName) == Proficiency.Level.Expert)
            {
                scoreLeftToNextLevel.text = "warning  <<<!!!>>>>  this player is a professor";
            }
            else
            {
                var nextLevel = Proficiency.GetGameNextLevel(GameName);
                var minScoreForNextLevel = Proficiency.GetMinScoreForLevel(nextLevel, GameName);
                scoreLeftToNextLevel.text = String.Format("{0} score needed for", minScoreForNextLevel);
                GameObjectManager.GetGoInChildren(Go, "CommingBadge").GetComponent<SpriteRenderer>().sprite = Badge.GetGameNext(GameName);
            }
        }
    }
}