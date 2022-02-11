using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Audio;

namespace DestinyMod.Content.Tiles
{
    public class VoGTeleport : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            MinPick = 50; // 500
            MineResist = 2f; // 200
            AddMapEntry(new Color(255, 255, 0));
            SoundType = SoundID.Tink;
        }

        //Come back to this
		/*public override bool RightClick(int i, int j)
		{
            if (ModContent.GetInstance<TheDestinyMod>().raidInterface.CurrentState == null)
            {
                ModContent.GetInstance<TheDestinyMod>().raidInterface.SetState(new UI.RaidSelectionUI(Language.GetTextValue("Mods.TheDestinyMod.VaultOfGlass"), DestinyWorld.clearsVOG, NPC.downedBoss3, Language.GetTextValue("NPCName.SkeletronHead")));
                SoundEngine.PlaySound(SoundID.MenuOpen);
                DestinyWorld.vogPosition = new Vector2(i, j);
            }
            else
            {
                ModContent.GetInstance<TheDestinyMod>().raidInterface.SetState(null);
                SoundEngine.PlaySound(SoundID.MenuClose);
            }

            return base.RightClick(i, j);
        }*/
    }
}