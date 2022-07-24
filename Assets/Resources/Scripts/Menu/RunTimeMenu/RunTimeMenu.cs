using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;

namespace Assets.Resources.Scripts.Menu.RunTimeMenu
{
    public class RunTimeMenu : MyMono
    {
        [UsedImplicitly]
        private void OnDisappearAnimFinish()
        {
            Go.BroadcastMessage("OnMenuDisappear");
        }
    }
}
