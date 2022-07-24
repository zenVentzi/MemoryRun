using Assets.Resources.Scripts.General;
using JetBrains.Annotations;

namespace Assets.Resources.Scripts.Menu.HighScore
{
    public class RunLevel : MyMono
    {
        [UsedImplicitly]
        private void Start()
        {
            Txt.text = "\t Skill  " + Experience.GetGameLevel("Run");
        }
    }
}
