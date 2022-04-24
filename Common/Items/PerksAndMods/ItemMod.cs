using Terraria;

namespace DestinyMod.Common.Items.PerksAndMods
{
    public abstract class ItemMod : DestinyModItem
    {
        public ItemType ApplyType;

        public virtual void Update(Player player) { }

        public virtual void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) { }
    }
}
