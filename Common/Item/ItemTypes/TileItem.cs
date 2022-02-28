using Terraria;
using Terraria.ID;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class TileItem : DestinyModItem
	{
		public virtual int TileType { get; } = -1;

		public override void AutomaticSetDefaults()
		{
			base.AutomaticSetDefaults();
			Item.useTime = 10;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = TileType;
		}
	}
}