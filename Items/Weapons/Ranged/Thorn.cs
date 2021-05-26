using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class Thorn : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Rounds pierce targets\n\"To rend one's enemies is to see them not as equals, but objects - hollow of spirit and meaning.\"");
		}

		public override void SetDefaults() {
			item.damage = 45;
			item.ranged = true;
			item.width = 58;
			item.height = 30;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Thorn");
			item.shoot = 10;
			item.shootSpeed = 18f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .80f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(6));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			int p = Projectile.NewProjectile(position.X, position.Y - 1, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Main.projectile[p].penetrate = -1;
			return false;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
			return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(0, 3);
		}

		/*public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Ectoplasm, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }*/
	}
}