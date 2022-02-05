using DestinyMod.Common.Recipes;
using Terraria.ID;

namespace DestinyMod.Content.Recipes.RecipeGroups
{
	public class EvilMatter : DestinyModRecipeGroup
	{
		public override string GetName() => "Shadow Scale or Tissue Sample";

		public override int[] Items() => new int[] { ItemID.ShadowScale, ItemID.TissueSample };
	}
}