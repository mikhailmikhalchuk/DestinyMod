using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Audio;
using DestinyMod.Content.UI.RaidSelection;
using DestinyMod.Common.ModSystems;

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

		public override bool RightClick(int i, int j)
		{
            if (ModContent.GetInstance<RaidSelectionUI>().UserInterface.CurrentState == null)
            {
                ModContent.GetInstance<RaidSelectionUI>().UserInterface.SetState(new RaidSelectionUI());
                ModContent.GetInstance<RaidSelectionUI>().Raid = Language.GetTextValue("Mods.DestinyMod.Common.VaultOfGlass");
                ModContent.GetInstance<RaidSelectionUI>().Clears = VaultOfGlassSystem.RaidClears;
                ModContent.GetInstance<RaidSelectionUI>().DownedRequirement = NPC.downedBoss3;
                ModContent.GetInstance<RaidSelectionUI>().DownedName = Language.GetTextValue("NPCName.SkeletronHead");
                SoundEngine.PlaySound(SoundID.MenuOpen);
                VaultOfGlassSystem.TilePosition = new Vector2(i, j);
            }
            else
            {
                ModContent.GetInstance<RaidSelectionUI>().UserInterface.SetState(null);
                SoundEngine.PlaySound(SoundID.MenuClose);
            }

            return base.RightClick(i, j);
        }
    }
}