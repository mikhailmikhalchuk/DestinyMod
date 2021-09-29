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
	public class HakkeShotgun : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Strzelba Hakke");
			Tooltip.SetDefault("Fires a spread of bullets\nHas a chance to grant the \"Hakke Craftsmanship\" buff on use");
			Tooltip.AddTranslation(GameCulture.Polish, "Rozsyła serie kul\nMa szansę zapewnić buff \"Rzemiosło Hakke\" po użyciu");
		}

		public override void SetDefaults() {
			item.damage = 10;
			item.ranged = true;
			item.width = 100;
			item.height = 40;
			item.useTime = 50;
			item.useAnimation = 50;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Hakke2");
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .70f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
				position += muzzleOffset;
			}
			int numberProjectiles = 5 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++) {
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<HakkeBullet>(), damage, knockBack, player.whoAmI);
			}
			if (Main.rand.NextBool(10) && !player.GetModPlayer<DestinyPlayer>().hakkeCraftsmanship) {
				player.AddBuff(ModContent.BuffType<HakkeBuff>(), 90);
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

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RelicIron>(), 35);
			recipe.AddIngredient(ItemID.ShadowScale, 12);
			recipe.AddIngredient(ItemID.DemoniteBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RelicIron>(), 35);
			recipe.AddIngredient(ItemID.TissueSample, 12);
			recipe.AddIngredient(ItemID.CrimtaneBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}