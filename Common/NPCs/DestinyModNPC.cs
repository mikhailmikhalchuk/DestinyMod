using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DestinyMod.Common.NPCs
{
	public abstract class DestinyModNPC : ModNPC
	{
		public sealed override void SetDefaults()
		{
			AutomaticSetDefaults();
			DestinySetDefaults();
		}

		public virtual void AutomaticSetDefaults() { }

		public virtual void DestinySetDefaults() { }

		/// <summary>
		/// Do NOT save instanced data here.
		/// </summary>
		/// <param name="tagCompound">The <see cref="TagCompound"/> to add data to.</param>
		public virtual void Save(TagCompound tagCompound) { }
		
		/// <summary>
		/// Do NOT load instanced data here.
		/// </summary>
		/// <param name="tagCompound">The <see cref="TagCompound"/> to load data from.</param>
		public virtual void Load(TagCompound tagCompound) { }
	}
}