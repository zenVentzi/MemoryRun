using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.Games.Run;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu.RunTimeMenu
{
    public class RestartGameButton : CloseGameButton
    {
        private string gameName;

        [UsedImplicitly]
        private void Awake()
        {
            gameName = Game.GameInstance.name.Substring(0, Game.GameInstance.name.LastIndexOf("Game"));
            //var energyLeft = GameEnergyManager.GetEnergy(gameName);
            var energyLeft = 5;

            if(energyLeft <= 0)
            {
                Destroy(Go);
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            OnMenuDisappearEvent += Restart;

            if (!RunGame.IsPreview)
            {
                GameEnergyManager.AddEnergy(gameName, -1);
                GameManager.TotalGamesPlayed++;
            }
        }

        private void Restart()
        {
            Application.LoadLevelAdditive(Game.GameInstance.GameSceneName);

            if (!Game.GameInstance.GameOver) return;
            MenuAudioManager.Instance.Stop();
            GameAudioManager.Instance.Play();
        }
    }
}
