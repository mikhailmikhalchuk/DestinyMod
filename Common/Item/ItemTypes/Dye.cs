using Terraria;
using Terraria.ID;

namespace DestinyMod.Common.Items.ItemTypes
{
	public abstract class Dye : DestinyModItem
	{
		public override void AutomaticSetDefaults()
		{
			base.AutomaticSetDefaults();
			int dye = Item.dye;
			Item.CloneDefaults(ItemID.GelDye);
			Item.dye = dye;
		}
	}
}