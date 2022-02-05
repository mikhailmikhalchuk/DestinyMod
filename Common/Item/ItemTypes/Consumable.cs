using Terraria;
using Terraria.ID;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class Consumable : DestinyModItem
	{
		public override void AutomaticSetDefaults()
		{
			base.AutomaticSetDefaults();
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useTurn = true;
			Item.consumable = true;
			Item.maxStack = 30;
		}
	}
}