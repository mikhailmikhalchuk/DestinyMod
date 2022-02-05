using Terraria;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;

namespace DestinyMod.Common.Tiles.TileType
{
	public abstract class Flag : DestinyModTile
	{
		public override void AutomaticSetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 9;
			TileObjectData.newTile.Origin = new Point16(3, 8);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16, 16, 16, 16 };
			TileObjectData.newTile.Width = 7;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, 1, 3);
			TileObjectData.addTile(Type);
			DustType = 1;
		}
	}
}