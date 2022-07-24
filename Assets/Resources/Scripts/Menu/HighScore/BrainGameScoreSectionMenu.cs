using Assets.Resources.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Menu.HighScore
{
    public class BrainGameScoreSectionMenu : ScoreSectionMenu
    {
        private Transform sectionBar;
        private string sectionName;

        protected override void Start()
        {
            base.Start();
            PrintSectionLevel();
        }

        protected override void Initialize()
        {
            base.Initialize();
            sectionBar = GameObjectManager.GetGoInChildren(Go, "All").transform.GetChild(0);
            sectionName = Go.name.Substring(0, Go.name.LastIndexOf("ScoreMenu"));
        }

        protected override void MoveBars()
        {
            base.MoveBars();
            int sectionBarOffset = Experience.GetSectionBar(sectionName);
            sectionBar.transform.localPosition =
                new Vector3(sectionBar.transform.localPosition.x + sectionBarOffset,
                    sectionBar.transform.localPosition.y);
        }

        private void PrintSectionLevel()
        {
            var level = GameObjectManager.GetGoInChildren(Go, "Level").GetComponent<Text>();
            level.text = "Skill  " + Experience.GetSectionLevel(sectionName);
        }
    }
}