using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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

		public override void SaveData(TagCompound tag)
		{
			tag.Add("ClassType", (byte)ClassType);
		}

		public override void LoadData(TagCompound tag)
		{
			ClassType = (DestinyClassType)tag.Get<byte>("ClassType");
		}
	}
}