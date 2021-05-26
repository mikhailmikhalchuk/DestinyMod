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
	public class SurosShotgun : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SUROS Shotgun");
			DisplayName.AddTranslation(GameCulture.Polish, "Strzelba SUROS");
			Tooltip.SetDefault("Fires a spread of bullets\nStandard SUROS Shotgun");
		}

		public override void SetDefaults() {
			item.damage = 35;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 50;
			item.useAnimation = 50;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Pink;
			item.UseSound = SoundID.Item36;
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .70f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			int numberProjectiles = 4 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++) {
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, 0);
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.7f;
            return true;
        }

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Obsidian, 20);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}