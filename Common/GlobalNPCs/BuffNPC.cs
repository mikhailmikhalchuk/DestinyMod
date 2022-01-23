using System;
using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Buffs;

namespace DestinyMod.Common.GlobalNPCs
{
	public class BuffNPC : GlobalNPC
	{
		public DestinyModBuff IsBuffIndexDestinyModBuff(NPC npc, int index)
		{
			int buffType = npc.buffType[index];
			ModBuff modBuff = ModContent.GetModBuff(buffType);
			if (modBuff == null || modBuff is not DestinyModBuff destinyModBuff)
			{
				return null;
			}

			return destinyModBuff;
		}

		public void ImplementBuffIteration(NPC npc, Action<DestinyModBuff> onSuccessfulIteration)
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
	}
}