using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TheDestinyMod.Tiles;
using Microsoft.Xna.Framework;

namespace TheDestinyMod.Commands
{
	public class CheckTimeCommand : ModCommand
	{
		public override CommandType Type
			=> CommandType.World;

		public override string Command
			=> "checkTime";

		public override string Usage
			=> "/checkTime";

		public override string Description
			=> "Checks the world time in ticks";

		public override void Action(CommandCaller caller, string input, string[] args) {
            Main.NewText(Main.time);
			Main.NewText(Main.LocalPlayer.position);
		}
	}
}