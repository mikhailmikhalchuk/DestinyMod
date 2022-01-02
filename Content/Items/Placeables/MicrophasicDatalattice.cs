using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Placeables
{
	public class MicrophasicDatalattice : TileItem
	{
		public override void DestinySetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 99;
			Item.placeStyle = 0;
			Item.value = Item.buyPrice(copper: 60);
			Item.createTile = ModContent.TileType<Tiles.MicrophasicDatalattice>();
		}
	}
}