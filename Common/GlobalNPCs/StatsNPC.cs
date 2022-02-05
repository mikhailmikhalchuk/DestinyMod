using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Common.GlobalNPCs
{
	public class StatsNPC : GlobalNPC
	{
		public int HighlightDuration;

		public override void PostAI(NPC npc)
		{
			if (--HighlightDuration <= 0)
			{
				HighlightDuration = 0;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (HighlightDuration > 0)
			{
				if (drawColor.R < 255)
				{
					drawColor.R = 255;
				}

				if (drawColor.G < 50)
				{
					drawColor.G = 50;
				}

				if (drawColor.B < 50)
				{
					drawColor.B = 50;
				}
			}
		}
	}
}