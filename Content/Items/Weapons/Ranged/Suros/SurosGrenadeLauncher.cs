using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace DestinyMod.Content.Items.Weapons.Ranged.Suros
{
	public class SurosGrenadeLauncher : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUROS Grenade Launcher");
			Tooltip.SetDefault("Standard SUROS Grenade Launcher");
		}

		public override void DestinySetDefaults()
		{
			Item.damage = 35;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.autoReuse = true;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item61;
			Item.shoot = ProjectileID.GrenadeI;
			Item.shootSpeed = 8f;
			Item.useAmmo = AmmoID.Rocket;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-10, -5);

		public override void AddRecipes() => CreateRecipe(1)
			.AddIngredient(ItemID.Obsidian, 20)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}