using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class FoundVerdict : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Fires a spread of bullets\n\"Witness.\"");
		}

		public override void SetDefaults() {
			item.damage = 25;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Yellow;
			item.knockBack = 0;
			item.width = 78;
			item.height = 20;
			item.useTime = 60;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/FoundVerdict");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 16f;
			item.useAnimation = 60;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.9f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			for (int i = 0; i < 5; i++) {
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
				Projectile.NewProjectile(position.X, position.Y - 2, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.9f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-12, -3);
		}
	}
}