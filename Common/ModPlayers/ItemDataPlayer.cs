using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Content.Items.Mods.Weapon;
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

        protected override bool CloneNewInstances => false;

        public IList<int> UnlockedMods = new List<int>();

        public IList<int> DiscoveredCatalysts = new List<int>();

        public override void PreUpdate()
        {
            if (UnlockedMods.Count > 0)
            {
                return;
            }

            // Here for testing purposes
            Main.NewText("[ Testing ]: Populating IList<ItemMod> UnlockedMods with Boss Spec and Minor Spec.");
            UnlockedMods = new List<int>()
            {
                ModifierBase.GetType<BossSpec>(),
                ModifierBase.GetType<MinorSpec>(),
            };
        }

        public override void ResetEffects()
        {
            LightLevel = 0;
        }

        public override void SaveData(TagCompound tag)
        {
            if (UnlockedMods.Count > 0)
            {
                tag.Add("UnlockedMods", UnlockedMods.Select(mod => ItemMod.GetInstance(mod).Name).ToList());
            }

            if (DiscoveredCatalysts.Count > 0)
            {
                tag.Add("DiscoveredCatalyst", DiscoveredCatalysts.Select(catalyst => ItemCatalyst.GetInstance(catalyst).Name).ToList());
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
                List<string> discoveredCatalystsSaved = tag.Get<List<string>>("DiscoveredCatalyst");
                foreach (string catalystName in discoveredCatalystsSaved)
                {
                    if (!ModAndPerkLoader.ItemCatalystsByName.TryGetValue(catalystName, out ItemCatalyst itemCatalyst))
                    {
                        continue;
                    }

                    UnlockedMods.Add(itemCatalyst.Type);
                }
            }
        }

        public override void PostUpdateMiscEffects()
        {
            int itemsConsidered = 0;

            foreach (Item armorItem in Player.armor)
            {
                if (armorItem == null || armorItem.IsAir || !armorItem.TryGetGlobalItem(out ItemDataItem armorItemData) || armorItemData.LightLevel < ItemData.MaximumLightLevel)
                {
                    continue;
                }

                itemsConsidered++;
                LightLevel += Utils.Clamp(armorItemData.LightLevel, ItemData.MinimumLightLevel, ItemData.MaximumLightLevel);

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers)
                {
                    modifier?.Update(Player);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem != null && !heldItem.IsAir && heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                /*if (heldItemData.LightLevel < ItemData.MaximumLightLevel) huh?
                {
                    return;
                }*/

                itemsConsidered++;
                LightLevel += Utils.Clamp(heldItemData.LightLevel, ItemData.MinimumLightLevel, ItemData.MaximumLightLevel);

                foreach (ModifierBase modifier in heldItemData.AllItemModifiers)
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

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers)
                {
                    modifier?.ModifyHitNPC(Player, item, target, ref damage, ref knockback, ref crit);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers)
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

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers)
                {
                    modifier?.OnHitNPC(Player, item, target, damage, knockback, crit);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers)
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

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers)
                {
                    modifier?.ModifyHitNPCWithProj(Player, proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers)
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

                foreach (ModifierBase modifier in armorItemData.AllItemModifiers)
                {
                    modifier?.OnHitNPCWithProj(Player, proj, target, damage, knockback, crit);
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers)
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

            foreach (ModifierBase modifier in heldItemData.AllItemModifiers)
            {
                modifier?.UseSpeedMultiplier(Player, item, ref start);
            }
            return start;
        }
    }
}
