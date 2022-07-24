using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.General.Managers
{
    public class BlackScreenManager : MyMono
    {
        public static BlackScreenManager Instance { get; private set; }

        public bool IsTotallyBlack { get; private set; }

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
        }

        [UsedImplicitly]
        private void Start()
        {
            Go.SetActive(false);
        }

        private IEnumerator Show()
        {
            var color = SpriteRend.color;
            color.a = 0;
            SpriteRend.color = color;

            while (SpriteRend.color.a < 1)
            {
                color.a += Time.deltaTime;
                SpriteRend.color = color;
                yield return null;
            }

            IsTotallyBlack = true;
        }

        public void Deactivate()
        {
            IsTotallyBlack = false;
            gameObject.SetActive(false);
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            Reset();
        }

        private void Reset()
        {
            IsTotallyBlack = false;
            StartCoroutine(Show());
        }
    }
}