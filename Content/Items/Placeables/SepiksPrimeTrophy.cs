using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Placeables
{
	public class SepiksPrimeTrophy : ModItem
	{
		public override void SetDefaults() {
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.createTile = ModContent.TileType<Tiles.SepiksPrimeTrophy>();
			item.placeStyle = 0;
		}
	}
}