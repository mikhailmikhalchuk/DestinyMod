using DestinyMod.Common.Items.ItemTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DestinyMod.Content.Items.Bosses.SepiksPrime
{
	public class SepiksPrimeTrophy : TileItem
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("Can be used to deposit Laurels");

		public override void DestinySetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 99;
			Item.placeStyle = 0;
			Item.value = Item.buyPrice(gold: 1);
			Item.createTile = ModContent.TileType<Tiles.Trophies.SepiksPrimeTrophy>();
		}
	}
}