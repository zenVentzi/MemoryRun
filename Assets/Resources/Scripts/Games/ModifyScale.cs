using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games
{
    public class ModifyScale : MyMono
    {
        [SerializeField] private bool isChild = false;

        [UsedImplicitly]
        private void Start()
        {
            Modify();
        }

        private void Modify()
        {
            Tr.localScale = isChild
                  ? Vector3.one
                  : new Vector3(GamePlayerPrefs.GetFloat("ScaleX"), GamePlayerPrefs.GetFloat("ScaleY"), 1);

            //if (SpriteRend == null) return;

            //transform.localScale = new Vector3(1, 1, 1);

            //float width = SpriteRend.sprite.bounds.size.x;
            //float height = SpriteRend.sprite.bounds.size.y;


            //float worldScreenHeight = Camera.main.orthographicSize * 2f;
            //float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            //Vector3 xWidth = transform.localScale;
            //xWidth.x = worldScreenWidth / width;
            //transform.localScale = xWidth;

            //Vector3 yHeight = transform.localScale;
            //yHeight.y = worldScreenHeight / height;
            //transform.localScale = yHeight;
        }
    }
}