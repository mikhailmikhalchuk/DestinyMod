using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class TicuusDivination : ModItem
	{
        public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ticuu's Divination");
			Tooltip.SetDefault("Fires 3 homing arrows\nRight click to fire a single, more powerful arrow\n\"Three points, pushed through forever.\"");
		}

		public override void SetDefaults() {
			item.damage = 50;
			item.ranged = true;
			item.noMelee = true;
			item.width = 36;
			item.height = 114;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Purple;
			item.UseSound = SoundID.Item5;
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.scale = 0.65f;
			item.useAmmo = AmmoID.Arrow;
		}

        public override bool AltFunctionUse(Player player) {
			return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			if (player.altFunctionUse == 2) {
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<CausalityArrow>(), damage, knockBack, player.whoAmI);
				return false;
			}
			for (int i = 0; i < 3; i++) {
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<SacredFlame>(), damage / 2, knockBack, player.whoAmI);
			}
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.65f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(0, 2);
		}
	}
}