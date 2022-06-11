using Terraria.ModLoader.IO;

namespace DestinyMod.Common.NPCs.Data
{
    public struct NPCShopData
    {
        public int ItemType;

        public int ItemCurrency;

        public int ItemPrice;

        public NPCShopData(int itemType, int currencyType, int price)
        {
            ItemType = itemType;
            ItemCurrency = currencyType;
            ItemPrice = price;
        }

        public TagCompound Save()
        {
            return new TagCompound()
            {
                { "ItemType", ItemType },
                { "ItemCurrency", ItemCurrency },
                { "ItemPrice", ItemPrice },
            };
        }

        public static NPCShopData Load(TagCompound tagCompound)
        {
            int itemType = tagCompound.Get<int>("ItemType");
            int currency = tagCompound.Get<int>("ItemCurrency");
            int price = tagCompound.Get<int>("ItemPrice");
            return new NPCShopData(itemType, currency, price);
        }
    }
}