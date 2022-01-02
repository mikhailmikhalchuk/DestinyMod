using System;
using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Buffs;

namespace DestinyMod.Common.ModPlayers
{
	public class BuffPlayer : ModPlayer
	{
		public DestinyModBuff IsBuffIndexDestinyModBuff(int index)
		{
			int buffType = Player.buffType[index];
			ModBuff modBuff = ModContent.GetModBuff(buffType);
			if (modBuff == null || modBuff is not DestinyModBuff destinyModBuff)
			{
				return null;
			}

			return destinyModBuff;
		}

		public void ImplementBuffIteration(Action<DestinyModBuff> onSuccessfulIteration)
		{
			for (int indexer = 0; indexer < Player.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(indexer) is DestinyModBuff destinyModBuff)
				{
					onSuccessfulIteration(destinyModBuff);
				}
			}
		}

		public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			for (int indexer = 0; indexer < Player.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(indexer) is DestinyModBuff destinyModBuff)
				{
					destinyModBuff.ModifyHitNPC(Player, item, target, ref damage, ref knockback, ref crit); // Relegated to not simply code :penisive:
				}
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			for (int indexer = 0; indexer < Player.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(indexer) is DestinyModBuff destinyModBuff)
				{
					destinyModBuff.ModifyHitNPCWithProj(Player, proj, target, ref damage, ref knockback, ref crit, ref hitDirection); // Relegated to not simply code :penisive:
				}
			}
		}

		public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
		{
			for (int indexer = 0; indexer < Player.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(indexer) is DestinyModBuff destinyModBuff)
				{
					destinyModBuff.ModifyHitPvp(Player, item, target, ref damage, ref crit); // Relegated to not simply code :penisive:
				}
			}
		}

		public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
		{
			for (int indexer = 0; indexer < Player.buffType.Length; indexer++)
			{
				if (IsBuffIndexDestinyModBuff(indexer) is DestinyModBuff destinyModBuff)
				{
					destinyModBuff.ModifyHitPvpWithProj(Player, proj, target, ref damage, ref crit); // Relegated to not simply code :penisive:
				}
			}
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit) =>
			ImplementBuffIteration(destinyModBuff => destinyModBuff.OnHitNPC(Player, item, target, damage, knockback, crit));
	}
}