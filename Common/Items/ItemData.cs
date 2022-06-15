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
using System.Linq;
using DestinyMod.Content.Items.Perks.Weapon.Barrels;
using DestinyMod.Content.Items.Perks.Weapon.Traits;
using DestinyMod.Content.Items.Perks.Weapon.Magazines;

namespace DestinyMod.Common.Items
{
    /// <summary>Container class for <see cref="ItemDataItem"/> manipulation. Contains general helper methods and perk data for items.</summary>
    public class ItemData : ILoadable
	{
        /// <summary>
        /// The type of this item.
        /// </summary>
		public int ItemType;

        public const int MinimumLightLevel = 1350;

        public const int PreHardmodeLightLevelCap = 1400; // 6x minimum

        public const int PrePlanteraLightLevelCap = 1420; // 11x minimum

        public const int PreMoonlordLightLevelCap = 1500; // 50x minimum :D ( I love Tera balancing )

        public const int MaximumLightLevel = 1560; // ~90x minimum with current formula (This is the hard cap)

        public int DefaultLightLevel;

        /// <summary>
        /// If set, runs this action to determine the weapon's light level.
        /// </summary>
        public Action<int> InterpretLightLevel;

        public Func<List<ItemPerkPool>> GeneratePerkPool;

        /// <summary>
        /// The maximum number of mod slots to include on this weapon.
        /// </summary>
        public int MaximumModCount;

        /// <summary>
        /// The type of this item's catalyst, if it exists. Should be set in SetStaticDefaults along with other modifier data.
        /// </summary>
        public int ItemCatalyst = -1;

        /// <summary>
        /// Whether or not this item can have shaders applied to it. Defaults to <see langword="true"/>.
        /// </summary>
        public bool Shaderable = true;

        public int DefaultImpact = 0; // Damage

        public int DefaultRange = 0; // Range

        public int DefaultStability = 0; // Spread

        public int DefaultRecoil = 0; // Recoil

        public int DefaultReloadSpeed = 0; // Mag Soon!

        public static readonly ItemPerk[] AutoRifleBarrels = new ItemPerk[]
        {
            ModContent.GetInstance<ArrowheadBrake>(),
            ModContent.GetInstance<BarrelShroud>(),
            ModContent.GetInstance<ChamberedCompensator>(),
        };

        public static readonly ItemPerk[] AllLauncherBarrels = new ItemPerk[]
        {
            
        };

        public static readonly ItemPerk[] AllTraits = new ItemPerk[]
        {
            ModContent.GetInstance<Frenzy>(),
            ModContent.GetInstance<HighCaliberRounds>()
        };

        public static readonly ItemPerk[] AllHafts = new ItemPerk[]
        {
            
        };

        /// <summary>
        /// A dictionary of all item types mapped to their relevant data.
        /// </summary>
        public static IDictionary<int, ItemData> ItemDatasByID { get; private set; } = new Dictionary<int, ItemData>();

        /// <summary>Used internally. To create new item data, use <see cref="InitializeNewItemData(int, int, Action{int}, int)"/>.</summary>
        private ItemData(int itemType, int defaultLightLevel = MinimumLightLevel, Action<int> interpretLightLevel = null, int maximumModCount = 0)
        {
            ItemType = itemType;
            DefaultLightLevel = Math.Clamp(defaultLightLevel, MinimumLightLevel, MaximumLightLevel);
            InterpretLightLevel = interpretLightLevel;
            MaximumModCount = maximumModCount;
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
        /// <returns>The generated perk data.</returns>
        public static ItemData InitializeNewItemData(int itemType, int defaultLightLevel = MinimumLightLevel, Action<int> interpretLightLevel = null, int maximumModCount = 0)
        {
            if (ItemDatasByID == null || ItemDatasByID.ContainsKey(itemType))
            {
                return null;
            }

            ItemData newItemData = new ItemData(itemType, defaultLightLevel, interpretLightLevel, maximumModCount);
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

        /// <summary>
        /// Creates an <see cref="Item"/> with <see cref="ItemDataItem"/> data.
        /// </summary>
        /// <param name="player">The player to spawn this item on.</param>
        /// <param name="overrideLightLevel">The light level of this item.</param>
        /// <param name="perkPool">The list of perk pools for this item.</param>
        /// <returns>The generated <see cref="Item"/> with <see cref="ItemDataItem"/> data.</returns>
        public Item GenerateItem(Player player, int overrideLightLevel = -1, IList<ItemPerkPool> perkPool = null)
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

            if (perkPool == null && GeneratePerkPool != null)
            {
                perkPool = GeneratePerkPool();
            }

            Item item = new Item(ItemType);
            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            itemDataItem.LightLevel = itemPowerLevel;
            itemDataItem.PerkPool = perkPool;
            itemDataItem.SetDefaults(item);

            if (perkPool != null)
            {
                itemDataItem.ActivePerks = new List<ItemPerk>();
                foreach (ItemPerkPool perkPoolType in perkPool)
                {
                    itemDataItem.ActivePerks.Add(perkPoolType.Perks[0]);
                }
            }

            itemDataItem.ItemMods = new List<ItemMod>();
            for (int modIndexer = 0; modIndexer < MaximumModCount; modIndexer++)
            {
                itemDataItem.ItemMods.Add(ModContent.GetInstance<NullMod>());
            }

            return item;
        }

        /// <summary>
        /// Creates an <see cref="Item"/> with <see cref="ItemDataItem"/> data.
        /// </summary>
        /// <param name="player">The player to spawn this item on.</param>
        /// <param name="overrideLightLevel">The light level of this item.</param>
        /// <param name="perkPool">The list of perk pools for this item.</param>
        /// <returns>The generated <see cref="Item"/> with <see cref="ItemDataItem"/> data.</returns>
        public void GenerateItem(Item item, Player player, int overrideLightLevel = -1, IList<ItemPerkPool> perkPool = null)
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

            if (perkPool == null && GeneratePerkPool != null)
            {
                perkPool = GeneratePerkPool();
            }

            ItemDataItem itemDataItem = item.GetGlobalItem<ItemDataItem>();
            itemDataItem.LightLevel = itemPowerLevel;
            itemDataItem.PerkPool = perkPool;
            itemDataItem.SetDefaults(item);

            if (perkPool != null)
            {
                itemDataItem.ActivePerks = new List<ItemPerk>();
                foreach (ItemPerkPool perkPoolType in perkPool)
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

        /// <summary>
        /// Randomly generates an <see cref="ItemPerk"/> array, for use with <see cref="ItemPerkPool"/>.
        /// </summary>
        /// <param name="amountOfPerks">How many perks to include in the array.</param>
        /// <param name="perks">The perks to choose from.</param>
        /// <returns>The <see cref="ItemPerk"/> array.</returns>
        public static ItemPerk[] RollRandomPerks(int amountOfPerks, params ItemPerk[] perks)
        {
            if (amountOfPerks > perks.Length)
            {
                throw new ArgumentException("Amount of selectable perks cannot be more than the amount of perks provided.");
            }

            if (perks.Distinct().ToArray().Length != perks.Length)
            {
                Main.NewText("Latest call to RollRandomPerks included duplicate perks. They were filtered, but consider removing the duplicates from the call.", Color.Red);
                perks = perks.Distinct().ToArray();
            }

            ItemPerk[] start = new ItemPerk[amountOfPerks];
            for (int i = 0; i < amountOfPerks; i++)
            {
                int indexer = Main.rand.Next(0, perks.Length);
                while (perks[indexer] == null)
                {
                    indexer = Main.rand.Next(0, perks.Length);
                }

                start[i] = perks[indexer];
                perks[indexer] = null;
            }
            return start;
        }

        public static int CalculateRecoil(int recoil) => (int)Math.Round(Math.Sin((recoil + 5) * (MathHelper.TwoPi / 20)) * (100 - recoil));
    }
}