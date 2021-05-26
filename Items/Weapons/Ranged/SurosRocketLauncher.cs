using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class SurosRocketLauncher : ModItem
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("SUROS Rocket Launcher");
			DisplayName.AddTranslation(GameCulture.Polish, "Wyrzutnia Rakiet SUROS");
			Tooltip.SetDefault("Standard SUROS Rocket Launcher");
		}

		public override void SetDefaults() {
			item.damage = 55;
			item.ranged = true;
			item.width = 100;
			item.height = 30;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.autoReuse = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Pink;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Riskrunner");
			item.shoot = ProjectileID.RocketI;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Rocket;
			item.scale = .80f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 7, speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(-50, -5);
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