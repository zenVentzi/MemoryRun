using Assets.Resources.Scripts.Games.Run;
using JetBrains.Annotations;

namespace Assets.Resources.Scripts.Menu.RunMenu
{
    public class LoadRunGameSceneButton : LoadGameSceneButton
    {
        [UsedImplicitly]
        private void Awake()
        {
            LevelName = "RunScene";
        }

        protected override void Load()
        {
            base.Load();
            RunGame.IsPreview = Go.name.Contains("View");
        }
    }
}