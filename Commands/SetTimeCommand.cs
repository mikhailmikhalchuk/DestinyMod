using Terraria;
using Terraria.ModLoader;

namespace TheDestinyMod.Commands
{
	public class SetTimeCommand : ModCommand
	{
		public override CommandType Type
			=> CommandType.World;

		public override string Command
			=> "setTime";

		public override string Usage
			=> "/setTime time";

		public override string Description
			=> "Sets the world time in ticks";

		public override void Action(CommandCaller caller, string input, string[] args) {
            Main.time = int.Parse(args[0]);
		}
	}
}