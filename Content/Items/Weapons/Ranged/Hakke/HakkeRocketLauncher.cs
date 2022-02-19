using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.Recipes.RecipeGroups;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public class HakkeRocketLauncher : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Standard Hakke Rocket Launcher");

		public override void DestinySetDefaults()
		{
			Item.damage = 20;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/RocketLauncher");
			Item.shoot = ProjectileID.RocketI;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Rocket;
			Item.reuseDelay = 125;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, ProjectileID.RocketI, damage / 3, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-50, -5);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 40)
			.AddRecipeGroup(ModContent.GetInstance<EvilMatter>().RecipeGroup, 10)
			.AddRecipeGroup(ModContent.GetInstance<EvilBars>().RecipeGroup, 7)
			.AddTile(TileID.Anvils)
			.Register();
	}
}