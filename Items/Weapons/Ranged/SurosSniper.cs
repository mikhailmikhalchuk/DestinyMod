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
	public class SurosSniper : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SUROS Sniper");
			DisplayName.AddTranslation(GameCulture.Polish, "Karabin Snajperski SUROS");
			Tooltip.SetDefault("Standard SUROS Sniper Rifle");
		}

		public override void SetDefaults() {
			item.damage = 80;
			item.ranged = true;
			item.width = 100;
			item.height = 32;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Pink;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JadeRabbit");
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 300f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = .85f;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 0.8f;
            return true;
        }

		public override Vector2? HoldoutOffset() {
			return new Vector2(-10, -3);
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