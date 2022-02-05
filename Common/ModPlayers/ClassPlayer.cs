using Terraria.ModLoader;

namespace DestinyMod.Common.ModPlayers
{
	public class ClassPlayer : ModPlayer
	{
		public DestinyClassType ClassType;

		public bool ExoticEquipped = false;

		public override void Initialize()
		{
			ClassType = DestinyClassType.None;
		}

		public override void ResetEffects()
		{
			ExoticEquipped = false;
		}
	}
}