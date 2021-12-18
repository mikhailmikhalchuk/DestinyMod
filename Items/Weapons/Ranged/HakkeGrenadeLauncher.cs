using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class HakkeGrenadeLauncher : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Granatnik Hakke");
			Tooltip.SetDefault("Standard Hakke Grenade Launcher");
		}

		public override void SetDefaults() {
			item.damage = 15;
			item.ranged = true;
			item.width = 54;
			item.height = 24;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item61;
			item.shoot = ProjectileID.GrenadeI;
			item.shootSpeed = 3f;
			item.useAmmo = ItemID.Grenade;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 7, speedX, speedY, ProjectileID.GrenadeI, damage, knockBack, player.whoAmI);
            return false;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, -3);
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