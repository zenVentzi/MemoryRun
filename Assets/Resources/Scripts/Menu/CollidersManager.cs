using System.Collections.Generic;
using System.Linq;
using Assets.Resources.Scripts.Games;
using Assets.Resources.Scripts.General;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public static class CollidersManager
    {
        private static readonly List<GameObject> DeactivatedMenuObjects;

        static CollidersManager()
        {
            DeactivatedMenuObjects = new List<GameObject>();
        }

        public static bool IsObjectInArea(this GameObject obj, GameObject area)
        {
            var topLeft = new Vector2(obj.transform.position.x - obj.GetComponent<Collider2D>().bounds.extents.x,
                obj.transform.position.y + obj.GetComponent<Collider2D>().bounds.extents.y);
            var bottomRight = new Vector2(obj.transform.position.x + obj.GetComponent<Collider2D>().bounds.extents.x,
                obj.transform.position.y - obj.GetComponent<Collider2D>().bounds.extents.y);
            var topRight = new Vector2(obj.transform.position.x + obj.GetComponent<Collider2D>().bounds.extents.x,
                obj.transform.position.y + obj.GetComponent<Collider2D>().bounds.extents.y);
            var bottomLeft = new Vector2(obj.transform.position.x - obj.GetComponent<Collider2D>().bounds.extents.x,
                obj.transform.position.y - obj.GetComponent<Collider2D>().bounds.extents.y);

            return (area.GetComponent<Collider2D>().OverlapPoint(topLeft) && area.GetComponent<Collider2D>().OverlapPoint(topRight) &&
                    area.GetComponent<Collider2D>().OverlapPoint(bottomLeft) && area.GetComponent<Collider2D>().OverlapPoint(bottomRight));
        }

        public static void EnableGameColliders(GameObject exludedOne = null, bool enabled = true)
        {
            EnableColliders(Game.GameInstance.Go, exludedOne, enabled);
        }

        public static void EnableColliders(GameObject go, GameObject excludedOne = null, bool enable = true)
        {
            var children = GameObjectManager.GetChildren(go);

            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != excludedOne && children[i].GetComponent<Collider2D>() != null)
                {
                    children[i].GetComponent<Collider2D>().enabled = enable;
                }
            }
        }

        public static bool AreCollidersActivated(GameObject go)
        {
            //return go.transform.Cast<Transform>().All(tr => null == tr.GetComponent<Collider2D>() || tr.GetComponent<Collider2D>().enabled);

            foreach (Transform child in go.transform)
            {
                if (null != child.GetComponent<Collider2D>() && !child.GetComponent<Collider2D>().enabled)
                {
                    return false;                    
                }
            }

            return true;
        }

        public static void EnableMenuColliders(this MyMono obj, bool enable = true)
        {
            if (enable)
            {
                EnableColliders(DeactivatedMenuObjects.Last());
                DeactivatedMenuObjects.RemoveAt(DeactivatedMenuObjects.Count - 1);
            }
            else
            {
                EnableColliders((obj as LoadMenuSceneButton).CurrentMenu, enable: false);
                DeactivatedMenuObjects.Add((obj as LoadMenuSceneButton).CurrentMenu);
            }
        }
    }
}