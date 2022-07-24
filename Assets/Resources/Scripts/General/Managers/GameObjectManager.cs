using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts.General
{
    public class GameObjectManager : ScriptableObject
    {
        private static GameObject mainMenu;

        public static void DeactivateMenu()
        {
            if (null == mainMenu)
            {
                mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
            }

            mainMenu.SetActive(false);
        }

        public static void ActivateMenu()
        {
            mainMenu.SetActive(true);
        }

        public static GameObject GetGoInChildren(GameObject root, string name)
        {
            var transforms = new Queue<Transform>();
            transforms.Enqueue(root.transform);

            while (transforms.Count > 0)
            {
                var current = transforms.Dequeue();

                if (current.name == name && current != root.transform)
                {
                    return current.gameObject;
                }

                foreach (Transform transf in current)
                {
                    transforms.Enqueue(transf);
                }
            }

            return null;
        }

        public static GameObject[] GetChildren(GameObject parent)
        {
            var queue = new Queue<Transform>();
            var children = new List<GameObject>();
            queue.Enqueue(parent.transform);
            while (queue.Count > 0)
            {
                Transform current = queue.Dequeue();
                children.Add(current.gameObject);

                foreach (Transform transf in current)
                {
                    queue.Enqueue(transf);
                }
            }

            return children.ToArray();
        }
    }
}