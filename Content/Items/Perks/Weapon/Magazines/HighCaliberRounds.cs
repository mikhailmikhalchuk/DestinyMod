using DestinyMod.Common.GlobalItems;
using DestinyMod.Common.Items.Modifiers;
using DestinyMod.Common.ModPlayers;
using Terraria;

namespace DestinyMod.Content.Items.Perks.Weapon.Magazines
{
    public class HighCaliberRounds : ItemPerk
    {
        public override void SetDefaults()
        {
            DisplayName = "High-Caliber Rounds";
            Description = "Bullets fired from this weapon have greater knockback."
                + "\n- Slightly increases range";
        }

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => knockback += 2.5f;

        public override void Update(Player player)
        {
            ItemDataPlayer itemDataPlayer = player.GetModPlayer<ItemDataPlayer>();
            if (itemDataPlayer.Range >= 0)
            {
                itemDataPlayer.Range += 5;
            }
        }
    }
}