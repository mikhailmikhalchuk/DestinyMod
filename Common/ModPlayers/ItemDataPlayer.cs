using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items;
using DestinyMod.Common.Items.PerksAndMods;
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

        public override bool CloneNewInstances => false;

        public IList<ItemMod> UnlockedMods = new List<ItemMod>();

        public override void PreUpdate()
        {
            if (UnlockedMods.Count > 0)
            {
                return;
            }

            // Here for testing purposes
            Main.NewText("[ Testing ]: Populating IList<ItemMod> UnlockedMods with Boss Spec and Minor Spec.");
            UnlockedMods = new List<ItemMod>()
            {
                ModContent.GetInstance<BossSpec>(),
                ModContent.GetInstance<MinorSpec>(),
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
                tag.Add("UnlockedMods", UnlockedMods.Select(mod => mod?.Name).ToList());
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

                    UnlockedMods.Add(itemMod);
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

                if (armorItemData.ActivePerks != null)
                {
                    foreach (ItemPerk itemPerk in armorItemData.ActivePerks)
                    {
                        itemPerk?.Update(Player);
                    }
                }

                if (armorItemData.ItemMods != null)
                {
                    foreach (ItemMod itemMod in armorItemData.ItemMods)
                    {
                        itemMod?.Update(Player);
                    }
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

                if (heldItemData.ActivePerks != null)
                {
                    foreach (ItemPerk itemPerk in heldItemData.ActivePerks)
                    {
                        itemPerk?.Update(Player);
                    }
                }

                if (heldItemData.ItemMods != null)
                {
                    foreach (ItemMod itemMod in heldItemData.ItemMods)
                    {
                        itemMod?.Update(Player);
                    }
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

                if (armorItemData.ActivePerks != null)
                {
                    foreach (ItemPerk itemPerk in armorItemData.ActivePerks)
                    {
                        itemPerk?.ModifyHitNPC(Player, item, target, ref damage, ref knockback, ref crit);
                    }
                }

                if (armorItemData.ItemMods != null)
                {
                    foreach (ItemMod itemMod in armorItemData.ItemMods)
                    {
                        itemMod?.ModifyHitNPC(Player, item, target, ref damage, ref knockback, ref crit);
                    }
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            if (heldItemData.ActivePerks != null)
            {
                foreach (ItemPerk itemPerk in heldItemData.ActivePerks)
                {
                    itemPerk?.ModifyHitNPC(Player, item, target, ref damage, ref knockback, ref crit);
                }
            }

            if (heldItemData.ItemMods != null)
            {
                foreach (ItemMod itemMod in heldItemData.ItemMods)
                {
                    itemMod?.ModifyHitNPC(Player, item, target, ref damage, ref knockback, ref crit);
                }
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

                if (armorItemData.ActivePerks != null)
                {
                    foreach (ItemPerk itemPerk in armorItemData.ActivePerks)
                    {
                        itemPerk?.ModifyHitNPCWithProj(Player, proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
                    }
                }

                if (armorItemData.ItemMods != null)
                {
                    foreach (ItemMod itemMod in armorItemData.ItemMods)
                    {
                        itemMod?.ModifyHitNPCWithProj(Player, proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
                    }
                }
            }

            Item heldItem = Main.mouseItem.IsAir ? Player.HeldItem : Main.mouseItem;
            if (heldItem == null || heldItem.IsAir || !heldItem.TryGetGlobalItem(out ItemDataItem heldItemData))
            {
                return;
            }

            if (heldItemData.ActivePerks != null)
            {
                foreach (ItemPerk itemPerk in heldItemData.ActivePerks)
                {
                    itemPerk?.ModifyHitNPCWithProj(Player, proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
                }
            }

            if (heldItemData.ItemMods != null)
            {
                foreach (ItemMod itemMod in heldItemData.ItemMods)
                {
                    itemMod?.ModifyHitNPCWithProj(Player, proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
                }
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

            if (heldItemData.ActivePerks != null)
            {
                foreach (ItemPerk itemPerk in heldItemData.ActivePerks)
                {
                    itemPerk.UseSpeedMultiplier(Player, item, ref start);
                }
            }

            if (heldItemData.ItemMods != null)
            {
                foreach (ItemMod itemMod in heldItemData.ItemMods)
                {
                    itemMod.UseSpeedMultiplier(Player, item, ref start);
                }
            }

            return start;
        }
    }
}
