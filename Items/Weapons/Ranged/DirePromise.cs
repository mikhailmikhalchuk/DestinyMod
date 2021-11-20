using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class DirePromise : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("\"If you think this world ends well, you are mistaken.\"");
		}

		public override void SetDefaults() {
			item.damage = 60;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.LightRed;
			item.knockBack = 0;
			item.width = 56;
			item.height = 28;
			item.useTime = 20;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/HandCannon120");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 40f;
			item.useAnimation = 20;
			item.shoot = 10;
			item.scale = .80f;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 6, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(5, 2);
		}
	}
}