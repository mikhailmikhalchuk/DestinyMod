using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;

namespace TheDestinyMod.Tiles.Furniture
{
	public class HunterFlag : ModTile
	{
		public override void SetDefaults() {
			Main.tileFrameImportant[Type] = true;
			disableSmartCursor = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 9;
			TileObjectData.newTile.Origin = new Point16(3, 8);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16, 16, 16, 16 };
			TileObjectData.newTile.Width = 7;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, 1, 3);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Hunter Flag");
			AddMapEntry(new Color(128, 221, 255), name);
			dustType = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			Item.NewItem(i * 16, j * 16, 124, 160, ModContent.ItemType<Items.Placeables.Furniture.HunterFlag>());
		}
	}
}