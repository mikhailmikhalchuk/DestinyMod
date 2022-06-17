using DestinyMod.Common.Items.Modifiers;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;
using DestinyMod.Content.Buffs;

namespace DestinyMod.Content.Items.Perks.Weapon.Traits
{
    public class HakkeCraftsmanship : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "Hakke Craftsmanship";
            Description = "Firing this weapon slightly improves defense, movement speed, and ranged damage.";
        }

        public override bool Shoot(Player player, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int hakkeBuff = ModContent.BuffType<HakkeBuff>();
            if (Main.rand.NextBool(10) && !player.HasBuff(hakkeBuff))
            {
                player.AddBuff(hakkeBuff, 90);
            }
            return true;
        }
    }
}