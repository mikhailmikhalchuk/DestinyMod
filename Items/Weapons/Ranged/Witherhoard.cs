using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Witherhoard : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Projectiles blight surrounding tiles on impact\nEnemies that come in contact with the blight will be damaged\n\"Like a one-man private security company.\"");
		}

		public override void SetDefaults() {
			item.damage = 35;
			item.ranged = true;
			item.width = 82;
			item.height = 30;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Pink;
			item.UseSound = SoundID.Item61;
			item.shoot = ProjectileID.GrenadeI;
			item.shootSpeed = 8f;
			item.useAmmo = ItemID.Grenade;
			item.scale = .80f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 7, speedX, speedY, ProjectileID.GrenadeI, damage, knockBack, player.whoAmI);
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-15, 0);
		}
	}
}