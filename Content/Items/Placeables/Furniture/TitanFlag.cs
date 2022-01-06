using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Placeables.Furniture
{
	public class TitanFlag : TileItem
	{
		public override void DestinySetDefaults()
		{
			Item.rare = ItemRarityID.White;
			Item.maxStack = 99;
			Item.placeStyle = 0;
			Item.value = Item.buyPrice(copper: 80);
			Item.createTile = ModContent.TileType<Tiles.Furniture.TitanFlag>();
		}
	}
}