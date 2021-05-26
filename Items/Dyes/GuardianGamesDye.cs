using Terraria.ID;
using Terraria.ModLoader;

namespace TheDestinyMod.Items.Dyes
{
    public class GuardianGamesDye : ModItem
    {
        public override void SetDefaults() {
            byte dye = item.dye;
            item.CloneDefaults(ItemID.GelDye);
            item.rare = ItemRarityID.Blue;
            item.dye = dye;
        }
    }
}