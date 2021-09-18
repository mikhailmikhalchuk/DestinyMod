using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Buffs;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class PraedythsRevenge : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Praedyth's Revenge");
			Tooltip.SetDefault("\"Praedyth's fall isn't over... because it hasn't happened yet... and it will happen again.\"");
		}

		public override void SetDefaults() {
			item.damage = 50;
			item.ranged = true;
			item.width = 116;
			item.height = 28;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/PraedythsRevenge");
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 300f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .85f;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.85f;
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 6, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, -5);
		}
	}
}