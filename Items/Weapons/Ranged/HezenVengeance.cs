using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class HezenVengeance : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("\"The Hezen Axis Mind is tireless and infinite. While it cannot be stopped... it can be paused.\"");
		}

		public override void SetDefaults() {
			item.damage = 20;
			item.ranged = true;
			item.width = 110;
			item.height = 46;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/HezenVengeance");
			item.shoot = ProjectileID.RocketI;
			item.shootSpeed = 16f;
			item.useAmmo = ItemID.Grenade;
			item.scale = .80f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 7, speedX, speedY, ProjectileID.RocketI, damage / 3, knockBack, player.whoAmI);
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-50, -5);
		}
	}
}