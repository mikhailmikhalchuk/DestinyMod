using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.Recipes.RecipeGroups;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Khvostov
{
	public class Khvostov7G0X : Khvostov
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Khvostov 7G-0X");

		public override void DestinySetDefaults()
		{
			Item.damage = 10;
			Item.useTime = 9;
			Item.useAnimation = 9;
			Item.rare = ItemRarityID.LightRed;
		}

		public override void AddRecipes() => CreateRecipe(1)
			.AddRecipeGroup(ModContent.GetInstance<AdamantiteOrTitaniumBars>().RecipeGroup, 12)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 20)
			.AddIngredient(ItemID.AdamantiteBar, 12)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}