using System.Collections;
using Assets.Resources.Scripts.Audio.Audio;
using Assets.Resources.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public class SplashScreen : MyMono
    {
        private GameObject mainMenu;

        [UsedImplicitly]
        private IEnumerator Start()
        {
            yield return null;

            mainMenu = GameObject.Find("MainMenu");
            mainMenu.SetActive(false);
        }
        [UsedImplicitly]
        private void OnAppear()
        {
            mainMenu.SetActive(true);
            MenuAudioManager.Instance.Play();
            Destroy(Go);
        }
    }
}
