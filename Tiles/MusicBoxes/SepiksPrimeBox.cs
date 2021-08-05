using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace TheDestinyMod.Tiles.MusicBoxes
{
    public class SepiksPrimeBox : ModTile
    {
        public override void SetDefaults() {
            Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault(Language.GetTextValue("ItemName.MusicBox"));
			AddMapEntry(new Color(200, 200, 200), name);
			dustType = -1;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			Item.NewItem(i * 16, j * 16, 16, 48, ModContent.ItemType<Items.Placeables.MusicBoxes.SepiksPrimeBox>());
		}

		public override void MouseOver(int i, int j) {
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = ModContent.ItemType<Items.Placeables.MusicBoxes.SepiksPrimeBox>();
		}
    }
}