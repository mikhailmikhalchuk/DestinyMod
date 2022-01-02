using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.Buffs
{
    public class DestinyModBuff : ModBuff
    {
        public virtual void ModifyHitNPC(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void ModifyHitNPCWithProj(Player player, Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) { }

        public virtual void ModifyHitPvp(Player player, Item item, Player target, ref int damage, ref bool crit) { }

        public virtual void ModifyHitPvpWithProj(Player player, Projectile proj, Player target, ref int damage, ref bool crit) { }

        public virtual void OnHitNPC(Player player, Item item, NPC target, int damage, float knockback, bool crit) { }
    }
}