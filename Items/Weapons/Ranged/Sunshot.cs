using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Sunshot : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Fires explosive rounds\n\"Can't outrun the sunrise.\" -Liu Feng");
		}

		public override void SetDefaults() {
			item.damage = 84;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Red;
			item.knockBack = 0;
			item.width = 56;
			item.height = 30;
			item.useTime = 20;
			item.crit = 10;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/AceOfSpades");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 10f;
			item.useAnimation = 20;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.8f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.ExplosiveBullet, damage, knockBack, player.whoAmI);
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(5, 0);
		}
	}
}