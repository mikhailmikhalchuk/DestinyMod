using Terraria;
using Terraria.GameContent.Creative;
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

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}
	}
}