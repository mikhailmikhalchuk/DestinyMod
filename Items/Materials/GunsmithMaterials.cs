using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Materials
{
	public class GunsmithMaterials : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.AddTranslation(GameCulture.Polish, "Materiały Rusznikarskie");
			Tooltip.SetDefault("Used to craft guns");
		}

        public override void SetDefaults() {
			item.height = 32;
			item.width = 32;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 1, 0);
        }

		public override bool CanBurnInLava() {
			return true;
		}
	}
}