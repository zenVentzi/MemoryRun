using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using JetBrains.Annotations;
using Soomla.Store;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class GetCoinsWithMoneyButton : OrderCoinsButton
    {
        protected override bool Available
        {
            get { return true; }
        }

        private static bool StoreEventsInitialized;

        [UsedImplicitly]
        private void Awake()
        {
            if (StoreEventsInitialized) return;
            InitStoreEvents();
        }
        private void InitStoreEvents()
        {
            StoreEvents.OnItemPurchased += OnItemPurchased;

            StoreEvents.OnItemPurchaseStarted += item =>
            {
                Debug.Log("SOOMLA OnItemPurchaseStarted Enzi 1.0");
                DeactivateButtons();
            };

            StoreEvents.OnUnexpectedErrorInStore += s =>
            {
                Debug.Log("SOOMLA OnUnexpectedErrorInStore Enzi 1.0  " + s);
                ActivateButtons();
            };

            StoreEvents.OnMarketPurchaseCancelled += item =>
            {
                Debug.Log("SOOMLA OnMarketPurchaseCancelledd Enzi 1.0");
                ActivateButtons();
            };

            StoreEvents.OnBillingNotSupported +=
                () => Debug.Log("SOOMLA OnBillingNotSupported Enzi 1.0");

            StoreEventsInitialized = true;
        }

        private void OnItemPurchased(PurchasableVirtualItem purchasableVirtualItem, string payload)
        {
            Debug.Log("SOOMLA OnItemPurchased Enzi 1.0");

            ActivateButtons();
            var coinsAsString = payload.Substring(5);
            var coins = int.Parse(coinsAsString);
            CoinsManager.Add(coins);
            Tr.parent.parent.parent.SendMessage("UpdateUi");
        }

        protected override void Purchase()
        {
#if !UNITY_EDITOR
            StoreInventory.BuyItem(name, name);
#else
            var coinsAsString = name.Substring(5);
            var coins = int.Parse(coinsAsString);
            CoinsManager.Add(coins);
            Tr.parent.parent.parent.SendMessage("UpdateUi");
#endif
        }
    }
}
