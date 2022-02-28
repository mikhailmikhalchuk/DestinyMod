using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DestinyMod.Content.Tiles
{
	public class MicrophasicDatalattice : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			// TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Microphasic Datalattice");
			AddMapEntry(new Color(160, 231, 242), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<Items.Placeables.MicrophasicDatalattice>());

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			Vector2 drawOffSet = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
			Vector2 screenPosition = new Vector2(i, j).ToWorldCoordinates().ToScreenPosition();
			spriteBatch.Draw(TextureAssets.Tile[Type].Value, screenPosition + drawOffSet, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			return false;
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch) => Lighting.AddLight(new Vector2(i * 16, j * 16), Color.White.ToVector3() * 0.55f);
	}
}