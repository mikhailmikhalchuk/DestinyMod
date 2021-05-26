using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class JadeRabbit : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Jade Rabbit");
			Tooltip.SetDefault("\"What kind of harebrained scheme have you got in mind this time?\"");
			Tooltip.AddTranslation(GameCulture.Polish, "Jaki bezsensowny plan masz na myśli tym razem?");
		}

		public override void SetDefaults() {
			item.damage = 150;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/JadeRabbit");
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 300f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = 1.05f;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-13, 0);
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
			scale *= 1.05f;
            return true;
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