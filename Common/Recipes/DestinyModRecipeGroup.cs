using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DestinyMod.Common.Recipes
{
	public abstract class DestinyModRecipeGroup : ILoadable
	{
		public RecipeGroup RecipeGroup { get; private set; }

		public abstract string GetName();

		public abstract int[] Items();

		public virtual void Load(Mod mod) => RecipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + GetName(), Items());

		public virtual void Unload() { }
	}
}