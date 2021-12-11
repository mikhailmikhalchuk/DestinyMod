using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using TheDestinyMod.Items.Weapons.Ranged;

namespace TheDestinyMod.Items
{
    public class DestinyGlobalItem : GlobalItem
    {
        public DestinyRarityType WeaponRarity;

        private readonly List<int> raidProhibitedItems = new List<int>()
        {
            ItemID.RodofDiscord,
            ItemID.CellPhone,
            ItemID.MagicMirror,
            ItemID.IceMirror,
            ItemID.RecallPotion,
            ItemID.TeleportationPotion,
            ItemID.WormholePotion
        };

        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;

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
            if ((raidProhibitedItems.Contains(item.type) || item.mountType != -1) && TheDestinyMod.currentSubworldID != string.Empty) {
                tooltips.Add(new TooltipLine(mod, "RaidUse", "Cannot use this item here")
                {
                    overrideColor = Color.Red
                });
            }
        }

        public override bool CanUseItem(Item item, Player player) {
            DestinyPlayer dPlayer = player.DestinyPlayer();
            if (dPlayer.stasisFrozen || dPlayer.detained || dPlayer.isThundercrash) {
                return false;
            }
            if ((raidProhibitedItems.Contains(item.type) || item.mountType != -1) && TheDestinyMod.currentSubworldID != string.Empty) {
                return false;
            }
            return base.CanUseItem(item, player);
        }

        public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand) {
            if (item.type == ModContent.ItemType<Placeables.MusicBoxes.SepiksPrimeBox>()) {
                return false;
            }
            return null;
        }
    }

    public enum DestinyRarityType : byte
    {
        Common,
        Uncommon,
        Rare,
        Legendary,
        Exotic
    }
}