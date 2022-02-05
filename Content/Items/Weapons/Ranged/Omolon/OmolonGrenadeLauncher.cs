using DestinyMod.Content.Recipes.RecipeGroups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Omolon
{
	public class OmolonGrenadeLauncher : OmolonWeapon
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Standard Omolon Grenade Launcher");

		public override void DestinySetDefaults()
		{
			Item.damage = 28;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.autoReuse = true;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item61;
			Item.shoot = ProjectileID.GrenadeI;
			Item.shootSpeed = 6f;
			Item.useAmmo = ItemID.Grenade;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 12), velocity, ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -5);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.SoulofLight, 15)
			.AddRecipeGroup(ModContent.GetInstance<AdamantiteOrTitaniumBars>().RecipeGroup, 8)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}