using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Placeables
{
	public class Podium : TileItem
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Can be used to deposit Laurels");

		public override void DestinySetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 99;
			Item.placeStyle = 0;
			Item.value = Item.buyPrice(copper: 80);
			Item.createTile = ModContent.TileType<Tiles.Podium>();
		}
	}
}