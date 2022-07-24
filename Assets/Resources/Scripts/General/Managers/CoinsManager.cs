namespace Assets.Resources.Scripts.General.Managers
{
    public static class CoinsManager
    {
        private const int InitialCoins = 3500;
        public static void Add(int coins)
        {
            GamePlayerPrefs.SetInt("coins", GetAmount() + coins);
        }

        public static void Rid(int coins)
        {
            var coinsSoFar = GetAmount();
            coinsSoFar -= coins;

            if (coinsSoFar >= 0) GamePlayerPrefs.SetInt("coins", coinsSoFar);
        }

        public static int GetAmount()
        {
            return GamePlayerPrefs.GetInt("coins", InitialCoins);
        }
    }
}
