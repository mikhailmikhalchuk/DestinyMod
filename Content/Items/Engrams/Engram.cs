using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using DestinyMod.Common.Items;
using Terraria.GameContent.Creative;

namespace DestinyMod.Content.Items.Engrams
{
	public abstract class Engram : DestinyModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A highly advanced, encrypted storage unit"
				+ "\nA cryptarch could probably break its encryption for you");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 6));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void AutomaticSetDefaults()
		{
			//base.AutomaticSetDefaults();
			Item.width = 22;
			Item.height = 22;
			Item.maxStack = 99;
		}

		public override bool? CanBurnInLava() => true;
	}
}