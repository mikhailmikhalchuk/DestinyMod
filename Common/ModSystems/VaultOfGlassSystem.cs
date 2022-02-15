using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace DestinyMod.Common.ModSystems
{
	public class VaultOfGlassSystem : ModSystem
	{
		public static int OraclesKilledOrder = 1;

		public static int OraclesTimesRefrained;

		public static int Checkpoint;

		public static int RaidClears;

		public static Vector2 TilePosition;

		public override void PreSaveAndQuit()
		{
			OraclesKilledOrder = 1;
			OraclesTimesRefrained = 0;
			Checkpoint = 0;
			RaidClears = 0;
			TilePosition = Vector2.Zero;
		}
	}
}