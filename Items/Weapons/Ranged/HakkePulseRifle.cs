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
	public class HakkePulseRifle : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Karabin Pulsacyjny Hakke");
			Tooltip.SetDefault("Three round burst\nHas a chance to grant the \"Hakke Craftsmanship\" buff on use");
			Tooltip.AddTranslation(GameCulture.Polish, "Seria trzech rund\nMa szansę zapewnić buff \"Rzemiosło Hakke\" po użyciu");
		}

		public override void SetDefaults() {
			item.damage = 14;
			item.ranged = true;
			item.width = 70;
			item.height = 30;
			item.useTurn = false;
			item.useTime = 4;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/RedDeath");
			item.shoot = 10; 
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .85f;

		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
				position += muzzleOffset;
			}
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			player.GetModPlayer<DestinyPlayer>().destinyWeaponDelay = 14;
			Projectile.NewProjectile(position.X, position.Y - 1, speedX, speedY, ModContent.ProjectileType<HakkeBullet>(), damage, knockBack, player.whoAmI);
			if (Main.rand.NextBool(10) && !player.GetModPlayer<DestinyPlayer>().hakkeCraftsmanship) {
				player.AddBuff(ModContent.BuffType<HakkeBuff>(), 90);
			}
            return false;
		}

		public override bool CanUseItem(Player player) {
			if (player.GetModPlayer<DestinyPlayer>().destinyWeaponDelay > 0) {
				return false;
			}
			return base.CanUseItem(player);
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.85f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(-5, 0);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RelicIron>(), 40);
			recipe.AddIngredient(ItemID.Bone, 40);
			recipe.AddIngredient(ItemID.HellstoneBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}