using System;
using Assets.Resources.Scripts.General;
using Assets.Resources.Scripts.General.Managers;
using Assets.Resources.Scripts.Menu;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Resources.Scripts.UI.Buttons
{
    public class OrderProductButton : OrderButton
    {
        protected override bool Available
        {
            get
            {
                //Debug.Log(costInCoins + " " + CoinsManager.GetAmount());
                return costInCoins <= CoinsManager.GetAmount();
            }
        }

        [SerializeField, UsedImplicitly]
        private int costInCoins;

        protected override void Purchase()
        {
            var gameName = ((GameOrdersButton) LoadMenuSceneButton.LastClicked).GameName;
            if (GameEnergyManager.Unlimited(gameName) || !Available) return;

            CoinsManager.Rid(costInCoins);
            var secondPartOfName = name.Substring(6);

            try
            {
                var additionalEnergy = int.Parse(secondPartOfName);
                GameEnergyManager.AddEnergy(gameName, additionalEnergy);
            }
            catch (Exception)
            {
                switch (name)
                {
                    case "Unlimited":
                        {
                            GameEnergyManager.AddEnergy(gameName, int.MaxValue);
                            break;
                        }
                    case "AutoRecoveryLimitx3":
                        {
                            GameEnergyManager.BoostAutoRecoveryLimit(gameName, 3);
                            GameObjectManager.DeactivateMenu();
                            GameObjectManager.ActivateMenu();
                            break;
                        }
                    case "AutoRecoveryLimitx4":
                        {
                            GameObjectManager.DeactivateMenu();
                            GameObjectManager.ActivateMenu();
                            GameEnergyManager.BoostAutoRecoveryLimit(gameName, 4);
                            break;
                        }
                    case "CooldownReduction":
                        {
                            GameEnergyManager.ReduceCooldown(gameName);
                            break;
                        }
                }
            }

            Tr.parent.parent.SendMessage("UpdateUi");
        }
    }
}
