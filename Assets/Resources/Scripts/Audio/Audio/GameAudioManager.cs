using System.Collections;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Audio.Audio
{
    public class GameAudioManager : MyMono
    {
        public static GameAudioManager Instance;

        [SerializeField]
        private AudioClip clip;

        private Coroutine turnOnCoroutine,
                          turnOffCoroutine;

        private int turnOffTime;

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
        }

        private IEnumerator TurnOff()
        {
            while (Audioo.volume > 0)
            {
                turnOffTime = Audioo.timeSamples / clip.frequency;
                Audioo.volume -= .05f;
                yield return null;
            }

            Audioo.Stop();
        }

        private IEnumerator TurnOn()
        {
            Audioo.timeSamples = turnOffTime * clip.frequency;
            Audioo.volume = 0;
            Audioo.Play();

            while (Audioo.volume < 1)
            {
                Audioo.volume += .005f;
                yield return null;
            }
        }

        public void Play()
        {
            if (turnOffCoroutine != null)
                StopCoroutine(turnOffCoroutine);

            turnOnCoroutine = StartCoroutine(TurnOn());
        }

        public void Stop()
        {
            if (turnOnCoroutine != null)
                StopCoroutine(turnOnCoroutine);

            turnOffCoroutine = StartCoroutine(TurnOff());
        }
    }
}
