using Terraria.ModLoader.IO;

namespace DestinyMod.Common.NPCs.Data
{
    /// <summary>Contains data pertaining to an NPC shop item slot.</summary>
    public struct NPCShopData
    {
        /// <summary>
        /// The type of the item to sell.
        /// </summary>
        public int ItemType;

        /// <summary>
        /// The price of the item to sell. Use ONLY for custom currencies, set to null if there is no custom currency (which will cause the item to be sold at its value).
        /// </summary>
        public int? ItemPrice;

        /// <summary>
        /// The custom currency ID of the currency to use when selling this item.
        /// </summary>
        public int ItemCurrency;

        /// <summary>
        /// The slot in the NPC's shop to place this item in.
        /// </summary>
        public int ItemSlot;

        public NPCShopData()
        {
            ItemType = -1;
            ItemPrice = null;
            ItemCurrency = -1;
            ItemSlot = -1;
        }

        public NPCShopData(int itemType, int? price, int currencyType, int itemSlot)
        {
            ItemType = itemType;
            ItemPrice = price;
            ItemCurrency = currencyType;
            ItemSlot = itemSlot;
        }

        public TagCompound Save()
        {
            return new TagCompound()
            {
                { "ItemType", ItemType },
                { "ItemPrice", ItemPrice },
                { "ItemCurrency", ItemCurrency },
                { "ItemSlot", ItemSlot },
            };
        }

        public static NPCShopData Load(TagCompound tagCompound)
        {
            int itemType = tagCompound.Get<int>("ItemType");
            int? price = tagCompound.Get<int?>("ItemPrice");
            int currency = tagCompound.Get<int>("ItemCurrency");
            int slot = tagCompound.Get<int>("ItemSlot");
            return new NPCShopData(itemType, price, currency, slot);
        }
    }
}