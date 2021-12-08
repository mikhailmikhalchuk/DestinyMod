using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class NemesisStar : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("\"What is the answer when the question is extinction?\"");
		}

		public override void SetDefaults() {
			item.damage = 30;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.rare = ItemRarityID.Red;
			item.knockBack = 0;
			item.width = 76;
			item.height = 34;
			item.useTime = 9;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/NemesisStar");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 20f;
			item.useAnimation = 9;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = .80f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 3, speedX, speedY, type, damage, knockBack, player.whoAmI);
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