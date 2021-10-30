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
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 19));
        }

        public override void SetDefaults() {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Gray;
        }

        public override bool ItemSpace(Player player) => true;

        public override bool CanPickup(Player player) {
            if (RemnantOwner == null) {
                return true;
            }
            return player == RemnantOwner;
        }

        public override bool OnPickup(Player player) {
            var modPlayer = player.GetModPlayer<DestinyPlayer>();
            if (modPlayer.thornPierceAdd < 1f) {
                modPlayer.thornPierceAdd += 0.2f;
            }
            player.AddBuff(ModContent.BuffType<Buffs.MarkOfTheDevourer>(), 600);
            return false;
        }
    }
}