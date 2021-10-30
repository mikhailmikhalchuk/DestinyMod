using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Telesto : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Hold down the trigger to fire\nProjectiles attach to surfaces and explode after a short delay\n\"Vestiges of the Queen's Harbingers yet linger among Saturn's moons.\"");
		}

		public override void SetDefaults() {
			item.damage = 35;
			item.crit = 4;
			item.ranged = true;
			item.noMelee = true;
			item.channel = true;
			item.rare = ItemRarityID.Yellow;
			item.knockBack = 0;
			item.width = 60;
			item.height = 28;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 10f;
			item.useAnimation = 15;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.9f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 2, speedX, speedY, ModContent.ProjectileType<Projectiles.Ranged.FusionShot>(), damage, knockBack, player.whoAmI, 7, ModContent.ProjectileType<Projectiles.Ranged.TelestoBullet>());
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.9f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-1, 0);
		}
	}
}