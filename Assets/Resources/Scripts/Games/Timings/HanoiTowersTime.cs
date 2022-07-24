using System.Collections;

namespace Assets.Resources.Scripts.Games.Timings
{
    public class HanoiTowersTime : TimeElapse
    {
        protected override IEnumerator StartGame()
        {
            while (!Game.GameInstance.HasGameStarted)
            {
                yield return null;
            }
        }
    }
}