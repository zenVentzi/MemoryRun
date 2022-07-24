using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.Games.Run;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Menu.RunTimeMenu
{
    public class GameOverMenu : RunTimeMenu
    {
        [UsedImplicitly]
        private void Start()
        {
            if (RunGame.IsPreview) return;
            SubmitResults();
        }

        private void SubmitResults()
        {
            GameObject highScore = GameObjectManager.GetGoInChildren(Go, "HighScore"),
                score = GameObjectManager.GetGoInChildren(Go, "Score"),
                exp = GameObjectManager.GetGoInChildren(Go, "Exp");

            string text1,
                text2,
                text3;

            var gameResult = ScriptableObject.CreateInstance<Result>();
            gameResult.Submit(out text1, out text2, out text3);
            score.GetComponent<Text>().text = text1;
            highScore.GetComponent<Text>().text = text2;
            exp.GetComponent<Text>().text = text3;
        }

        public static void Load()
        {
            Game.GameInstance.GameOver = true;
            CollidersManager.EnableGameColliders(enabled: false);
            Application.LoadLevelAdditive("GameOverScene");
            GameAudioManager.Instance.Stop();
            MenuAudioManager.Instance.Play();
            EffectsAudioManager.Instance.PlayGameOver();
        }
    }
}