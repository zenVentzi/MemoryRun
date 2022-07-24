using System.Collections;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Games.Timings
{
    public abstract class AbstractTime : MyMono
    {
        #region variables

        private bool CanUpdateTime
        {
            get
            {
                return Game.GameInstance != null && (Game.GameInstance.HasGameStarted && !Game.GameInstance.GameOver &&
                                                     !IsTimePaused);
            }
        }

        private bool IsTimePaused { get; set; }

        #region fields

// ReSharper disable InconsistentNaming
        public float time = 10;
// ReSharper restore InconsistentNaming

        #endregion

        #region props

        public float TimeProp
        {
            get { return time; }
            set
            {
                time = value;
                Txt.text = Helper.GetFormattedTime(Mathf.CeilToInt(time));
            }
        }

        public static AbstractTime Instance { get; private set; }
        #endregion

        #endregion

        #region methods

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
        }

        [UsedImplicitly]
        private void Start()
        {
            StartCoroutine(StartGame());
        }

        [UsedImplicitly]
        private void Update()
        {
            if (CanUpdateTime)
            {
                UpdateTiming();
            }
        }

        protected virtual IEnumerator StartGame()
        {
            yield return null;
        }

        protected virtual void UpdateTiming()
        {
        }

        public void Pause()
        {
            IsTimePaused = true;
        }

        public void Resume()
        {
            IsTimePaused = false;
        }

        #endregion
    }
}