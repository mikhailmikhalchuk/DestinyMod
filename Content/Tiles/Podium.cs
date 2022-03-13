using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using DestinyMod.Common.ModSystems;

namespace DestinyMod.Content.Tiles
{
	public class Podium : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 9;
			TileObjectData.newTile.Origin = new Point16(5, 8);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16, 16, 16, 16 };
			TileObjectData.newTile.Width = 10;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.StyleHorizontal = true;
			// TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			DustType = 63;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Podium");
			AddMapEntry(new Color(255, 255, 255), name);
		}

		public override bool HasSmartInteract() => true;

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 178, 178, ModContent.ItemType<Items.Placeables.Podium>());
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			Lighting.AddLight(new Vector2(i * 16, j * 16), Color.White.ToVector3() * 0.55f);
		}
	
		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<Items.Misc.Laurel>();
		}

		public override bool RightClick(int i, int j) => GuardianGamesSystem.TryDeposit(Main.LocalPlayer);
	}
}