using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Content.Items.Catalysts;
using DestinyMod.Content.Items.Mods.Weapon;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DestinyMod.Common.ModPlayers
{
    public class ItemDataPlayer : ModPlayer
    {
        public int LightLevel;

        private int RangeInternal = -1;

        public int Range
        {
            get => RangeInternal;
            set => RangeInternal = Utils.Clamp(value, 0, 100);
        }

        private int StabilityInternal = -1;

        public int Stability
        {
            get => StabilityInternal;
            set => StabilityInternal = Utils.Clamp(value, 0, 100);
        }

        private int RecoilInternal = -1;

        public int Recoil
        {
            get => RecoilInternal;
            set => RecoilInternal = Utils.Clamp(value, 0, 100);
        }

        public float OldWeaponBounce;

        /// <summary>
        /// Used for weapon recoil "bouncing" calculation (weapon rotational deviation on use).
        /// </summary>
        public float WeaponUseBounce;

        public int ResetBounceTimer;

        public int ResetBounceThreshold = 60;

        protected override bool CloneNewInstances => false;

        /// <summary>
        /// Contains the types of all mods unlocked by this player.
        /// </summary>
        public IList<int> UnlockedMods = new List<int>();

        /// <summary>
        /// Contains data on all weapon catalysts for this player, discovered or not.
        /// Access catalysts from this list using <see cref="ModifierBase.GetType{T}"/> as an index.
        /// </summary>
        public IList<ItemCatalyst> CatalystData = new List<ItemCatalyst>();

        #region Data Structures

        public override void Initialize()
        {
            foreach (ItemCatalyst catalyst in ModAndPerkLoader.ItemCatalysts)
            {
                CatalystData.Add(ItemCatalyst.CreateInstance(catalyst.Name));
            }
        }

        public override void SaveData(TagCompound tag)
        {
            if (UnlockedMods.Count > 0)
            {
                tag.Add("UnlockedMods", UnlockedMods.Select(mod => ItemMod.GetInstance(mod).Name).ToList());
            }

            if (CatalystData.Count > 0)
            {
                tag.Add("DiscoveredCatalyst", CatalystData.Select(catalyst => new TagCompound()
                {
                    { "Name", catalyst.Name },
                    { "Data", catalyst.Save() }
                }).ToList());
            }
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("UnlockedMods"))
            {
                List<string> unlockedModsSaved = tag.Get<List<string>>("UnlockedMods");
                foreach (string modName in unlockedModsSaved)
                {
                    if (!ModAndPerkLoader.ItemModsByName.TryGetValue(modName, out ItemMod itemMod))
                    {
                        continue;
                    }

                    UnlockedMods.Add(itemMod.Type);
                }
            }

            if (tag.ContainsKey("DiscoveredCatalyst"))
            {
                List<TagCompound> discoveredCatalystsSaved = tag.Get<List<TagCompound>>("DiscoveredCatalyst");
                foreach (TagCompound savedCatalyst in discoveredCatalystsSaved)
                {
                    string catalystName = savedCatalyst.Get<string>("Name");
                    if (!ModAndPerkLoader.ItemCatalystsByName.TryGetValue(catalystName, out ItemCatalyst itemCatalyst))
                    {
                        continue;
                    }

                    ItemCatalyst catalyst = CatalystData.First(cat => cat.Name == catalystName);
                    catalyst.Load(savedCatalyst.Get<TagCompound>("Data"));
                }
            }
        }

        #endregion

        #region Testing

        public override void PreUpdate()
        {
            // Here for testing purposes
            if (UnlockedMods.Count == 0)
            {
                Main.NewText("[ Testing ]: Populating IList<int> UnlockedMods with Boss Spec and Minor Spec.");
                UnlockedMods = new List<int>()
                {
                    ModifierBase.GetType<BossSpec>(),
                    ModifierBase.GetType<MinorSpec>(),
                };
            }

            CatalystData[ModifierBase.GetType<NoTimeToExplainCatalyst>()].IsDiscovered = true;
        }

        #endregion

        public override void ResetEffects()
        {
            LightLevel = 0;
            RangeInternal = -1;
            StabilityInternal = -1;
            RecoilInternal = -1;

            if (++ResetBounceTimer > ResetBounceThreshold)
            {
                WeaponUseBounce = 0;
                ResetBounceThreshold = 60;
            }

            if (WeaponUseBounce > 0)
            {
                WeaponUseBounce--;

                WeaponUseBounce -= ResetBounceTimer / 10;

                if (WeaponUseBounce < 0)
                {
                    WeaponUseBounce = 0;
                }
            }

            if (WeaponUseBounce < 0)
            {
                WeaponUseBounce++;

                WeaponUseBounce += ResetBounceTimer / 10;

                if (WeaponUseBounce > 0)
                {
                    WeaponUseBounce = 0;
                }
            }
        }

        public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item)
        {
            if (item.ModItem is IItemModGranter modGranter)
            {
                ItemDataPlayer itemDataPlayer = Player.GetModPlayer<ItemDataPlayer>();
                if (itemDataPlayer.UnlockedMods.Contains(modGranter.ItemModType()))
                {
                    return;
                }

                itemDataPlayer.UnlockedMods.Add(modGranter.ItemModType());
                Main.NewText($"Unlocked [c/0092E0:{modGranter.ItemModName()}]! You can now socket this mod in any compatible item.", Color.LimeGreen);
                item.TurnToAir();
            }
        }

        #region Implement Modifiers

        public override void PostUpdateMiscEffects()
        {
            int itemsConsidered = 0;

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem != null && !heldItem.IsAir && heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                /*if (heldItemData.LightLevel < ItemData.MaximumLightLevel) huh?
                {
                    return;
                }*/

                if (ItemData.ItemDatasByID != null && ItemData.ItemDatasByID.TryGetValue(heldItem.type, out ItemData itemData))
                {
                    heldItemData.Range = itemData.DefaultRange;
                    heldItemData.Stability = itemData.DefaultStability;
                    heldItemData.Recoil = itemData.DefaultRecoil;
                }

                itemsConsidered++;
                LightLevel += Utils.Clamp(heldItemData.LightLevel, ItemData.MinimumLightLevel, ItemData.MaximumLightLevel);
                Range = heldItemData.Range;
                Stability = heldItemData.Stability;
                Recoil = heldItemData.Recoil;

                foreach (ModifierBase modifier in heldItemData.AllItemModifiers(Player))
                {
                    modifier?.Update(Player);
                }
            }

            foreach (Item armorItem in Player.armor)
            {
                if (armorItem == null || armorItem.IsAir || !armorItem.TryGetGlobalItem(out ItemDataItem armorItemData) || armorItemData.LightLevel < ItemData.MaximumLightLevel)
                {
                    continue;
                }

                itemsConsidered++;
                LightLevel += Utils.Clamp(armorItemData.LightLevel, ItemData.MinimumLightLevel, ItemData.MaximumLightLevel);

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers(Player))
                {
                    modifier?.Update(Player);
                }
            }

            if (itemsConsidered > 0)
            {
                LightLevel /= itemsConsidered;
                Player.statDefense += (LightLevel - 1350) / 10;
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            foreach (Item armorItem in Player.armor)
            {
                if (armorItem == null || armorItem.IsAir || !armorItem.TryGetGlobalItem(out ItemDataItem armorItemData))
                {
                    continue;
                }

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers(Player))
                {
                    modifier?.ModifyHitNPC(Player, item, target, ref damage, ref knockback, ref crit);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers(Player))
            {
                modifier?.ModifyHitNPC(Player, item, target, ref damage, ref knockback, ref crit);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            foreach (Item armorItem in Player.armor)
            {
                if (armorItem == null || armorItem.IsAir || !armorItem.TryGetGlobalItem(out ItemDataItem armorItemData))
                {
                    continue;
                }

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers(Player))
                {
                    modifier?.OnHitNPC(Player, item, target, damage, knockback, crit);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers(Player))
            {
                modifier?.OnHitNPC(Player, item, target, damage, knockback, crit);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            foreach (Item armorItem in Player.armor)
            {
                if (armorItem == null || armorItem.IsAir || !armorItem.TryGetGlobalItem(out ItemDataItem armorItemData))
                {
                    continue;
                }

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers(Player))
                {
                    modifier?.ModifyHitNPCWithProj(Player, proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers(Player))
            {
                modifier?.ModifyHitNPCWithProj(Player, proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            foreach (Item armorItem in Player.armor)
            {
                if (armorItem == null || armorItem.IsAir || !armorItem.TryGetGlobalItem(out ItemDataItem armorItemData))
                {
                    continue;
                }

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers(Player))
                {
                    modifier?.OnHitNPCWithProj(Player, proj, target, damage, knockback, crit);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers(Player))
            {
                modifier?.OnHitNPCWithProj(Player, proj, target, damage, knockback, crit);
            }
        }

        public override float UseSpeedMultiplier(Item item)
        {
            float start = base.UseSpeedMultiplier(item);
            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return start;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers(Player))
            {
                modifier?.UseSpeedMultiplier(Player, item, ref start);
            }
            return start;
        }

        #endregion
    }
}
