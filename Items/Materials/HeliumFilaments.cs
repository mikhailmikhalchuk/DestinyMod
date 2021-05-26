using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TheDestinyMod.Items.Materials
{
	public class HeliumFilaments : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Filaments of helium-3 fusion fuel, gathered from the lunar regolith by a helium coil");
		}

		public override void SetDefaults() {
			item.height = 44;
			item.width = 32;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 1, 0);
		}

		public override bool CanBurnInLava() {
			return true;
		}
	}
}