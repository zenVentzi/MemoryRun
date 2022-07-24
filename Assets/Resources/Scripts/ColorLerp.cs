using System;
using System.Collections;
using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Resources.Scripts
{
    public class ColorLerp : MyMono
    {
        private Color currentColor,
                      nextColor;

        private float startSpeed;

        [SerializeField] 
        private float speed = .8f;

        protected float LerpSpeed
        {
            get { return speed; }
            set { speed = value; }
        }

        protected Color NextColor
        {
            get { return nextColor; }
            set { nextColor = value; }
        }

        private bool DifferentFromNextColor
        {
            get { return nextColor != (currentColor = Color.Lerp(currentColor, nextColor, speed * Time.deltaTime)); }
        }

        [UsedImplicitly]
        protected virtual void Start()
        {
            startSpeed = speed;
            StartCoroutine(Lerp());
        }

        private bool AnyRgbValueReached()
        {
            return (Math.Abs(currentColor.r - nextColor.r) < .01f ||
                    Math.Abs(currentColor.g - nextColor.g) < .01f ||
                    Math.Abs(currentColor.b - nextColor.b) < .01f);
        }

        protected virtual IEnumerator Lerp()
        {
            while (!Game.GameInstance.GameOver)
            {
                speed = startSpeed;
                currentColor = SpriteRend.color;

                float r = Random.Range(0f, 1f),
                      g = Random.Range(0f, 1f),
                      b = Random.Range(0f, 1f),
                      a = Random.Range(1f, 1f);
                nextColor = new Color(r, g, b, a);

                while (DifferentFromNextColor && nextColor != default(Color))
                {
                    if (AnyRgbValueReached())
                    {
                        speed += 2f;
                    }

                    SpriteRend.color = currentColor;
                    yield return null;
                }
            }
        }
    }
}