using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.Recipes.RecipeGroups;

namespace DestinyMod.Content.Items.Weapons.Ranged
{
	public class HakkeSniper : HakkeCraftsmanshipWeapon
	{
		public override void DestinySetDefaults()
		{
			Item.damage = 50;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/HakkeLongRifle");
			Item.shootSpeed = 300f;
			ShootOffset = new Vector2(0, -3);
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 25)
			.AddRecipeGroup(ModContent.GetInstance<GoldOrPlatinumBars>().RecipeGroup, 8)
			.AddTile(TileID.Anvils)
			.Register();
	}
}