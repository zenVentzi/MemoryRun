using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public class SetParent : MyMono
    {
        [UsedImplicitly]
        protected virtual void Awake()
        {
            //Tr.parent = GameObject.Find(ParentName).transform;
            Tr.parent = LoadMenuSceneButton.LastClicked.Tr.parent;
        }
    }
}