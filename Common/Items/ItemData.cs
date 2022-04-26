using Terraria.ModLoader;
using DestinyMod.Common.Items.PerksAndMods;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.GlobalItems;
using DestinyMod.Content.Items.Mods;

namespace DestinyMod.Common.Items
{
    public class ItemData : ILoadable
	{
		public int ItemType;

        public const int MinimumLightLevel = 1350;

        public static readonly int PreHardmodeLightLevelCap = 1400; // 6x minimum

        public static readonly int PrePlanteraLightLevelCap = 1420; // 11x minimum

        public static readonly int PreMoonlordLightLevelCap = 1500; // 50x minimum :D ( I love Tera balancing )

        public const int MaxmimumLightLevel = 1560; // ~90x minimum with current formula

        public int DefaultLightLevel;

        public Action<int> InterpretLightLevel;

        public int MaximumModCount;

        public IList<ItemPerkPool> PerkPool;

        public static IDictionary<int, ItemData> ItemDatasByID { get; private set; } = new Dictionary<int, ItemData>();

        private ItemData(int itemType, int defaultLightLevel = 1350, Action<int> interpretLightLevel = null, int maximumModCount = 0, IList<ItemPerkPool> perkPool = null)
        {
            ItemType = itemType;
            DefaultLightLevel = Math.Clamp(defaultLightLevel, MinimumLightLevel, MaxmimumLightLevel);
            InterpretLightLevel = interpretLightLevel;
            MaximumModCount = maximumModCount; 
            PerkPool = perkPool;
        }

        public void Load(Mod mod) { }

        public void Unload() => ItemDatasByID?.Clear();

        public static bool InitializeNewItemData(int itemType, int defaultLightLevel = 1350, Action<int> interpretLightLevel = null, int maximumModCount = 0, IList<ItemPerkPool> perkPool = null)
        {
            if (ItemDatasByID == null || ItemDatasByID.ContainsKey(itemType))
            {
                return false;
            }

            ItemDatasByID.Add(itemType, new ItemData(itemType, defaultLightLevel, interpretLightLevel, maximumModCount, perkPool));
            return true;
        }

        public int ClampLightLevel(int lightLevel)
        {
            if (!Main.hardMode)
            {
                lightLevel = Math.Clamp(lightLevel, MinimumLightLevel, PreHardmodeLightLevelCap);
            }

            if (!NPC.downedPlantBoss)
            {
                lightLevel = Math.Clamp(lightLevel, MinimumLightLevel, PrePlanteraLightLevelCap);
            }

            if (!NPC.downedMoonlord)
            {
                lightLevel = Math.Clamp(lightLevel, MinimumLightLevel, PreMoonlordLightLevelCap);
            }

            return Math.Clamp(lightLevel, DefaultLightLevel, MaxmimumLightLevel);
        }

        public void GenerateItem(Player player, IEntitySource source, int overrideLightLevel = -1)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            int itemPowerLevel = itemDataPlayer.LightLevel;

            if (overrideLightLevel != -1)
            {
                itemPowerLevel = overrideLightLevel;
            }
            else
            {
                if (Main.rand.NextFloat() < 0.2) // 1 in 5 to be 2 light level stronger or weaker than your current light level
                {
                    itemPowerLevel += Main.rand.NextFloat() < 0.33f ? 2 : -2; // NextFloat() in case you want to make weaker/stronger more probable than the other
                }
                else if (Main.rand.NextFloat() < 0.5f) // 50% chance otherwise
                {
                    itemPowerLevel += Main.rand.NextFloat() < 0.5f ? 1 : -1; // NextFloat() in case you want to make weaker/stronger more probable than the other
                }

                itemPowerLevel = ClampLightLevel(itemPowerLevel);
            }

            Item item = Main.item[player.QuickSpawnItem(source, ItemType)];
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            itemDataItem.LightLevel = itemPowerLevel;
            itemDataItem.SetDefaults(item);

            if (PerkPool != null)
            {
                itemDataItem.ActivePerks = new List<ItemPerk>();
                foreach (ItemPerkPool perkPoolType in PerkPool)
                {
                    itemDataItem.ActivePerks.Add(perkPoolType.Perks[0]);
                }
            }

            itemDataItem.ItemMods = new List<ItemMod>();
            for (int modIndexer = 0; modIndexer < MaximumModCount; modIndexer++)
            {
                itemDataItem.ItemMods.Add(null);
            }
        }
    }
}