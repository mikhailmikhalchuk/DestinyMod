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
	public class KhvostovSupercell : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Khvostov Supercell");
		}

		public override void SetDefaults() {
			item.damage = 40;
			item.ranged = true;
			item.width = 100;
			item.height = 42;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Hakke1");
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 30f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .7f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			position.Y -= 3;
			return true;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.7f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(-13, 0);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<KhvostovGX>(), 1);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
			recipe.AddIngredient(ItemID.Ectoplasm, 15);
			recipe.AddIngredient(ItemID.PixieDust, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}