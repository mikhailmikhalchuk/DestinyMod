using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ModLoader;

namespace DestinyMod.Common.BossBars
{
    public class ShieldBossBar : ModBossBar
    {
        public float ShieldPercentToSet = 0f;

        private int _headIndex = -1;

        public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
        {
            if (_headIndex != -1)
            {
                return TextureAssets.NpcHeadBoss[_headIndex];
            }
            return null;
        }

        public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float lifePercent, ref float shieldPercent)
        {
            NPC npc = Main.npc[info.npcIndexToAimAt];
            
            if (!npc.active)
            {
                return false;
            }

            _headIndex = npc.GetBossHeadTextureIndex();

            lifePercent = Utils.Clamp(npc.life / (float)npc.lifeMax, 0f, 1f);

            shieldPercent = ShieldPercentToSet;

            return true;
        }
    }
}