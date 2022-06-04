using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.Recipes.RecipeGroups;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public class HakkeScoutRifle : HakkeCraftsmanshipWeapon
	{
		public override void DestinySetDefaults()
		{
			Item.damage = 25;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.knockBack = 4;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/HakkeLongRifle");
			Item.shootSpeed = 300f;
			ShootOffset = new Vector2(0, -3);
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 35)
			.AddRecipeGroup(ModContent.GetInstance<EvilMatter>().RecipeGroup, 12)
			.AddRecipeGroup(ModContent.GetInstance<EvilBars>().RecipeGroup, 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}