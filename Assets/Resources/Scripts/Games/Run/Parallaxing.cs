using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Games.Run
{
    public class Parallaxing : ScrollingRunObj
    {
        private static int numberInARow = 1;
        private GameObject buddy;

        [UsedImplicitly]
        protected override void Start()
        {
            base.Start();
            SpeedMultiplier = 40;
            StartCoroutine(AddNext());
        }

        private IEnumerator AddNext()
        {
            while (true)
            {
                if (CanAddNext)
                {
                    buddy =
                        Instantiate(Tr.gameObject,
                            new Vector3(Tr.position.x + SpriteRend.bounds.size.x * .95f, Tr.position.y, Tr.position.z),
                            Quaternion.identity) as GameObject;
                    OnAddNew();
                }

                yield return null;
            }
        }

        private bool CanAddNext
        {
            get { return Tr.position.x <= 500 && buddy == null && Recording; }
        }

        private void OnAddNew()
        {
            buddy.transform.parent = Tr.parent;
            buddy.transform.localScale = Tr.localScale;
            buddy.name = buddy.name.Substring(0, 3) + numberInARow;
            numberInARow++;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            numberInARow--;
        }
    }
}