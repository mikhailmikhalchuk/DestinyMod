using DestinyMod.Common.Items.ItemTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Weapons.Ranged.Suros
{
	public class SurosGrenadeLauncher : Gun
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUROS Grenade Launcher");
			Tooltip.SetDefault("Standard SUROS Grenade Launcher");
		}

		public override void SetDefaults()
		{
			Item.damage = 35;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 70;
			Item.height = 30;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.autoReuse = true;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item61;
			Item.shoot = ProjectileID.GrenadeI;
			Item.shootSpeed = 8f;
			Item.useAmmo = ItemID.Grenade;
			Item.scale = .80f;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 7), velocity, ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -5);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Obsidian, 20).AddIngredient(ItemID.HallowedBar, 10).AddTile(TileID.MythrilAnvil).Register();
		}
	}
}