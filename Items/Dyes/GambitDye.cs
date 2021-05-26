using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Dyes
{
    public class GambitDye : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.AddTranslation(GameCulture.Polish, "Barwnik Gambit");
        }

        public override void SetDefaults() {
            byte dye = item.dye;
			item.CloneDefaults(ItemID.GelDye);
            item.rare = ItemRarityID.Blue;
			item.dye = dye;
        }
    }
}