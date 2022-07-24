using UnityEngine;

namespace Assets.Resources.Scripts.Menu.OrdersMenu
{
    public class SetOrdersMenuParent : SetParent
    {
        private Transform FindParent()
        {
            var parent = LoadMenuSceneButton.LastClicked.Tr.parent;

            if (!parent.name.Contains("Menu"))
                parent = LoadMenuSceneButton.LastClicked.Tr.parent.parent;

            return parent;
        }

        protected override void Awake()
        {
            Tr.parent = FindParent();
        }
    }
}
