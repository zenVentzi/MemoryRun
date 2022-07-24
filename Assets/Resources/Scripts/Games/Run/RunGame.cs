using System.Collections;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.Menu.RunTimeMenu;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.Run
{
    public class RunGame : Game
    {
        #region variables

        [SerializeField]
        private Sprite background;

        private bool canUpdateFinishedGames;
        public const float Speed = 12f;

        public static Proficiency.Level Difficulty;
        public static bool IsPreview;

        public void ScaleChildren()
        {
            var spriteWidth = background.bounds.size.x;
            var spriteHeight = background.bounds.size.y;

            var worldScreenHeight = Camera.main.orthographicSize * 2f;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            var scale = new Vector2(worldScreenWidth/spriteWidth, worldScreenHeight/spriteHeight);

            for (int i = 0; i < Tr.childCount; i++)
            {
                Tr.GetChild(i).localScale = scale;
            }
        }

        public override bool GameOver
        {
            get { return base.GameOver; }
            set
            {
                if (value)
                {
                    GameSpeed = 0;
                }

                base.GameOver = value;
            }
        }

        public float GameSpeed { get; private set; }

        #endregion

        [UsedImplicitly]
        private void Update()
        {
            if (IsPreview && Input.GetKeyUp(KeyCode.Escape) && !GameOver)
                GameOverMenu.Load();
        }

        private void LoadLevels()
        {
            var difficulty = Difficulty.ToString();
            var path = "Prefabs/Run/Levels/" + difficulty + "/LevelManager_" + difficulty;

            var levelManager = Instantiate(UnityEngine.Resources.Load<GameObject>(path));
            levelManager.transform.parent = GameObjectManager.GetGoInChildren(Go, "Platforms").transform;
            levelManager.name = levelManager.name.Substring(0, levelManager.name.LastIndexOf("(Clone)"));
            levelManager.transform.localScale = Vector3.one;
        }

        private IEnumerator StartGame()
        {
            while (!GameIntro.HasIntroBeenShown)
            {
                yield return null;
            }

            yield return new WaitForEndOfFrame();
            HasGameStarted = true;
        }

        protected override void Init()
        {
            base.Init();
            ScaleChildren();
            LoadLevels();
            StartCoroutine(StartGame());
            canUpdateFinishedGames = !IsPreview;
        }

        protected override void OnGameBegin()
        {
            base.OnGameBegin();

            GameSpeed = Speed;
            GetComponentInChildren<RewindButton>().Ignite();
        }
    }
}