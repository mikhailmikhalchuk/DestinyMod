using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TheDestinyMod.Items.Weapons.Supers
{
    public abstract class SuperClass : ModItem
    {
        public sealed override void SetDefaults() {
            item.rare = ItemRarityID.Expert;
            SetSuperDefaults();
        }

        public virtual void SetSuperDefaults() {

        }

        public override bool CanUseItem(Player player) {
            var modPlayer = player.GetModPlayer<DestinyPlayer>();
            return modPlayer.superActiveTime > 0;
        }

        public override bool? PrefixChance(int pre, UnifiedRandom rand) {
            return false;
        }
    }
}