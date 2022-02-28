using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace DestinyMod
{
	public class DestinyMod : Mod
	{
		public static DestinyMod Instance { get; set; }

		public DestinyMod() => Instance = this;

		public override void Unload() => Instance = null;
    }
}