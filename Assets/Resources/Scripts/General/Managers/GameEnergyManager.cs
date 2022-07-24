using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.General.Managers
{
    public class GameEnergyManager : MyMono
    {
        #region static

        private const int InitialEnergy = 500;
        private const int InitialCooldownTime = 7200;//seconds
        private const int InitialAutoRecoveryLimit = 10;
        public static int GetTotalCooldownTime(string gameName)
        {
            return GamePlayerPrefs.GetInt(gameName + "TotalCooldownTime", InitialCooldownTime); //7200 secs = 2h
        }

        public static int GetTimeLeftToNextRecovery(string gameName)
        {
            return GetTotalCooldownTime(gameName) - GetElapsedTimeSinceRecovery(gameName) % GetTotalCooldownTime(gameName);
        }

        private static int GetElapsedTimeSinceRecovery(string gameName)
        {
            var span = DateTime.Now - GetLastRecoveryLaunchTime(gameName);
            return (int)span.TotalSeconds;
        }

        private static DateTime GetLastRecoveryLaunchTime(string gameName)
        {
            return GamePlayerPrefs.GetTime(gameName + "LastRecoveryLaunch", DateTime.Now);//y,m,d,h,m,s
        }

        private static void SetLastRecoveryLaunchTime(string gameName, DateTime time)
        {
            GamePlayerPrefs.SetTime(gameName + "LastRecoveryLaunch", time);
        }

        public static bool Unlimited(string gameName)
        {
            return GetEnergy(gameName) >= int.MaxValue;
            //return true;
        }

        public static int GetEnergy(string gameName)
        {
            UpdateEnergyAmount(gameName);
            //Debug.Log("Get " + gameName + " " + GamePlayerPrefs.GetInt(gameName + "Energy", InitialEnergy));
            return GamePlayerPrefs.GetInt(gameName + "Energy", InitialEnergy);
        }

        private static void UpdateEnergyAmount(string gameName)
        {
            var timeSpanSinceLastRecovery = GetElapsedTimeSinceRecovery(gameName);
            var totalCooldownTime = GetTotalCooldownTime(gameName);

            var addition = timeSpanSinceLastRecovery / totalCooldownTime;
            var energySoFar = GamePlayerPrefs.GetInt(gameName + "Energy", InitialEnergy);

            if (energySoFar < GetAutoRecoveryLimit(gameName) && addition > 0)
            {
                addition = (energySoFar + addition <= GetAutoRecoveryLimit(gameName)
                    ? addition
                    : GetAutoRecoveryLimit(gameName) - energySoFar);

                AddEnergy(gameName, addition);
            }
        }

        public static void AddEnergy(string gameName, int amount)
        {
            var key = gameName + "Energy";
            var newAmount = GamePlayerPrefs.GetInt(key, InitialEnergy) + amount;
            GamePlayerPrefs.SetInt(key, newAmount);
            //Debug.Log("Set " + gameName + " " + GamePlayerPrefs.GetInt(gameName + "Energy", InitialEnergy));

            if (newAmount < GetAutoRecoveryLimit(gameName))
            {
                SetLastRecoveryLaunchTime(gameName, DateTime.Now);
            }
        }

        public static int GetAutoRecoveryLimit(string gameName)
        {
            return GamePlayerPrefs.GetInt(gameName + "RecoveryLimit", InitialAutoRecoveryLimit);
        }

        public static void BoostAutoRecoveryLimit(string gameName, int times)
        {
            var limit = GetAutoRecoveryLimit(gameName);
            limit *= times;
            GamePlayerPrefs.SetInt(gameName + "RecoveryLimit", limit);
            AddEnergy(gameName, 0);
        }

        public static void ReduceCooldown(string gameName)
        {
            var newValue = GetTotalCooldownTime(gameName) / 4;
            GamePlayerPrefs.SetInt(gameName + "TotalCooldownTime", newValue);
        }

        #endregion

        private Vector2 recoveringEnergyPos,
                        unlimitedEnergyPos,
                        normalEnergypos;

        private GameObject energyGo,
                           cooldownGo,
                           plusBtn;
        private enum EnergyState
        {
            Normal,
            Recovering,
            Unlimited
        };

        private EnergyState State
        {
            get
            {
                if (Unlimited(gameName))
                {
                    return EnergyState.Unlimited;
                }

                return IsRecovering() ? EnergyState.Recovering : EnergyState.Normal;
            }
        }

        private string gameName;

        [UsedImplicitly]
        private void Awake()
        {
            gameName = name.Substring(0, name.LastIndexOf("Energy"));

            if(string.IsNullOrEmpty(gameName))
                gameName = Tr.parent.name;

            energyGo = GameObjectManager.GetGoInChildren(Go, "Energy");
            cooldownGo = GameObjectManager.GetGoInChildren(Go, "Cooldown");
            plusBtn = GameObjectManager.GetGoInChildren(Go, "GameOrders");
            InitEnergyPositions();
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            switch (State)
            {
                case EnergyState.Normal:
                    DisplayNormal();
                    break;
                case EnergyState.Recovering:
                    StartCoroutine(DisplayRecovering());
                    break;
                case EnergyState.Unlimited:
                    DisplayUnlimited();
                    break;
            }
        }

        private void InitEnergyPositions()
        {
            recoveringEnergyPos = energyGo.transform.localPosition;
            unlimitedEnergyPos = new Vector2(recoveringEnergyPos.x + 250, recoveringEnergyPos.y);
            normalEnergypos = new Vector2(recoveringEnergyPos.x + 150, recoveringEnergyPos.y);
        }

        private bool IsRecovering()
        {
            return GetEnergy(gameName) < GetAutoRecoveryLimit(gameName);
        }

        #region display methods
        private IEnumerator DisplayRecovering()
        {
            energyGo.transform.localPosition = recoveringEnergyPos;

            while (State == EnergyState.Recovering)
            {
                UpdateTimer();
                energyGo.GetComponent<Text>().text = GetEnergy(gameName).ToString();
                yield return new WaitForSeconds(1);
            }

            DisplayNormal();
        }

        private void UpdateTimer()
        {
            cooldownGo.SetActive(true);

            var cooldownInSeconds = GetTimeLeftToNextRecovery(gameName);
            cooldownGo.GetComponent<Text>().text = Helper.GetFormattedTime(cooldownInSeconds, true);
        }

        private void DisplayNormal()
        {
            energyGo.transform.localPosition = normalEnergypos;
            energyGo.GetComponent<Text>().text = GetEnergy(gameName).ToString();
            cooldownGo.SetActive(false);
        }

        private void DisplayUnlimited()
        {
            energyGo.transform.localPosition = unlimitedEnergyPos;
            energyGo.GetComponent<Text>().text = "unlimited";

            Destroy(plusBtn);
            Destroy(cooldownGo);
        }
        #endregion
    }
}
