using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.Recipes.RecipeGroups;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public class HakkeSidearm : HakkeCraftsmanshipWeapon
	{
		public override void DestinySetDefaults()
		{
			Item.damage = 16;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/HakkeSidearm");
			Item.shootSpeed = 16f;
			ShootOffset = new Vector2(0, -5);
		}

		public override Vector2? HoldoutOffset() => new Vector2(5, 2);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 25)
			.AddRecipeGroup(ModContent.GetInstance<GoldOrPlatinumBars>().RecipeGroup, 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}