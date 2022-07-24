using JetBrains.Annotations;
using Soomla.Store;
using UnityEngine;

namespace Assets.Resources.Scripts.General
{
    public class SoomlaBilling : MonoBehaviour
    {
        [UsedImplicitly]
        void Start()
        {
            SoomlaStore.Initialize(new MemoryRunAssetsStore());
        }
    }
}
