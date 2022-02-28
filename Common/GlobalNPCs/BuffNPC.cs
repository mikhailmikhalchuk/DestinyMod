using System;
using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Buffs;
using Microsoft.Xna.Framework;

namespace DestinyMod.Common.GlobalNPCs
{
	public class BuffNPC : GlobalNPC
	{
		public static DestinyModBuff IsBuffIndexDestinyModBuff(NPC npc, int index)
		{
			int buffType = npc.buffType[index];
			ModBuff modBuff = ModContent.GetModBuff(buffType);
			if (modBuff == null || modBuff is not DestinyModBuff destinyModBuff)
			{
				return null;
			}

			return destinyModBuff;
		}

		public static void ImplementBuffIteration(NPC npc, Action<DestinyModBuff> onSuccessfulIteration)
		{
			for (int indexer = 0; indexer < npc.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(npc, indexer) is DestinyModBuff destinyModBuff)
				{
					onSuccessfulIteration(destinyModBuff);
				}
			}
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			for (int indexer = 0; indexer < npc.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(npc, indexer) is DestinyModBuff destinyModBuff)
				{
					destinyModBuff.UpdateLifeRegen(npc, ref damage);
				}
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			for (int indexer = 0; indexer < npc.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(npc, indexer) is DestinyModBuff destinyModBuff)
				{
					destinyModBuff.DrawEffects(npc, ref drawColor);
				}
			}
		}

		public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			bool vanillaDamageFormula = true;
			for (int indexer = 0; indexer < npc.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(npc, indexer) is DestinyModBuff destinyModBuff 
					&& !destinyModBuff.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit))
				{
					vanillaDamageFormula = false;
				}
			}

			return vanillaDamageFormula;
		}

		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
		{
			for (int indexer = 0; indexer < npc.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(npc, indexer) is DestinyModBuff destinyModBuff)
				{
					destinyModBuff.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
				}
			}
		}

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			for (int indexer = 0; indexer < npc.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(npc, indexer) is DestinyModBuff destinyModBuff)
				{
					destinyModBuff.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);
				}
			}
		}
	}
}