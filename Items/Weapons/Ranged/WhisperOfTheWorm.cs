using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ammo;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class WhisperOfTheWorm : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Whisper of the Worm");
			Tooltip.SetDefault("\"A Guardian's power makes a rich feeding ground. Do not be revolted\"");
		}
		public override void SetDefaults() {
			item.damage = 100;
			item.ranged = true;
			item.noMelee = true;
			item.width = 124;
			item.height = 40;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Purple;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/WhisperOfTheWorm");
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .80f;
			item.reuseDelay = 10;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			if (Main.rand.NextBool(4)) {
				Dust.NewDust(position += Vector2.Normalize(new Vector2(speedX, speedY)) * 90f, 1, 1, 63);
			}
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, -2);
		}
	}
}