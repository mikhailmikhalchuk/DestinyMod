using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheDestinyMod.Tiles
{
	public class MicrophasicDatalattice : ModTile
	{
		public override void SetDefaults() {
			Main.tileFrameImportant[Type] = true;
            animationFrameHeight = 56;
            disableSmartCursor = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Microphasic Datalattice");
			name.AddTranslation(GameCulture.Polish, "Mikrofazowe Dane");
			AddMapEntry(new Color(160, 231, 242), name);
		}

        public override void KillMultiTile(int i, int j, int frameX, int frameY) {
            Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<Items.Placeables.MicrophasicDatalattice>());
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) {
			Tile tile = Main.tile[i, j];
			Texture2D texture;
			if (Main.canDrawColorTile(i, j)) {
				texture = Main.tileAltTexture[Type, tile.color()];
			}
			else {
				texture = Main.tileTexture[Type];
			}
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen) {
				zero = Vector2.Zero;
			}
			int height = tile.frameY % animationFrameHeight == 36 ? 18 : 16;
			Main.spriteBatch.Draw(texture, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            return false;
		}
		
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch) {
            Lighting.AddLight(new Vector2(i * 16, j * 16), Color.White.ToVector3() * 0.55f);
        }
	}
}