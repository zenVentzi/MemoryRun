using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class LeaderboardsButton : AnimatedButton
    {
        protected override void OnClick()
        {
            base.OnClick();

            if (Social.localUser.authenticated)
            {
                MyLeaderboardsManager.UpdateLeaderboards();
                Social.ShowLeaderboardUI();
            }
            else
            {
                Social.localUser.Authenticate(success =>
                {
                    if (!success) return;

                    MyLeaderboardsManager.UpdateLeaderboards();
                    Social.ShowLeaderboardUI();
                });
            }
        }
    }
}
