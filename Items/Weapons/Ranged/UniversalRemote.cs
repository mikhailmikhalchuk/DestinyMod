using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class UniversalRemote : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Pilot Universalny");
			Tooltip.SetDefault("Fires a spread of bullets\n\"To the untrained eye this beast is a junker. To the trained eye, however, this junker... is a beast.\"");
			Tooltip.AddTranslation(GameCulture.Polish, "Rozsyła serie kul\n\"Dla niewprawnego oka ta bestia to złodziej. Jednak dla wprawnego oka ten złodziej jest bestią\"");
		}

		public override void SetDefaults() {
			item.damage = 25;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Yellow;
			item.knockBack = 0;
			item.width = 80;
			item.height = 32;
			item.useTime = 60;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/UniversalRemote");
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 16f;
			item.useAnimation = 60;
			item.shoot = 10;
			item.useAmmo = AmmoID.Bullet;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.scale = 0.8f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Ectoplasm, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            for (int i = 0; i < 5; i++) {
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
        }

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(-15, -3);
		}
	}
}