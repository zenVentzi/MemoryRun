using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu.HighScore
{
    public class ScoreSectionMenu : ScrollableMenu
    {
        public const int FullGameExpBar = 686;

        private List<GameObject> Games { get; set; }

        [UsedImplicitly]
        protected override void Start()
        {
            base.Start();
            Initialize();
            MoveBars();
        }

        protected virtual void MoveBars()
        {
            foreach (var game in Games)
            {
                var gameName = game.name.Substring(0, game.name.LastIndexOf("Stats"));
                var barOffset = Experience.GetGameBar(gameName);
                var gameBar = game.transform.GetChild(0);
                gameBar.localPosition = new Vector3(gameBar.localPosition.x + barOffset, gameBar.localPosition.y);
            }
        }

        protected virtual void Initialize()
        {
            Games = new List<GameObject>();

            for (int i = 0; i < Tr.childCount; i++)
            {
                var current = Tr.GetChild(i);

                if (current.name.Contains("Stats"))
                {
                    Games.Add(current.gameObject);
                }
            }
        }
    }
}