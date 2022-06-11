using Terraria;
using DestinyMod.Common.Buffs;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace DestinyMod.Content.Buffs.Debuffs
{
    public class Judgment : DestinyModBuff
    {
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Judgment");
            Description.SetDefault("All damage taken is critical");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

		public override void ModifyHitByNPC(Player player, NPC npc, ref int damage, ref bool crit) => crit = true;

		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit) => crit = true;

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => crit = true;

		public override void ModifyHitByProjectile(Player player, Projectile proj, ref int damage, ref bool crit) => crit = true;

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
			drawColor = Color.Yellow;
			if (Main.rand.NextBool(10))
			{
				Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Firework_Yellow);
				dust.noGravity = true;
			}
		}
    }
}