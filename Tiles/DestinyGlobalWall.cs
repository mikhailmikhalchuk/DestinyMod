using Terraria;
using Terraria.ModLoader;

namespace TheDestinyMod.Tiles
{
    public class DestinyGlobalWall : GlobalWall
    {
        public override bool CanExplode(int i, int j, int type) {
            if (TheDestinyMod.currentSubworldID != string.Empty) {
                return false;
            }
            return base.CanExplode(i, j, type);
        }
    }
}