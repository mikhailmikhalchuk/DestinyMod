using Terraria.ModLoader;

namespace DestinyMod.Common.Tiles
{
	public abstract class DestinyModTile : ModTile
	{
		public sealed override void SetStaticDefaults()
		{
			AutomaticSetDefaults();
			DestinySetStaticDefaults();
		}

		public virtual void AutomaticSetDefaults() { }

		public virtual void DestinySetStaticDefaults() { }
	}
}