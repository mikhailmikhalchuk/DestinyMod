using DestinyMod.Common.Recipes;
using Terraria.ID;

namespace DestinyMod.Content.Recipes.RecipeGroups
{
	public class GoldOrPlatinumBars : DestinyModRecipeGroup
	{
		public override string GetName() => "Gold or Platinum Bars";

		public override int[] Items() => new int[] { ItemID.GoldBar, ItemID.PlatinumBar };
	}
}