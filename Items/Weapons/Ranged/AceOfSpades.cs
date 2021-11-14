using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class AceOfSpades : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "As pików");
			Tooltip.SetDefault("Kills with this weapon cause the target to explode\n\"Folding was never an option.\"");
			Tooltip.AddTranslation(GameCulture.Polish, "Zabójstwa tą bronią powodują że cel wybucha\n\"Składanie nigdy nie wchodziło w grę\"");
		}

		public override void SetDefaults() {
			item.damage = 135;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Yellow;
			item.knockBack = 0;
			item.width = 58;
			item.height = 30;
			item.useTime = 15;
			item.crit = 10;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/AceOfSpades");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 10f;
			item.useAnimation = 15;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.8f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            Projectile.NewProjectile(position.X, position.Y - 7, speedX, speedY, ModContent.ProjectileType<AceBullet>(), damage, knockBack, player.whoAmI);
			return false;
        }

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(5, 0);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunsmithMaterials>(), 50);
			recipe.AddIngredient(ItemID.FragmentSolar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}