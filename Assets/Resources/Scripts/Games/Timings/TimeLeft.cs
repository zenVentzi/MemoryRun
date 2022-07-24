using System.Collections;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.Timings
{
    public class TimeLeft : AbstractTime
    {
        public float AdditionCorrect = 1;

        protected override IEnumerator StartGame()
        {
            if (GameIntro.HasIntroBeenShown)
            {
                yield return new WaitForEndOfFrame();

                Game.GameInstance.HasGameStarted = true;
            }
            else
            {
                while (!GameIntro.HasIntroBeenShown)
                {
                    yield return null;
                }

                yield return new WaitForEndOfFrame();

                Game.GameInstance.HasGameStarted = true;
            }
        }

        protected override void UpdateTiming()
        {
            if (TimeProp - Time.deltaTime > 0)
            {
                TimeProp -= Time.deltaTime;
            }
            else if (!PauseButton.Instance.GamePaused)
            {
                GameOverMenu.Load();
            }
        }

        public void AddTime()
        {
            time += AdditionCorrect;
            UpdateTiming();
        }
    }
}