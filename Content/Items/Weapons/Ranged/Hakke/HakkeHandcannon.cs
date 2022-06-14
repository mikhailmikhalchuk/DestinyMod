using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using DestinyMod.Common.Items.ItemTypes;
using DestinyMod.Content.Items.Materials;
using DestinyMod.Content.Recipes.RecipeGroups;
using DestinyMod.Content.Projectiles.Weapons.Ranged;

namespace DestinyMod.Content.Items.Weapons.Ranged.Hakke
{
	public class HakkeHandcannon : Gun
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Standard Hakke Handcannon");

		public override void DestinySetDefaults()
		{
			Item.damage = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = new SoundStyle("DestinyMod/Assets/Sounds/Item/Weapons/Ranged/HakkeHandcannon");
			Item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 10f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), velocity, ModContent.ProjectileType<HakkeBullet>(), damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(5, 2);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<RelicIron>(), 25)
			.AddRecipeGroup(ModContent.GetInstance<GoldOrPlatinumBars>().RecipeGroup, 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}