using System.Collections.Generic;
using Soomla.Store;

namespace Assets.Resources.Scripts.General
{
    class MemoryRunAssetsStore : IStoreAssets
    {
        #region IDs
        private const string Coins1500Id = "coins1500",
                             Coins2200Id = "coins2200",
                             Coins3300Id = "coins3300",
                             Coins4640Id = "coins4640",
                             Coins7260Id = "coins7260",
                             Coins10208Id = "coins10208",
                             Coins16376Id = "coins16376",
                             Coins23320Id = "coins23320",
                             Coins50723Id = "coins50723",
                             Coins111590Id = "coins111590";
        #endregion

        #region Virtual Goods

        private static VirtualGood Coins1500Vg = new SingleUseVG(
            "Coins1500",
            "",
            Coins1500Id,
            new PurchaseWithMarket(new MarketItem(Coins1500Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins2200Vg = new SingleUseVG(
            "Coins2200",
            "",
            Coins2200Id,
            new PurchaseWithMarket(new MarketItem(Coins2200Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins3300Vg = new SingleUseVG(
            "Coins3300",
            "",
            Coins3300Id,
            new PurchaseWithMarket(new MarketItem(Coins3300Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins4640Vg = new SingleUseVG(
            "Coins4640",
            "",
            Coins4640Id,
            new PurchaseWithMarket(new MarketItem(Coins4640Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins7260Vg = new SingleUseVG(
            "Coins7260",
            "",
            Coins7260Id,
            new PurchaseWithMarket(new MarketItem(Coins7260Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins10208Vg = new SingleUseVG(
            "Coins10208",
            "",
            Coins10208Id,
            new PurchaseWithMarket(new MarketItem(Coins10208Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins16376Vg = new SingleUseVG(
            "Coins16376",
            "",
            Coins16376Id,
            new PurchaseWithMarket(new MarketItem(Coins16376Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins23320Vg = new SingleUseVG(
            "Coins23320",
            "",
            Coins23320Id,
            new PurchaseWithMarket(new MarketItem(Coins23320Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins50723Vg = new SingleUseVG(
            "Coins50723",
            "",
            Coins50723Id,
            new PurchaseWithMarket(new MarketItem(Coins50723Id, MarketItem.Consumable.CONSUMABLE, 1)));

        private static VirtualGood Coins111590Vg = new SingleUseVG(
            "Coins111590",
            "",
            Coins111590Id,
            new PurchaseWithMarket(new MarketItem(Coins111590Id, MarketItem.Consumable.CONSUMABLE, 1)));
        #endregion

        #region methods

        public int GetVersion()
        {
            return 1;
        }

        public VirtualCurrency[] GetCurrencies()
        {
            return new VirtualCurrency[] { };
        }

        public VirtualGood[] GetGoods()
        {
            return new[] { Coins1500Vg, Coins2200Vg, Coins3300Vg,
                           Coins4640Vg, Coins7260Vg, Coins10208Vg,
                           Coins16376Vg, Coins23320Vg, Coins50723Vg,
                           Coins111590Vg };
        }

        public VirtualCurrencyPack[] GetCurrencyPacks()
        {
            return new VirtualCurrencyPack[] { };
        }

        public VirtualCategory[] GetCategories()
        {
            return new[] { new VirtualCategory("General", new List<string>()
            {
                Coins1500Id, Coins2200Id, Coins3300Id,
                Coins4640Id, Coins7260Id, Coins10208Id,
                Coins16376Id, Coins23320Id, Coins50723Id,
                Coins111590Id
            }), };
        }
        #endregion
    }
}
