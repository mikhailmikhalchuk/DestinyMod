using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.GlobalNPCs
{
	public class DebuffNPC : GlobalNPC
	{
		public int OutbreakInfectiousDuration;

		public int OutbreakHitCount;

		public Player NecroticApplier;

		public override bool InstancePerEntity => true;

		public override void PostAI(NPC npc)
		{
			if (--OutbreakHitCount <= 0)
			{
				OutbreakHitCount = 0;
			}
		}
	}
}