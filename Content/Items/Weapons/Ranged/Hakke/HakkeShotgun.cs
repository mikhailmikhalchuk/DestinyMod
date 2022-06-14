using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.Recipes.RecipeGroups;
using Terraria.DataStructures;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public class HakkeShotgun : HakkeCraftsmanshipWeapon
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Fires a spread of bullets\nHas a chance to grant the 'Hakke Craftsmanship' buff on use");

		public override void DestinySetDefaults()
		{
			Item.damage = 10;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/HakkeShotgun");
			Item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			HandleMuzzleOffset(ref position, velocity);

			int numberProjectiles = 5 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
				Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<HakkeBullet>(), damage, knockback, player.whoAmI);
			}

			HandleApplyingHakke(player);
			return false;
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