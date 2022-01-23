using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.GlobalNPCs
{
	public class OutbreakNPC : GlobalNPC
	{
		public int OutbreakInfectiousDuration;

		public int OutbreakHitCount;

		public override void PostAI(NPC npc)
		{
			if (--OutbreakHitCount <= 0)
			{
				OutbreakHitCount = 0;
			}
		}
	}
}