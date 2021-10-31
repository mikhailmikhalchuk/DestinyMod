using Terraria;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Items.Buffers
{
    public class ThornRemnant : ModItem
    {
        public Player RemnantOwner
        {
            get; set;
        }

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Remnant");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults() {
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Gray;
        }

        public override bool ItemSpace(Player player) => true;

        public override bool CanPickup(Player player) {
            return player == RemnantOwner;
        }

        public override bool OnPickup(Player player) {
            var modPlayer = player.GetModPlayer<DestinyPlayer>();
            if (modPlayer.necroticDamageMult < 1f) {
                modPlayer.necroticDamageMult += 0.2f;
            }
            player.AddBuff(ModContent.BuffType<Buffs.MarkOfTheDevourer>(), 600);
            Main.PlaySound(SoundID.Grab, player.Center);
            return false;
        }

        public override void GrabRange(Player player, ref int grabRange) {
            grabRange = 128;
        }

        public override void PostUpdate() {
            Lighting.AddLight(item.Center, Color.Green.ToVector3());
        }
    }
}