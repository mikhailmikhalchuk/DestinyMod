using Terraria.ModLoader;
using DestinyMod.Common.Items.Modifiers;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria;
using DestinyMod.Common.ModPlayers;
using DestinyMod.Common.GlobalItems;
using DestinyMod.Content.Items.Mods;
using Microsoft.Xna.Framework;

namespace DestinyMod.Common.Items
{
    public class ItemData : ILoadable
	{
		public int ItemType;

        public const int MinimumLightLevel = 1350;

        public static readonly int PreHardmodeLightLevelCap = 1400; // 6x minimum

        public static readonly int PrePlanteraLightLevelCap = 1420; // 11x minimum

        public static readonly int PreMoonlordLightLevelCap = 1500; // 50x minimum :D ( I love Tera balancing )

        public const int MaximumLightLevel = 1560; // ~90x minimum with current formula (This is the hard cap)

        public int DefaultLightLevel;

        public Action<int> InterpretLightLevel;

        public int MaximumModCount;

        public IList<ItemPerkPool> PerkPool;

        public ItemCatalyst ItemCatalyst;

        public bool Shaderable;

        public int DefaultImpact;

        public int DefaultRange;

        public int DefaultStability;

        public int DefaultHandling;

        public int DefaultReloadSpeed;

        public int Recoil;

        public static IDictionary<int, ItemData> ItemDatasByID { get; private set; } = new Dictionary<int, ItemData>();

        private ItemData(int itemType, int defaultLightLevel = MinimumLightLevel, Action<int> interpretLightLevel = null, int maximumModCount = 0, IList<ItemPerkPool> perkPool = null)
        {
            ItemType = itemType;
            DefaultLightLevel = Math.Clamp(defaultLightLevel, MinimumLightLevel, MaximumLightLevel);
            InterpretLightLevel = interpretLightLevel;
            MaximumModCount = maximumModCount; 
            PerkPool = perkPool;
        }

        public void Load(Mod mod) { }

        public void Unload()
        {
            ItemDatasByID?.Clear();
        }

        /// <summary>
        /// Initializes perk data for the item.
        /// </summary>
        /// <param name="itemType">The item's type.</param>
        /// <param name="defaultLightLevel">The light level to default to when rolling this weapon.</param>
        /// <param name="interpretLightLevel">An <see cref="Action"/> to be called to calculate the light level when rolling this weapon.</param>
        /// <param name="maximumModCount">The maximum amount of mod slots to include on this weapon (how many mods can be socketed at one time).</param>
        /// <param name="perkPool">The list of perks for this weapon.</param>
        /// <returns>The generated perk data.</returns>
        public static ItemData InitializeNewItemData(int itemType, int defaultLightLevel = MinimumLightLevel, Action<int> interpretLightLevel = null, int maximumModCount = 0, IList<ItemPerkPool> perkPool = null)
        {
            if (ItemDatasByID == null || ItemDatasByID.ContainsKey(itemType))
            {
                return null;
            }

            ItemData newItemData = new ItemData(itemType, defaultLightLevel, interpretLightLevel, maximumModCount, perkPool);
            ItemDatasByID.Add(itemType, newItemData);
            return newItemData;
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

            return Math.Clamp(lightLevel, DefaultLightLevel, MaximumLightLevel);
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
                itemDataItem.ItemMods.Add(ModContent.GetInstance<NullMod>());
            }
        }

        public static int CalculateRecoil(int recoil) => (int)Math.Round(Math.Sin((recoil + 5) * (MathHelper.TwoPi / 20)) * (100 - recoil));
    }
}