using Terraria;
using DestinyMod.Common.Items.PerksAndMods;

namespace DestinyMod.Content.Items.Mods.Weapon
{
    public class BossSpec : ItemMod
    {
        public override void SetStaticDefaults() => Tooltip.SetDefault("Deals extra damage against boss enemies.");

        public override void DestinySetDefaults()
        {
            ApplyType = ItemType.Weapon;
            Item.maxStack = 99;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (target.boss)
            {
                damage = (int)(damage * 1.05f);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.boss)
            {
                damage = (int)(damage * 1.05f);
            }
        }
    }
}
