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
	public class OmolonSidearm : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Broń Boczna Omolon");
			Tooltip.SetDefault("Standard Omolon Sidearm");
		}
		public override void SetDefaults() {
			item.damage = 20;
			item.ranged = true;
			item.width = 60;
			item.height = 30;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.LightRed;
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

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) {
			Texture2D texture = ModContent.GetTexture("TheDestinyMod/Items/Weapons/Ranged/OmolonSidearm_Glow");
			spriteBatch.Draw(texture,new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddIngredient(ItemID.CobaltBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddIngredient(ItemID.PalladiumBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}