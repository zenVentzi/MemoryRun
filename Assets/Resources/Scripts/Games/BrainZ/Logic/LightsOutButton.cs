using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.UI.Buttons;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.BrainZ.Logic
{
    public class LightsOutButton : GameButton
    {
        [SerializeField] private Sprite lightOnSprite;
        [SerializeField] private Sprite lightOffSprite;

        private List<LightsOutButton> neighbours;
        public List<LightsOutButton> Neighbours
        {
            get { return neighbours; }
        }

        public bool LightOn
        {
            get { return SpriteRend.sprite.name.Contains("On"); }
        }

        private void ToggleLights()
        {
            SpriteRend.sprite = LightOn ? lightOffSprite : lightOnSprite;

            foreach (var neighbour in neighbours)
            {
                neighbour.SpriteRend.sprite = neighbour.LightOn ? lightOffSprite : lightOnSprite;
            }
        }

        private void Awake()
        {
            neighbours = new List<LightsOutButton>();
        }

        protected override void OnClick()
        {
            ToggleLights();
            base.OnClick();
        }

        public void TurnLightOn()
        {
            SpriteRend.sprite = lightOnSprite;
        }
    }
}
