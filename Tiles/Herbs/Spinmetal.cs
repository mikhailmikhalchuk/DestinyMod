using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using TheDestinyMod.Items.Placeables;

namespace TheDestinyMod.Tiles.Herbs
{
	public enum PlantStage : byte
	{
		Planted,
		Growing,
		Grown
	}

	public class Spinmetal : ModTile
	{
		private const int FrameWidth = 18;

		public override void SetDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileCut[Type] = true;
			Main.tileNoFail[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Spinmetal");
			name.AddTranslation(GameCulture.Polish, "Wierzbowy Metal");
			AddMapEntry(new Color(144, 161, 163), name);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				TileID.Grass,
				TileID.HallowedGrass,
				TileID.SnowBlock,
				TileID.IceBlock
			};

			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				TileID.ClayPot,
				TileID.PlanterBox
			};

			TileObjectData.addTile(Type);
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects) {
			if (i % 2 == 1)
				spriteEffects = SpriteEffects.FlipHorizontally;
		}

		public override bool Drop(int i, int j) {
			PlantStage stage = GetStage(i, j);

			if (stage == PlantStage.Grown)
				Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ModContent.ItemType<SpinmetalSeeds>());

			return false;
		}

		public override void RandomUpdate(int i, int j) {
			Tile tile = Framing.GetTileSafely(i, j);
			PlantStage stage = GetStage(i, j);
			if (stage != PlantStage.Grown) {
				tile.frameX += FrameWidth;
				if (Main.netMode != NetmodeID.SinglePlayer)
					NetMessage.SendTileSquare(-1, i, j, 1);
			}
		}

		private PlantStage GetStage(int i, int j) {
			Tile tile = Framing.GetTileSafely(i, j);
			return (PlantStage)(tile.frameX / FrameWidth);
		}
	}
}