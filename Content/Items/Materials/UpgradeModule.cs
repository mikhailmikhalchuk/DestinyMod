using Terraria;
using DestinyMod.Common.Items;

namespace DestinyMod.Content.Items.Materials
{
	public class UpgradeModule : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A collection of universal components that can be used to infuse power between gear items.");
		}

		public override void DestinySetDefaults()
		{
			Item.maxStack = 99;
			Item.value = Item.buyPrice(0, 1, 0, 0);
		}
	}
}