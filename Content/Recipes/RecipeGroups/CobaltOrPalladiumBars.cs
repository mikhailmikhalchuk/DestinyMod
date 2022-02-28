using DestinyMod.Common.Recipes;
using Terraria.ID;

namespace DestinyMod.Content.Recipes.RecipeGroups
{
	public class CobaltOrPalladiumBars : DestinyModRecipeGroup
	{
		public override string GetName() => "Cobalt or Palladium Bars";

		public override int[] Items() => new int[] { ItemID.CobaltBar, ItemID.PalladiumBar };
	}
}