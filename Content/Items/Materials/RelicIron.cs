using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Items.ItemTypes;

namespace DestinyMod.Content.Items.Materials
{
	public class RelicIron : TileItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A post-Collapse material of extraordinary density");
		}

		public override int TileType => ModContent.TileType<Tiles.RelicShard>();

		public override void DestinySetDefaults()
		{
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 2, 0);
		}
	}
}