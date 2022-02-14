using DestinyMod.Common.Items;

namespace DestinyMod.Content.Items.Materials
{
	public class MoteOfDark : DestinyModItem
	{
		public override void SetStaticDefaults() => Tooltip.SetDefault("A mysterious tetrahedral object. The Drifter may like this");

		public override void DestinySetDefaults() => Item.maxStack = 99;
	}
}