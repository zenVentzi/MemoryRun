using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public class LoadMenuSceneButton : MenuButton
    {
        [SerializeField, UsedImplicitly] private GameObject currentMenu;

        public GameObject CurrentMenu
        { get { return currentMenu; } }

        public static LoadMenuSceneButton LastClicked { get; private set; }
        protected override void OnClick()
        {
            base.OnClick();
            LastClicked = this;
            
            Application.LoadLevelAdditive(Tr.name + "Scene");
            CurrentMenu.SendMessage("Deactivate", SendMessageOptions.DontRequireReceiver);
            this.EnableMenuColliders(false);
        }
    }
}