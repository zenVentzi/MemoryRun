using System.Collections;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using Assets.Resources.Scripts.UI.Buttons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Menu.OrdersMenu
{
    public class OrdersMenu : ScrollableMenu
    {
        [SerializeField, UsedImplicitly] private GameObject gameIcon;

        private Text EnergyText
        {
            get 
            {
                return GameObjectManager.GetGoInChildren(Go, "EnergyText").GetComponent<Text>();
            }
        }

        private Text CoinsText
        {
            get
            {
                return GameObjectManager.GetGoInChildren(Go, "CoinsText").GetComponent<Text>();
            }
        }

        private Text CooldownText
        {
            get
            {
                return GameObjectManager.GetGoInChildren(Go, "CooldownTime").GetComponent<Text>();
            }
        }

        private Text RecoveryLimitText
        {
            get
            {
                return GameObjectManager.GetGoInChildren(Go, "RecoveryLimit").GetComponent<Text>();
            }
        }

        private string CurrentGameName
        { get { return Tr.parent.name; } }

        [UsedImplicitly]
        protected override void Awake()
        {
            base.Awake();
            gameIcon.GetComponent<SpriteRenderer>().sprite = ((GameOrdersButton)LoadMenuSceneButton.LastClicked).GameIcon;            
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            UpdateUi();
        }

        [UsedImplicitly]
        private void UpdateUi()
        {
            EnergyText.text = GameEnergyManager.GetEnergy(CurrentGameName).ToString();
            CoinsText.text = CoinsManager.GetAmount().ToString();
            CooldownText.text = Helper.GetFormattedTime(GameEnergyManager.GetTotalCooldownTime(CurrentGameName), true);
            RecoveryLimitText.text = GameEnergyManager.GetAutoRecoveryLimit(CurrentGameName).ToString();
        }
    }
}
