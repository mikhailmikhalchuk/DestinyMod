using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class FourthHorseman : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Fourth Horseman");
			Tooltip.SetDefault("\"It's not a holdout weapon; it's a pathfinder.\"");
		}

		public override void SetDefaults() {
			item.damage = 40;
			item.ranged = true;
			item.autoReuse = true;
			item.width = 100;
			item.height = 34;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Hakke2");
			item.shoot = 10;
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .70f;
			item.reuseDelay = 10;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			for (int i = 0; i < 4; i++) {
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, 0);
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.7f;
			return true;
		}
	}
}