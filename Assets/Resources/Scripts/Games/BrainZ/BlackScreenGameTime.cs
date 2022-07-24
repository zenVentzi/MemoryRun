using System.Collections;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.General.Managers;

namespace Assets.Resources.Scripts.Games.BrainZ
{
    public class BlackScreenGameTime : TimeLeft
    {
        private bool hasBlackScreenBeenActivated;

        protected override IEnumerator StartGame()
        {
            while (!Game.GameInstance.HasGameStarted)
            {
                if (!hasBlackScreenBeenActivated && GameIntro.HasIntroBeenShown)
                {
                    BlackScreenManager.Instance.gameObject.SetActive(true);
                    hasBlackScreenBeenActivated = true;
                }
                else if (BlackScreenManager.Instance.IsTotallyBlack)
                {
                    BlackScreenManager.Instance.Deactivate();
                    Game.GameInstance.HasGameStarted = true;
                }

                yield return null;
            }
        }
    }
}