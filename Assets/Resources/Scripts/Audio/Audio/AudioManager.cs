//using Assets.Scripts.Utilities;

using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Audio.Audio
{
    public class AudioManager : MyMono
    {
        public static AudioManager Instance;

        private int Volume
        {
            get
            {
                return GamePlayerPrefs.GetInt("Sound", 1);
            }
            set
            {
                GamePlayerPrefs.SetInt("Sound", value);
                AudioListener.volume = value;
            }
        }

        public bool SoundOn
        {
            get 
            {
                return Volume == 1; 
            }
        }

        [UsedImplicitly]
        private void Awake()
        {
            Instance = this;
            AudioListener.volume = Volume;
        }

        public void Mute()
        {
            Volume = 0;
        }

        public void Unmute()
        {
            Volume = 1;
        }
    }
}