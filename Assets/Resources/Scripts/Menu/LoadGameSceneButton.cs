using System.Collections;
using Assets.Resources.Scripts.Games.Run;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.Menu
{
    public class LoadGameSceneButton : MenuButton
    {
        protected string LevelName { private get; set; }

        [UsedImplicitly]
        private void Awake()
        {
            LevelName = Go.name + "Scene";
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            StartCoroutine(ActivateButton());            
        }

        private IEnumerator ActivateButton()
        {
            var energy = GameEnergyManager.GetEnergy(name);
            Colider.enabled = false;

            if (energy < 1)
            {
                yield return new WaitForSeconds(GameEnergyManager.GetTimeLeftToNextRecovery(name));
            }

            Colider.enabled = true;
        }

        protected override void OnClick()
        {
            base.OnClick();
            Load();

            if (!RunGame.IsPreview)
            {
                GameEnergyManager.AddEnergy(name, -1);
                GameManager.TotalGamesPlayed++;
            }
        }

        protected virtual void Load()
        {
            Application.LoadLevelAdditive(LevelName);
            Invoke("CallDeactivateMenu", 0.1f);
        }

        [UsedImplicitly]
        private void CallDeactivateMenu()
        {
            GameObjectManager.DeactivateMenu();
        }
    }
}