using GooglePlayGames;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.General
{
    public class GPlayManager : MonoBehaviour
    {
        [UsedImplicitly]
        void Start()
        {
            PlayGamesPlatform.Activate();
            GooglePlayGames.OurUtils.Logger.DebugLogEnabled = true;
        }
    }
}
