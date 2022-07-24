using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Audio.Audio
{
    public class EffectsAudioManager : MyMono
    {
        public static EffectsAudioManager Instance;
        [SerializeField, UsedImplicitly] private AudioClip click;
        [SerializeField, UsedImplicitly] private AudioClip correct;
        [SerializeField, UsedImplicitly] private AudioClip incorrect;
        [SerializeField, UsedImplicitly] private AudioClip explosion;
        [SerializeField, UsedImplicitly] private AudioClip gameOver;

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
        }

        public void PlayClick()
        {
            Audioo.clip = click;
            Audioo.Play();
        }

        public void PlayCorrect()
        {
            Audioo.clip = correct;
            Audioo.Play();
        }

        public void PlayIncorrect()
        {
            Audioo.clip = incorrect;
            Audioo.Play();
        }

        public void PlayExplosion()
        {
            Audioo.clip = explosion;
            Audioo.Play();
        }

        public void PlayGameOver()
        {
            Audioo.clip = gameOver;
            Audioo.Play();
        }
    }
}
