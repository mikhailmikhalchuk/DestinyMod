using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.GlobalNPCs
{
	public class DebuffNPC : GlobalNPC
	{
		public int OutbreakHitDuration;

		public int OutbreakHitCount;

		public Player NecroticApplier;

		public override bool InstancePerEntity => true;

		public override void PostAI(NPC npc)
		{
			if (--OutbreakHitDuration <= 0)
			{
				OutbreakHitCount = 0;
			}
		}
	}
}