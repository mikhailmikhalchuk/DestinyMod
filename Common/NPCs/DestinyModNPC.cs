using Terraria.ModLoader;

namespace DestinyMod.Common.NPCs
{
	public abstract class DestinyModNPC : ModNPC
	{
		public sealed override void SetDefaults()
		{
			AutomaticSetDefaults();
			DestinySetDefaults();
		}

		public virtual void AutomaticSetDefaults()
		{
			
		}

		public virtual void DestinySetDefaults() { }
	}
}