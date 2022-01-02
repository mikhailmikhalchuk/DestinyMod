using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace TheDestinyMod.Items.Placeables.Furniture
{
	public class TitanFlag : ModItem
	{
		public override void SetDefaults() {
			item.autoReuse = true;
			item.useTurn = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.rare = ItemRarityID.White;
			item.useAnimation = 15;
			item.useTime = 10;
			item.maxStack = 99;
			item.consumable = true;
			item.placeStyle = 0;
			item.width = 30;
			item.height = 32;
			item.value = 80;
			item.createTile = ModContent.TileType<Tiles.Furniture.TitanFlag>();
		}
	}
}