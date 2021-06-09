using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Materials
{
	public class PlasteelPlating : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("A durable hybrid plastic favored by Titans for its balance of strength and workability");
		}

		public override void SetDefaults() {
			item.height = 16;
			item.width = 18;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 10, 0);
		}

        public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar);
			recipe.AddIngredient(ItemID.Obsidian, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
		}
    }
}