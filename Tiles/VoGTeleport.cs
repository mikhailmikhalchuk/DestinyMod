using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Tiles
{
    public class VoGTeleport : ModTile
    {
        public override void SetDefaults() {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            minPick = 500;
            mineResist = 200f;
            AddMapEntry(new Color(255, 255, 0));
            soundType = SoundID.Tink;
        }

        public override bool NewRightClick(int i, int j) {
            if (ModContent.GetInstance<TheDestinyMod>().raidInterface.CurrentState == null) {
                ModContent.GetInstance<TheDestinyMod>().raidInterface.SetState(new UI.RaidSelectionUI());
                Main.PlaySound(SoundID.MenuOpen);
                DestinyWorld.vogPosition = new Vector2(i, j);
            }
            else {
                ModContent.GetInstance<TheDestinyMod>().raidInterface.SetState(null);
                Main.PlaySound(SoundID.MenuClose);
            }

            return base.NewRightClick(i, j);
        }
    }
}