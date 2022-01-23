using DestinyMod.Common.Recipes;
using Terraria.ID;

namespace DestinyMod.Content.Recipes.RecipeGroups
{
	public class AdamantiteOrTitaniumBars : DestinyModRecipeGroup
	{
		public override string GetName() => "Adamantite or Titanium Bars";

		public override int[] Items() => new int[] { ItemID.AdamantiteBar, ItemID.TitaniumBar };
	}
}