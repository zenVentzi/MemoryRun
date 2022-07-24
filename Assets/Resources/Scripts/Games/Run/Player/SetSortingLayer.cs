using Assets.Resources.Scripts.General;
using JetBrains.Annotations;

namespace Assets.Resources.Scripts.Games.Run.Player
{
    public class SetSortingLayer : MyMono
    {
        public SortingLayers SortingLayer;

        [UsedImplicitly]
        private void Start()
        {
            //Tr.GetComponent<UnityEngine.ParticleSystem>().GetComponent<UnityEngine.Renderer>().sortingLayerName = SortingLayer.ToString();
            //TODO: delete if player explosion works
        }
    }
}