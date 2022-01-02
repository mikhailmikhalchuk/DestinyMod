using Terraria;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class TreasureBag : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void AutomaticSetDefaults()
		{
			base.AutomaticSetDefaults();
			Item.maxStack = 999;
			Item.consumable = true;
			Item.expert = true;
		}

		public override bool CanRightClick() => true;
	}
}