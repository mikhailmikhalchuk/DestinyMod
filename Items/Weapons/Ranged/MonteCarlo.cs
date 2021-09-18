using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;
using TheDestinyMod.Projectiles.Ranged;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class MonteCarlo : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Defeating an enemy with this weapon stack one second of Monte Carlo Method\n\"There will always be paths to tread and methods to try. Roll with it.\"");
			Tooltip.AddTranslation(GameCulture.Polish, "Pokonanie przeciwnika korzystając z tej broni zapewnia jeden ładunek \"Metody Monte Carlo\"\n\"Zawsze będą dostępne ścieżki i metody do wybróbowania. Niech los zadecyduje\"");
		}

		public override void SetDefaults() {
			item.damage = 30;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.rare = ItemRarityID.Red;
			item.knockBack = 0;
			item.width = 70;
			item.height = 30;
			item.useTime = 9;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MonteCarlo");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 20f;
			item.useAnimation = 9;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50);
			recipe.AddIngredient(ItemID.FragmentVortex, 15);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			Projectile.NewProjectile(position.X, position.Y - 3, speedX, speedY, ModContent.ProjectileType<MonteBullet>(), damage, knockBack, player.whoAmI);
            return false;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-15, 0);
		}
	}
}