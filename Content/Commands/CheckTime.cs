using Terraria;
using Terraria.ModLoader;

namespace DestinyMod.Content.Commands
{
	public class CheckTimeCommand : ModCommand
	{
		public override CommandType Type => CommandType.World;

		public override string Command => "checkTime";

		public override string Usage => "/checkTime";

		public override string Description => "Checks the world time in ticks";

		public override void Action(CommandCaller caller, string input, string[] args) => Main.NewText("Time: " + Main.time + " @ " + Main.LocalPlayer.position);
	}
}