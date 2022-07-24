using Assets.Resources.Scripts.General;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Menu.HighScore
{
    public class ScoreMainMenu : ScrollableMenu
    {
        protected override void Start()
        {
            base.Start();
            UpdateUi();
        }

        private void UpdateUi()
        {
            UpdateBagdes();
            UpdateLevel();
            UpdateExp();
        }

        private void UpdateBagdes()
        {
            for (int i = 0; i < Tr.childCount - 6; i++)
            {
                var sectionName = Tr.GetChild(i).name.Substring(0, Tr.GetChild(i).name.LastIndexOf("Score"));
                Tr.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = Badge.GetSection(sectionName);
            }
        }

        private void UpdateLevel()
        {
            var level = GameObjectManager.GetGoInChildren(Go, "Level").GetComponent<Text>();
            level.text = "Skill  " + Experience.GetWholeGameLevel();
        }

        private void UpdateExp()
        {
            var expBar = GameObjectManager.GetGoInChildren(Go, "Percent").transform;
            expBar.localPosition = new Vector3(expBar.localPosition.x + Experience.GetWholeGameBar(), expBar.localPosition.y);
        }
    }
}