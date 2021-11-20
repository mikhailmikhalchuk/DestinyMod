using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using TheDestinyMod.Items.Materials;

namespace TheDestinyMod.Items.Weapons.Ranged
{
	public class MidaMultiTool : ModItem
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("MIDA Multi-Tool");
			DisplayName.AddTranslation(GameCulture.Polish, "Narzędzie Wielofunkcyjne MIDA");
			Tooltip.SetDefault("\"Select application: Ballistic engagement. Entrenching tool. Avionics trawl. Troll smasher. Stellar sextant. List continues.\"");
			Tooltip.AddTranslation(GameCulture.Polish, "\"Wybierz zastosowanie: Zaangażowanie balistyczne. Narzędzie do okopywania. Włok awioniki. Gwiezdny sekstant. A lista dalej się ciągnie.\"");
		}

		public override void SetDefaults() {
			item.damage = 35;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;
			item.width = 40;
			item.height = 20;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 4;
			item.crit = 2;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MidaMultiTool");
			item.shoot = 10;
			item.shootSpeed = 30f;
			item.useAmmo = AmmoID.Bullet;
			item.scale = 1.0f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			position.Y -= 4;
            return true;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-12, 0);
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