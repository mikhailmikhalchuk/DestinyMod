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
	public class HakkeSidearm : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Broń Boczna Hakke");
			Tooltip.SetDefault("Has a chance to grant the \"Hakke Craftsmanship\" buff on use");
			Tooltip.AddTranslation(GameCulture.Polish, "Ma szansę zapewnić buff \"Rzemiosło Hakke\" po użyciu");
		}
		public override void SetDefaults() {
			item.damage = 16;
			item.ranged = true;
			item.width = 34;
			item.height = 22;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/HakkeSidearm");
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
				position += muzzleOffset;
			}
			Projectile.NewProjectile(position.X, position.Y - 5, speedX, speedY, ModContent.ProjectileType<HakkeBullet>(), damage, knockBack, player.whoAmI);
			if (Main.rand.NextBool(10) && !player.DestinyPlayer().hakkeCraftsmanship) {
				player.AddBuff(ModContent.BuffType<HakkeBuff>(), 90);
			}
            return false;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(5, 2);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RelicIron>(), 25);
			recipe.AddIngredient(ItemID.PlatinumBar, 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RelicIron>(), 25);
			recipe.AddIngredient(ItemID.GoldBar, 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}