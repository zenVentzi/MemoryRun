using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.General.Managers
{
    [Serializable]
    public class GameManager : MyMono
    {
        public static int TotalGamesPlayed
        {
            get { return GamePlayerPrefs.GetInt("TotalGamesPlayed"); }
            set { GamePlayerPrefs.SetInt("TotalGamesPlayed", value);}
        }

        [UsedImplicitly]
        void Start()
        {
            Application.LoadLevelAdditive("SplashScene");
            Application.LoadLevelAdditive("MainMenuScene");
        }
    }
}
