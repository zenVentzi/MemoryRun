using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games
{
    public class Game : MyMono
    {
        private bool gameOver,
            hasGameStarted;

        private Score score;

        protected GameObject Canvas
        {
            get { return GameObjectManager.GetGoInChildren(Go, "Canvas"); }
        }

        public string GameName
        {
            get { return Go.name.Substring(0, Go.name.LastIndexOf("Game")); }
        }

        public bool IsGameRunning { get; set; }

        public virtual bool GameOver
        {
            get { return gameOver; }
            set
            {
                if (value)
                {
                    IsGameRunning = false;
                }

                gameOver = value;
            }
        }

        public bool HasGameStarted
        {
            get { return hasGameStarted; }
            set
            {
                if (value)
                {
                     OnGameBegin();
                }

                hasGameStarted = value;
            }
        }

        public string GameSceneName
        {
            get
            {
                return GameName + "Scene";
            }
        }

        public Score ScoreRef
        {
            get { return score ?? (score = GetComponentInChildren<Score>()); }
        }

        public static Game GameInstance { get; private set; }

        [UsedImplicitly]
        protected void Awake()
        {
            GameInstance = this;
            CollidersManager.EnableGameColliders(enabled: false);
        }

        [UsedImplicitly]
        protected void Start()
        {
            Init();

            if (!GameIntro.HasIntroBeenShown)
            {
                Application.LoadLevelAdditive("GameIntroScene");
            }
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            GameInstance = null;
        }

        protected virtual void Init()
        {
        }

        protected virtual void OnGameBegin()
        {
            CollidersManager.EnableGameColliders();
            IsGameRunning = true;
            MenuAudioManager.Instance.Stop();
            GameAudioManager.Instance.Play();
            if (ScoreRef != null) ScoreRef.Display();
        }

        public static T GetGame<T>() where T : Game
        {
            return GameInstance as T;
        }
    }
}