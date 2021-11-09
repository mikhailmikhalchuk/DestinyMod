using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Projectiles.Ranged;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Gjallarhorn : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Fires a homing rocket which explodes into mini rockets on impact\n\"If there is beauty in destruction, why not also in its delivery?\"");
			Tooltip.AddTranslation(GameCulture.Polish, "Wystrzeliwuje rakietę samonaprowadzającą ktora wybucha po uderzeniu zmienia się w mniejsze rakiety\n\"Skoro jest piękno w destrukcji dlaczego tego piękna nie ma w jaki sposób się je powoduje?\"");
		}

		public override void SetDefaults() {
			item.damage = 520;
			item.ranged = true;
			item.width = 98;
			item.height = 34;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Gjallarhorn");
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Rocket;
			item.scale = .80f;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Projectile.NewProjectile(position.X, position.Y - 6, speedX, speedY, ModContent.ProjectileType<GjallarhornRocket>(), damage, knockBack, player.whoAmI);
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
			recipe.AddIngredient(ItemID.Ectoplasm, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}