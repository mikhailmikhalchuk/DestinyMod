using Terraria;
using Terraria.ID;
using DestinyMod.Common.Items;

namespace DestinyMod.Content.Items.Materials
{
	public class PlasteelPlating : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A durable hybrid plastic favored by Titans for its balance of strength and workability");
		}

		public override void DestinySetDefaults()
		{
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 10, 0);
		}

		public override void AddRecipes() => CreateRecipe(2)
			.AddRecipeGroup(RecipeGroupID.IronBar)
			.AddIngredient(ItemID.Obsidian, 3)
			.AddTile(TileID.Anvils)
			.Register();
	}
}