using TheDestinyMod.Core.Autoloading;
using Terraria;
using Terraria.Localization;

namespace TheDestinyMod.Content.Autoloading.Mono
{
	public class PlayerDropTombstone : IAutoloadable
	{
		public void IAutoloadable_Load(IAutoloadable createdObject) => On.Terraria.Player.DropTombstone += Player_DropTombstone;

		public void IAutoloadable_PostSetUpContent() { }

		public void IAutoloadable_Unload() { }

		private void Player_DropTombstone(On.Terraria.Player.orig_DropTombstone orig, Player self, int coinsOwned, NetworkText deathText, int hitDirection)
		{
			if (TheDestinyMod.currentSubworldID == string.Empty)
			{
				orig.Invoke(self, coinsOwned, deathText, hitDirection);
			}
		}
	}
}