using System.Collections.Generic;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public class SetUpSettings : MyMono
    {
        public static KeyValuePair<float, float> TopBottomY;

        [UsedImplicitly]
        private void Awake()
        {
            float cameraWidth = (Camera.main.orthographicSize * 2 * Camera.main.aspect),
                  cameraHeight = (Camera.main.orthographicSize * 2),
                  scaleX = cameraWidth / SpriteRend.bounds.size.x,
                  scaleY = cameraHeight / SpriteRend.bounds.size.y;

            GamePlayerPrefs.SetFloat("ScaleX", scaleX);
            GamePlayerPrefs.SetFloat("ScaleY", scaleY);
            GamePlayerPrefs.SetFloat("CameraWidth", cameraWidth);
            GamePlayerPrefs.SetFloat("CameraHeight", cameraHeight);

            Tr.localScale = new Vector3(scaleX, scaleY, 1);
            TopBottomY = new KeyValuePair<float, float>(SpriteRend.bounds.extents.y, -SpriteRend.bounds.extents.y);
        }

        //private void Resize()
        //{
        //    if (SpriteRend == null) return;

        //    transform.localScale = new Vector3(1, 1, 1);

        //    var spriteWidth = SpriteRend.sprite.bounds.size.x;
        //    var spriteHeight = SpriteRend.sprite.bounds.size.y;


        //    var worldScreenHeight = Camera.main.orthographicSize * 2f;
        //    var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        //    var scaleX  = worldScreenWidth / spriteWidth;

        //    var scaleY = worldScreenHeight / spriteHeight;
        //    transform.localScale = new Vector3(scaleX, scaleY);
        //}
    }
}