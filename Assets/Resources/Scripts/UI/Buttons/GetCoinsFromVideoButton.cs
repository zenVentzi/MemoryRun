using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using ChartboostSDK;
using JetBrains.Annotations;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class GetCoinsFromVideoButton : OrderCoinsButton
    {
        protected override bool Available
        {
            get { return AdsManager.HasVideo; }
        }

        [UsedImplicitly]
        private void Start()
        {
            Chartboost.didCompleteRewardedVideo += (location, reward) =>
            {
                CoinsManager.Add(reward);
                Tr.parent.SendMessage("UpdateUi");
            };
        }

        protected override void Purchase()
        {
            AdsManager.ShowVideo();
        }
    }
}
