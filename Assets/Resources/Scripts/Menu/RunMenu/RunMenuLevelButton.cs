using Assets.Resources.Scripts.Games.Run;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu.RunMenu
{
    public class RunMenuLevelButton : MenuButton
    {
        #region variables

        private static RunMenuLevelButton clickedLevel;

        private GameObject demoButton;

        private bool isOnFocus;
        private GameObject playButton;

        private static bool IsPlayAvailable
        {
            get
            {
                return (int) RunGame.Difficulty <= (int) Proficiency.GetGame("Run");
            }
            //get { return true; }
        }

        #endregion

        [UsedImplicitly]
        private void Awake()
        {
            playButton = Tr.GetChild(0).gameObject;
            demoButton = Tr.GetChild(1).gameObject;
            ToggleButton();
        }

        protected override void OnClick()
        {
            base.OnClick();
            UpdateFocus();
            SelectDifficulty();
            ToggleButton();
        }

        private void SelectDifficulty()
        {
            if (Go.name.Contains("Beginner"))
            {
                RunGame.Difficulty = Proficiency.Level.Beginner;
            }
            else if (Go.name.Contains("Mediocre"))
            {
                RunGame.Difficulty = Proficiency.Level.Mediocre;
            }
            else if (Go.name.Contains("Advanced"))
            {
                RunGame.Difficulty = Proficiency.Level.Advanced;
            }
            else
            {
                RunGame.Difficulty = Proficiency.Level.Expert;
            }
        }

        private void UpdateFocus()
        {
            if (clickedLevel != null && clickedLevel != this)
                clickedLevel.RemoveFocus();

            isOnFocus = !isOnFocus;
            clickedLevel = (isOnFocus ? this : null);
        }

        [UsedImplicitly]
        private void RemoveFocus()
        {
            isOnFocus = false;
            ToggleButton();
        }

        private void ToggleButton()
        {
            if (isOnFocus)
            {
                demoButton.SetActive(true);

                if(IsPlayAvailable)
                    playButton.SetActive(true);
            }
            else
            {
                playButton.SetActive(false);
                demoButton.SetActive(false);
            }
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            if (!isOnFocus) return;
            ToggleButton();
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            RunGame.IsPreview = false;
            clickedLevel = null;
        }
    }
}