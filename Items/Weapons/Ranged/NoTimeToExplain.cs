using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class NoTimeToExplain : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("No Time to Explain");
			Tooltip.SetDefault("Three round burst\n\"A single word etched onto the inside of the weapon's casing: Now.\"");
		}
		public override void SetDefaults() {
			item.damage = 50;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.width = 108;
			item.height = 36;
			item.useTime = 4;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Purple;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/NoTimeToExplain");
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .70f;
			item.reuseDelay = 10;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 3, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.7f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-22, 0);
		}
	}
}