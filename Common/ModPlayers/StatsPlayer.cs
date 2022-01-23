using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class StatsPlayer : ModPlayer
	{
		public int ItemReuse = 0;

		public int ChannelTime = 0;

		public override void ResetEffects()
		{
			ItemReuse--;

			if (Player.channel)
			{
				ChannelTime++;
			}
		}
	}
}