using Terraria;
using DestinyMod.Common.Items;

namespace DestinyMod.Content.Items.Materials
{
	public class HeliumFilaments : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Filaments of helium-3 fusion fuel, gathered from the lunar regolith by a helium coil");
		}

		public override void DestinySetDefaults()
		{
			Item.maxStack = 999;
			Item.value = Item.buyPrice(0, 0, 1, 0);
		}
	}
}