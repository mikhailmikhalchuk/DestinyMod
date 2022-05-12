using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using DestinyMod.Common.Tiles.TileType;
using Terraria.DataStructures;

namespace DestinyMod.Content.Tiles.Furniture
{
	public class HunterFlag : Flag
	{
		public override void DestinySetStaticDefaults()
		{
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Hunter Flag");
			AddMapEntry(new Color(128, 221, 255), name);
			DustType = 1;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 124, 160, ModContent.ItemType<Items.Placeables.Furniture.HunterFlag>());
		}
	}
}