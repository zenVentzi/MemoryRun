using System.Collections;
using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.Games.Timings;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ
{
    public abstract class BrainGame : Game
    {
        #region variables

        private int maxNumOfMistakes = 3;

        protected int MaxNumOfMistakes
        {
            set { maxNumOfMistakes = value; }
        }

        //protected bool ShowCorrectIndicator { private get; set; }

        #region fields

        private int numOfMistakes;

        protected GameButton ClickedBtn;
        #endregion

        #endregion

        #region methods
        private IEnumerator ImgPopOut(bool isCorrect)
        {
            var img =
                Instantiate(
                    UnityEngine.Resources.Load<GameObject>("Prefabs/BrainZ/" + (isCorrect ? "Correct" : "Incorrect")));
            img.transform.parent = Tr;

            while (img.GetComponent<Animation>().IsPlaying("ImgPopOut"))
            {
                yield return null;
            }

            Destroy(img);
        }

        protected virtual void GenerateNew()
        {
        }

        protected virtual void ValidateIncorrect()
        {
            numOfMistakes++;
            var brainGameScore = ScoreRef as BrainGameScore;
            if (brainGameScore != null) brainGameScore.ResetAdditionalPoints();

            if (numOfMistakes == maxNumOfMistakes)
            {
                GameOverMenu.Load();
            }
            else
            {
                StartCoroutine(ImgPopOut(false));
                EffectsAudioManager.Instance.PlayIncorrect();
            }
        }

        protected virtual void ValidateCorrect()
        {
            AbstractTime.Instance.Go.GetComponent<TimeLeft>().AddTime();

            var brainGameScore = ScoreRef as BrainGameScore;
            if (brainGameScore != null) brainGameScore.AddAdditionalPoints();

            StartCoroutine(ImgPopOut(true));
            EffectsAudioManager.Instance.PlayCorrect();
        }

        [UsedImplicitly]
        protected virtual void OnGameButtonClick(GameButton clickedButton)
        {
            ClickedBtn = clickedButton;
        }

        protected override void OnGameBegin()
        {
            base.OnGameBegin();
            GenerateNew();
        }
        #endregion
    }
}