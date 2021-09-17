using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Fatebringer : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("\"Delivering the inevitable, one pull at a time.\"");
		}

		public override void SetDefaults() {
			item.damage = 60;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Red;
			item.knockBack = 0;
			item.width = 36;
			item.height = 20;
			item.useTime = 20;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/HandCannon120");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 40f;
			item.useAnimation = 20;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			position.Y -= 3;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(5, 0);
		}
	}
}