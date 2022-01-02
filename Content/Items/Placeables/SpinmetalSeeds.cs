using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace TheDestinyMod.Items.Placeables
{
	public class SpinmetalSeeds : ModItem
	{
        public override void SetStaticDefaults() {
            DisplayName.AddTranslation(GameCulture.Polish, "Wierzbowy Metal Nasiona");
        }

		public override void SetDefaults() {
			item.autoReuse = true;
			item.useTurn = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useAnimation = 15;
			item.useTime = 10;
			item.maxStack = 99;
			item.consumable = true;
			item.placeStyle = 0;
			item.width = 12;
			item.height = 20;
			item.value = 80;
			item.createTile = ModContent.TileType<Tiles.Herbs.Spinmetal>();
		}
	}
}