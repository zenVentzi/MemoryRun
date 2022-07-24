using Assets.Resources.Scripts.Menu;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class GameOrdersButton : LoadMenuSceneButton
    {
        [SerializeField, UsedImplicitly] private Sprite gameIcon;

        public string GameName
        {
            get
            {
                var tempName = Tr.parent.name.Substring(0, Tr.parent.name.LastIndexOf("Energy"));

                if (tempName.Length < 3)
                    tempName = Tr.parent.parent.name;                

                return tempName;
            } 
        }

        public Sprite GameIcon
        {
            get { return gameIcon; }
        }
    }
}
