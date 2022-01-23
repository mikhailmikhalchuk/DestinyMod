using Terraria;
using Terraria.DataStructures;
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

        public virtual bool PreKill(Player player, double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) => true;

        public static void ApplyDebuff(Player player, int damage)
        {
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }

            player.lifeRegenTime = 0;
            player.lifeRegen -= damage;
        }

        public virtual void UpdateBadLifeRegen(Player player) { }

        public static void ApplyDebuff(NPC npc, int damage)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= damage;
        }

        public virtual void UpdateLifeRegen(NPC npc, ref int damage) { }
    }
}