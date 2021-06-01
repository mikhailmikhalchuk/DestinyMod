using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace TheDestinyMod.Items
{
    public class DestinyGlobalItem : GlobalItem
    {
        public bool catalyst = false;

        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone) {
            DestinyGlobalItem myClone = (DestinyGlobalItem)base.Clone(item, itemClone);
            myClone.catalyst = catalyst;
            if (item.type == ModContent.ItemType<Weapons.Ranged.SweetBusiness>() && catalyst && !Main.LocalPlayer.channel) {
                item.useTime = item.useAnimation = 15;
            }
            return myClone;
        }

        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
            if (item.type == ModContent.ItemType<Weapons.Ranged.SweetBusiness>() && catalyst && !Main.LocalPlayer.channel) {
                item.useTime = item.useAnimation = 15;
            }
        }

        public override void OpenVanillaBag(string context, Player player, int arg) {
            if (context == "bossBag" && arg == ItemID.EyeOfCthulhuBossBag) {
                player.QuickSpawnItem(ModContent.ItemType<Materials.MoteOfDark>(), Main.rand.Next(2, 5));
            }
        }

        public override void SetDefaults(Item item) {
            if (item.type == ItemID.Grenade) {
                item.ammo = item.type;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (item.type == ModContent.ItemType<Weapons.Ranged.SweetBusiness>() && catalyst) {
                tooltips.Add(new TooltipLine(mod, "Catalyst", "This weapon takes less time to spin up")
                {
                    overrideColor = Color.Yellow
                });
            }
            else if (!catalyst && item.type == ModContent.ItemType<Weapons.Ranged.SweetBusiness>()) {
                tooltips.Add(new TooltipLine(mod, "Catalyst", $"Right click with a {item.Name} catalyst to apply it")
                {
                    overrideColor = Color.Khaki
                });
            }
        }

        public override bool CanRightClick(Item item) {
            return item.type == ModContent.ItemType<Weapons.Ranged.SweetBusiness>() && Main.mouseItem.type == ModContent.ItemType<SweetBusinessCatalyst>() && !catalyst;
        }

        public override void RightClick(Item item, Player player) {
            if (item.type == ModContent.ItemType<Weapons.Ranged.SweetBusiness>()) {
                catalyst = true;
                Main.mouseItem.TurnToAir();
                player.QuickSpawnClonedItem(item);
            }
        }

        public override bool CanUseItem(Item item, Player player) {
            return !player.HasBuff(ModContent.BuffType<Buffs.Debuffs.DeepFreeze>());
        }

        public override bool NeedsSaving(Item item) {
            return item.type == ModContent.ItemType<Weapons.Ranged.SweetBusiness>();
        }

        public override void Load(Item item, TagCompound tag) {
            catalyst = tag.GetBool("catalyst");
        }

        public override TagCompound Save(Item item) {
            return new TagCompound {
                {"catalyst", catalyst}
            };
        }

        public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand) {
            if (item.type == ModContent.ItemType<Placeables.MusicBoxes.SepiksPrimeBox>()) {
                return false;
            }
            return null;
        }
    }
}