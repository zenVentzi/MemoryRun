using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public class SoundButton : AnimatedButton
    {
        private Sprite soundOffSprite;
        private Sprite soundOnSprite;

        private bool isSoundOnSprite;

        [UsedImplicitly]
        private void Start()
        {
            Init();
            SetSpriteBeginning();
        }

        protected override void OnClick()
        {
            base.OnClick();
            ToggleSprite();
        }

        private void Init()
        {
            soundOffSprite = UnityEngine.Resources.LoadAll<Sprite>("Textures/Menu/Main/MainMenuButtons")[3];
            soundOnSprite = UnityEngine.Resources.LoadAll<Sprite>("Textures/Menu/Main/MainMenuButtons")[4];            
        }

        private void ToggleSprite()
        {
            if (isSoundOnSprite)
            {
                AudioManager.Instance.Mute();
                SpriteRend.sprite = soundOffSprite;
            }
            else
            {
                AudioManager.Instance.Unmute();
                SpriteRend.sprite = soundOnSprite;
            }

            isSoundOnSprite = !isSoundOnSprite;
        }

        private void SetSpriteBeginning()
        {
            SpriteRend.sprite = AudioManager.Instance.SoundOn
                ? soundOnSprite
                : soundOffSprite;

            isSoundOnSprite = AudioManager.Instance.SoundOn;
        }
    }
}