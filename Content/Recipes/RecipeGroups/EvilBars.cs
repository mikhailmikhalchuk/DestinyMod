using DestinyMod.Common.Recipes;
using Terraria.ID;

namespace DestinyMod.Content.Recipes.RecipeGroups
{
	public class EvilBars : DestinyModRecipeGroup
	{
		public override string GetName() => "Demonite or Crimtane Bars";

		public override int[] Items() => new int[] { ItemID.DemoniteBar, ItemID.CrimtaneBar };
	}
}