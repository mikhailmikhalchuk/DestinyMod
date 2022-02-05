using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Bosses.SepiksPrime
{
	public class SepiksPrimeBox : TileItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Music Box (Sepiks Prime)");

		public override void DestinySetDefaults()
		{
			Item.maxStack = 1;
			Item.createTile = ModContent.TileType<Tiles.MusicBoxes.SepiksPrimeBox>();
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(gold: 2);
			Item.accessory = true;
		}
	}
}