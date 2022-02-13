using Terraria.ModLoader;

namespace DestinyMod.Common.ModSystems
{
	public class VaultOfGlassSystem : ModSystem
	{
		public static int OraclesKilledOrder = 1;

		public static int OraclesTimesRefrained;

		public static int Checkpoint;

		public override void PreSaveAndQuit()
		{
			OraclesKilledOrder = 1;
			OraclesTimesRefrained = 0;
			Checkpoint = 0;
		}
	}
}