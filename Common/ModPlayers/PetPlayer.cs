using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class PetPlayer : ModPlayer
	{
		public bool Ghost;

		public override void ResetEffects()
		{
			Ghost = false;
		}
	}
}