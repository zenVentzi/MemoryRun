using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Scripts.Games.Run;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games
{
    public class GameIntro : MyMono
    {
        private static readonly List<string> UsedIntroSprites = new List<string>();
        private static string GameIntroName
        {
            get
            {
                return Game.GameInstance.GameName + "Intro";
            }
        }

        private static bool CanShowIntro()
        {
            return ScoreManager.GetHigh(Game.GameInstance.GameName) < 20 && !RunGame.IsPreview;
        }

        public static bool HasIntroBeenShown
        {
            get { return UsedIntroSprites.Contains(GameIntroName) || !CanShowIntro(); }
        }

        [UsedImplicitly]
        private void Start()
        {
            StartCoroutine(WatchIntro());
        }

        private IEnumerator WatchIntro()
        {
            var introSprite = UnityEngine.Resources.Load<Sprite>("Textures/Games/Intros/" + GameIntroName);
            SpriteRend.sprite = introSprite;
            Anim.Play("IntroFadeIn");

            while (!HasIntroBeenShown)
            {
                if ((InputManager.JustReleased() || Input.GetKeyUp(KeyCode.Escape)))
                {
                    Anim.Play("IntroFadeOut");
                    yield break;
                }

                yield return null;
            }
        }

        [UsedImplicitly]
        private IEnumerator OnDisappearAnimFinish()
        {
            UsedIntroSprites.Add(GameIntroName);

            yield return new WaitForSeconds(1);

            UsedIntroSprites.RemoveAt(UsedIntroSprites.Count - 1);
            Destroy(Go);
        }
    }
}