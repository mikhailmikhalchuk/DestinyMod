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
            Description.SetDefault("20% reduced defense");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

		public static int UndoPlayerDefense(int damage, int defense, float percentage = 0.2f)
		{
			if (Main.masterMode)
			{
				return damage + (int)(defense * percentage);
			}
			else if (Main.expertMode)
			{
				return damage + (int)(defense * 0.25f * percentage);
			}
			else
			{
				return damage + (int)(defense * 0.5f * percentage);
			}
		}

		public static int UndoNPCDefense(int damage, int defense, float percentage = 0.2f) => damage + (int)(defense * 0.5f * percentage);

		public override void ModifyHitByNPC(Player player, NPC npc, ref int damage, ref bool crit) => damage = UndoPlayerDefense(damage, player.statDefense);

		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit) => damage = UndoNPCDefense(damage, npc.defense);

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => damage = UndoNPCDefense(damage, npc.defense);

		public override void ModifyHitByProjectile(Player player, Projectile proj, ref int damage, ref bool crit) => damage = UndoPlayerDefense(damage, player.statDefense);

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