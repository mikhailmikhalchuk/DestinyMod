using DestinyMod.Common.Items.PerksAndMods;
using Terraria;

namespace DestinyMod.Content.Items.Mods.Weapon
{
    public class MinorSpec : ItemMod
    {
        public static readonly int RankAndFileHealthPoint = 5000;

        public override void SetDefaults()
        {
            DisplayName = "Minor Spec";
            Description = "Deals extra damage against rank-and-file enemies.";
        }

        public void Function(NPC target, ref int damage)
        {
            if (target.lifeMax > RankAndFileHealthPoint)
            {
                return;
            }

            damage = (int)(damage * 1.05f);
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) => Function(target, ref damage);

        public override void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => Function(target, ref damage);
    }
}