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
	public class SurosSidearm : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SUROS Sidearm");
			DisplayName.AddTranslation(GameCulture.Polish, "Broń Boczna SUROS");
			Tooltip.SetDefault("Standard SUROS Sidearm");
		}
		public override void SetDefaults() {
			item.damage = 30;
			item.ranged = true;
			item.width = 46;
			item.height = 30;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Pink;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JadeRabbit");
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
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
			return new Vector2(5, 2);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Obsidian, 15);
			recipe.AddIngredient(ItemID.HallowedBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}