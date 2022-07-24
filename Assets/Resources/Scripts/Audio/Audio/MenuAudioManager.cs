using System.Collections;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Audio.Audio
{
    public class MenuAudioManager : MyMono
    {
        public static MenuAudioManager Instance;

        [SerializeField, UsedImplicitly]
        private AudioClip clip;

        private int turnOffTime;

        private Coroutine turnOffCoroutine;

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
                Audioo.volume -= .02f;
                yield return null;
            }

            Audioo.Stop();
        }

        private IEnumerator TurnOn(float delay)
        {
            if (delay > 0)
                yield return new WaitForSeconds(delay);

            Audioo.timeSamples = turnOffTime * clip.frequency;
            Audioo.volume = 0;
            Audioo.Play();

            while (Audioo.volume < 1)
            {
                Audioo.volume += .005f;
                yield return null;
            }
        }

        public void Play(float delay = 0)
        {
            if (turnOffCoroutine != null)
            {
                StopCoroutine(turnOffCoroutine);
            }

            //turnOnCoroutine = StartCoroutine(TurnOn(delay));
            StartCoroutine(TurnOn(delay));
        }

        public void Stop()
        {
            if (Audioo.isPlaying)
                turnOffCoroutine = StartCoroutine(TurnOff());
        }
    }
}
