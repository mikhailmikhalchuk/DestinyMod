using DestinyMod.Common.Recipes;
using Terraria.ID;

namespace DestinyMod.Content.Recipes.RecipeGroups
{
	public class MythrilOrOrichalcumBars : DestinyModRecipeGroup
	{
		public override string GetName() => "Mythril or Orichalcum Bars";

		public override int[] Items() => new int[] { ItemID.MythrilBar, ItemID.OrichalcumBar };
	}
}