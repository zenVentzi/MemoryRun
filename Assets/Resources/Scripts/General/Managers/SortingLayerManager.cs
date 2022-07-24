using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.General.Managers
{
    public class SortingLayerManager : MonoBehaviour
    {
        private static readonly Dictionary<string, int> SortingLayers = new Dictionary<string, int>(10);

        [UsedImplicitly]
        private void OnEnable()
        {
            SortingLayers.Add("FirstLayer", 0);
            SortingLayers.Add("SecondLayer", 1);
            SortingLayers.Add("ThirdLayer", 2);
            SortingLayers.Add("FourthLayer", 3);
            SortingLayers.Add("FifthLayer", 4);
            SortingLayers.Add("SixthLayer", 5);
            SortingLayers.Add("SeventhLayer", 6);
        }

        private static int Compare(GameObject gameObject1, GameObject gameObject2)
        {
            string layer1,
                layer2;

            try
            {
                layer1 = gameObject1.GetComponent<Renderer>().sortingLayerName;
                layer2 = gameObject2.GetComponent<Renderer>().sortingLayerName;
            }
            catch (Exception)
            {
                return gameObject1.transform.position.z.CompareTo(gameObject2.transform.position.z);
            }


            if (SortingLayers[layer1].CompareTo(SortingLayers[layer2]) == 0)
            {
                return gameObject1.GetComponent<Renderer>().sortingOrder.CompareTo(gameObject2.GetComponent<Renderer>().sortingOrder);
            }

            return SortingLayers[layer1].CompareTo(SortingLayers[layer2]);
        }

        public static bool IsTopmost([NotNull] GameObject go, bool belowMouse = true)
        {
            if (go == null) throw new ArgumentNullException("go");

            Vector3 wp = belowMouse ? Camera.main.ScreenToWorldPoint(Input.mousePosition)
                : go.transform.position;
            wp.z = Camera.main.transform.position.z;
            var hits = Physics2D.RaycastAll(wp, Vector2.zero,
                                                Vector3.Distance(Camera.main.transform.position, go.transform.position)*2);

            if (hits.Length == 0)
            {
                return false;
            }

            GameObject topMostSoFar = hits[0].collider.gameObject;

            for (int i = 1; i < hits.Length; i++)
            {
                var hit = hits[i];

                if (Compare(topMostSoFar, hit.collider.gameObject) == -1)
                {
                    topMostSoFar = hit.collider.gameObject;
                }
            }

            return topMostSoFar == go;
        }

        public static GameObject GetTopmostBelowMouse()
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var allHits = Physics2D.RaycastAll(wp, Vector2.zero,
                Vector3.Distance(Camera.main.transform.position, Vector3.zero) * 2);

            return allHits.Where(hit => IsTopmost(hit.collider.gameObject)).Select(hit => hit.collider.gameObject).FirstOrDefault();
        }
    }
}